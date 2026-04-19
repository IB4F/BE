using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Application.Services.Providers
{
    /// <summary>
    /// Paddle Billing (v2) payment provider.
    /// Docs: https://developer.paddle.com/
    /// Required env vars: PADDLE_API_KEY, PADDLE_WEBHOOK_SECRET
    /// Optional: PADDLE_SANDBOX=true for test mode
    /// </summary>
    public class PaddlePaymentProvider : IPaymentProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<PaddlePaymentProvider> _logger;

        public PaymentProvider ProviderType => PaymentProvider.Paddle;

        public PaddlePaymentProvider(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            ILogger<PaddlePaymentProvider> logger)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        private string ApiBaseUrl => IsSandbox()
            ? "https://sandbox-api.paddle.com"
            : "https://api.paddle.com";

        public async Task<PaymentSessionResult> CreateCheckoutSessionAsync(PaymentSessionRequest request)
        {
            var apiKey = _configuration["PADDLE_API_KEY"]
                ?? throw new InvalidOperationException("PADDLE_API_KEY not configured");

            var package = request.Package;

            var paddlePriceId = request.BillingInterval == BillingInterval.Year
                ? package.PaddleYearlyPriceId
                : package.PaddleMonthlyPriceId;

            if (string.IsNullOrEmpty(paddlePriceId))
                throw new InvalidOperationException($"Paddle price ID not configured for package {package.Name} with interval {request.BillingInterval}");

            var customData = new
            {
                registration_type = request.RegistrationType,
                subscription_package_id = package.Id.ToString(),
                billing_interval = request.BillingInterval.ToString(),
                registration_data = request.RegistrationData
            };

            var payload = new
            {
                items = new[]
                {
                    new { price_id = paddlePriceId, quantity = 1 }
                },
                customer = new { email = request.Email },
                custom_data = customData,
                success_url = request.SuccessUrl
            };

            var client = _httpClientFactory.CreateClient("Paddle");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{ApiBaseUrl}/transactions", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Paddle API error: {StatusCode} {Body}", response.StatusCode, responseBody);
                throw new InvalidOperationException($"Paddle API returned {response.StatusCode}");
            }

            using var doc = JsonDocument.Parse(responseBody);
            var data = doc.RootElement.GetProperty("data");
            var transactionId = data.GetProperty("id").GetString();
            var checkoutUrl = data.GetProperty("checkout").GetProperty("url").GetString();

            _logger.LogInformation("Paddle transaction created: {TransactionId}", transactionId);

            return new PaymentSessionResult
            {
                SessionId = transactionId,
                CheckoutUrl = checkoutUrl,
                Provider = PaymentProvider.Paddle
            };
        }

        public async Task<bool> CancelSubscriptionAsync(string externalSubscriptionId)
        {
            try
            {
                var apiKey = _configuration["PADDLE_API_KEY"]!;
                var client = _httpClientFactory.CreateClient("Paddle");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", apiKey);

                var response = await client.PostAsync(
                    $"{ApiBaseUrl}/subscriptions/{externalSubscriptionId}/cancel",
                    new StringContent("{\"effective_from\":\"next_billing_period\"}", Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Paddle error canceling subscription: {Id}", externalSubscriptionId);
                return false;
            }
        }

        public async Task<bool> PauseSubscriptionAsync(string externalSubscriptionId)
        {
            try
            {
                var apiKey = _configuration["PADDLE_API_KEY"]!;
                var client = _httpClientFactory.CreateClient("Paddle");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", apiKey);

                var response = await client.PostAsync(
                    $"{ApiBaseUrl}/subscriptions/{externalSubscriptionId}/pause",
                    new StringContent("{\"effective_from\":\"next_billing_period\"}", Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Paddle error pausing subscription: {Id}", externalSubscriptionId);
                return false;
            }
        }

        public async Task<bool> ResumeSubscriptionAsync(string externalSubscriptionId)
        {
            try
            {
                var apiKey = _configuration["PADDLE_API_KEY"]!;
                var client = _httpClientFactory.CreateClient("Paddle");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", apiKey);

                var response = await client.PostAsync(
                    $"{ApiBaseUrl}/subscriptions/{externalSubscriptionId}/resume",
                    new StringContent("{}", Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Paddle error resuming subscription: {Id}", externalSubscriptionId);
                return false;
            }
        }

        /// <summary>
        /// Validates Paddle webhook using HMAC-SHA256 with the webhook secret key.
        /// </summary>
        public bool VerifyWebhookSignature(string body, string paddleSignatureHeader)
        {
            // Paddle-Signature: ts=<timestamp>;h1=<hash>
            var webhookSecret = _configuration["PADDLE_WEBHOOK_SECRET"] ?? "";

            var parts = paddleSignatureHeader.Split(';');
            string? ts = null;
            string? h1 = null;

            foreach (var part in parts)
            {
                if (part.StartsWith("ts=")) ts = part[3..];
                if (part.StartsWith("h1=")) h1 = part[3..];
            }

            if (ts == null || h1 == null) return false;

            var signedPayload = $"{ts}:{body}";
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(webhookSecret));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signedPayload));
            var expected = Convert.ToHexString(hash).ToLower();

            return string.Equals(expected, h1, StringComparison.OrdinalIgnoreCase);
        }

        private bool IsSandbox() =>
            bool.TryParse(_configuration["PADDLE_SANDBOX"], out var sandbox) && sandbox;
    }
}
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Application.Services.Providers
{
    /// <summary>
    /// Novalnet payment provider.
    /// Docs: https://developer.novalnet.de/
    /// Required env vars: NOVALNET_API_KEY, NOVALNET_TARBALL_KEY, NOVALNET_PROJECT_ID
    /// </summary>
    public class NovalnetPaymentProvider : IPaymentProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<NovalnetPaymentProvider> _logger;

        private const string ApiBaseUrl = "https://payport.novalnet.de/v2";

        public PaymentProvider ProviderType => PaymentProvider.Novalnet;

        public NovalnetPaymentProvider(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            ILogger<NovalnetPaymentProvider> logger)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<PaymentSessionResult> CreateCheckoutSessionAsync(PaymentSessionRequest request)
        {
            var apiKey = _configuration["NOVALNET_API_KEY"];
            var tarballKey = _configuration["NOVALNET_TARBALL_KEY"];
            var projectId = _configuration["NOVALNET_PROJECT_ID"];

            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(tarballKey) || string.IsNullOrEmpty(projectId))
                throw new InvalidOperationException("Novalnet non è ancora configurato. Aggiungi NOVALNET_API_KEY, NOVALNET_TARBALL_KEY e NOVALNET_PROJECT_ID nel file .env.");

            var package = request.Package;
            var amountCents = CalculateAmount(package, request.BillingInterval, request.FamilyMemberCount);

            // Novalnet reference: used to track the subscription in their system
            var orderRef = $"SUB-{Guid.NewGuid():N}";

            var payload = new
            {
                merchant = new
                {
                    signature = tarballKey,
                    tariff = projectId
                },
                customer = new
                {
                    first_name = request.Email.Split('@')[0],
                    email = request.Email
                },
                transaction = new
                {
                    test_mode = IsTestMode() ? 1 : 0,
                    payment_type = "CREDITCARD",
                    amount = amountCents,
                    currency = "EUR",
                    order_no = orderRef,
                    hook_url = _configuration["NOVALNET_WEBHOOK_URL"] ?? ""
                },
                subscription = new
                {
                    interval = request.BillingInterval == BillingInterval.Year ? "1y" : "1m",
                    period = request.BillingInterval == BillingInterval.Year ? "y" : "m"
                },
                custom = new
                {
                    lang = "EN",
                    input1 = "registration_type",
                    inputval1 = request.RegistrationType,
                    input2 = "subscription_package_id",
                    inputval2 = package.Id.ToString(),
                    input3 = "billing_interval",
                    inputval3 = request.BillingInterval.ToString()
                }
            };

            var client = _httpClientFactory.CreateClient("Novalnet");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{apiKey}:")));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{ApiBaseUrl}/seamless/payment", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Novalnet API error: {StatusCode} {Body}", response.StatusCode, responseBody);
                throw new InvalidOperationException($"Novalnet API returned {response.StatusCode}");
            }

            using var doc = JsonDocument.Parse(responseBody);
            var root = doc.RootElement;

            var resultStatus = root.GetProperty("result").GetProperty("status").GetString();
            if (resultStatus != "SUCCESS")
            {
                var statusText = root.GetProperty("result").GetProperty("status_text").GetString();
                throw new InvalidOperationException($"Novalnet error: {statusText}");
            }

            var redirectUrl = root.GetProperty("result").GetProperty("redirect_url").GetString();
            var txnId = root.GetProperty("transaction").GetProperty("tid").GetString();

            _logger.LogInformation("Novalnet session created: {TxnId}", txnId);

            return new PaymentSessionResult
            {
                SessionId = txnId,
                CheckoutUrl = redirectUrl,
                Provider = PaymentProvider.Novalnet
            };
        }

        public async Task<bool> CancelSubscriptionAsync(string externalSubscriptionId)
        {
            try
            {
                var apiKey = _configuration["NOVALNET_API_KEY"]!;
                var tarballKey = _configuration["NOVALNET_TARBALL_KEY"]!;

                var payload = new
                {
                    merchant = new { signature = tarballKey },
                    transaction = new { tid = externalSubscriptionId },
                    subscription = new { reason = "Cancelled by user" }
                };

                var client = _httpClientFactory.CreateClient("Novalnet");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes($"{apiKey}:")));

                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{ApiBaseUrl}/subscription/cancel", content);
                var responseBody = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(responseBody);
                var status = doc.RootElement.GetProperty("result").GetProperty("status").GetString();

                return status == "SUCCESS";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Novalnet error canceling subscription: {Id}", externalSubscriptionId);
                return false;
            }
        }

        public async Task<bool> PauseSubscriptionAsync(string externalSubscriptionId)
        {
            try
            {
                var apiKey = _configuration["NOVALNET_API_KEY"]!;
                var tarballKey = _configuration["NOVALNET_TARBALL_KEY"]!;

                var payload = new
                {
                    merchant = new { signature = tarballKey },
                    transaction = new { tid = externalSubscriptionId }
                };

                var client = _httpClientFactory.CreateClient("Novalnet");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes($"{apiKey}:")));

                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{ApiBaseUrl}/subscription/suspend", content);
                var responseBody = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(responseBody);
                var status = doc.RootElement.GetProperty("result").GetProperty("status").GetString();

                return status == "SUCCESS";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Novalnet error pausing subscription: {Id}", externalSubscriptionId);
                return false;
            }
        }

        public async Task<bool> ResumeSubscriptionAsync(string externalSubscriptionId)
        {
            try
            {
                var apiKey = _configuration["NOVALNET_API_KEY"]!;
                var tarballKey = _configuration["NOVALNET_TARBALL_KEY"]!;

                var payload = new
                {
                    merchant = new { signature = tarballKey },
                    transaction = new { tid = externalSubscriptionId }
                };

                var client = _httpClientFactory.CreateClient("Novalnet");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes($"{apiKey}:")));

                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{ApiBaseUrl}/subscription/reactivate", content);
                var responseBody = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(responseBody);
                var status = doc.RootElement.GetProperty("result").GetProperty("status").GetString();

                return status == "SUCCESS";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Novalnet error resuming subscription: {Id}", externalSubscriptionId);
                return false;
            }
        }

        /// <summary>
        /// Validates Novalnet webhook signature using HMAC-SHA256.
        /// Returns true if the signature header matches.
        /// </summary>
        public bool VerifyWebhookSignature(HttpRequest request, string body)
        {
            var tarballKey = _configuration["NOVALNET_TARBALL_KEY"] ?? "";
            var receivedChecksum = request.Headers["x-novalnet-signature"].ToString();

            if (string.IsNullOrEmpty(receivedChecksum))
                return false;

            using var hmac = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes(tarballKey));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(body));
            var expected = Convert.ToBase64String(hash);

            return string.Equals(expected, receivedChecksum, StringComparison.OrdinalIgnoreCase);
        }

        private bool IsTestMode() =>
            bool.TryParse(_configuration["NOVALNET_TEST_MODE"], out var testMode) && testMode;

        private static long CalculateAmount(Domain.Entities.SubscriptionPackage package, BillingInterval interval, int familyMemberCount)
        {
            if (package.UserType == Domain.Enums.UserType.Family &&
                package.BasePrice.HasValue && package.PricePerAdditionalMember.HasValue)
            {
                var minMembers = package.MinFamilyMembers ?? 1;
                var additionalMembers = Math.Max(0, familyMemberCount - minMembers);
                var basePrice = interval == BillingInterval.Year ? package.YearlyPrice : package.MonthlyPrice;
                return basePrice + (additionalMembers * package.PricePerAdditionalMember.Value);
            }

            return interval == BillingInterval.Year ? package.YearlyPrice : package.MonthlyPrice;
        }
    }
}
using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Application.Services;
using TeachingBACKEND.Application.Services.Providers;
using TeachingBACKEND.Domain.Enums;
using TeachingBACKEND.Data;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using DotNetEnv;
using static TeachingBACKEND.Application.Services.PasswordValidationService;
using TeachingBACKEND.Infrastructure;
using System.Text.Json.Serialization;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using TeachingBACKEND.Middleware;
using Microsoft.AspNetCore.HttpOverrides;

Env.Load();



var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TeachingBACKEND API", Version = "v1" });
    c.EnableAnnotations();
    
    // Include XML comments for API documentation
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT as: **Bearer &lt;token&gt;**"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
    ServiceLifetime.Scoped);

builder.Services.AddMemoryCache();
builder.Services.AddHealthChecks();

builder.Services
  .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
      // Force the old JwtSecurityTokenHandler path.
      // JwtBearer 8.x defaults to JsonWebTokenHandler, but its interaction with
      // System.IdentityModel 8.6.1 causes signature validation failures.
      // JwtSecurityTokenHandler is consistent with how tokens are generated and
      // with GetPrincipalFromExpiredToken in PasswordService.
#pragma warning disable CS0618
      options.UseSecurityTokenValidators = true;
#pragma warning restore CS0618

      var jwtSecret = builder.Configuration["JWT_SECRET_KEY"]
          ?? throw new InvalidOperationException("JWT_SECRET_KEY is not configured.");

      var validIssuer = builder.Configuration["JWT_ISSUER"];
      var validAudience = builder.Configuration["JWT_AUDIENCE"];

      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = !string.IsNullOrWhiteSpace(validIssuer),
          ValidIssuer = validIssuer,
          ValidateAudience = !string.IsNullOrWhiteSpace(validAudience),
          ValidAudience = validAudience,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
          ClockSkew = TimeSpan.Zero
      };

      options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
      {
          OnAuthenticationFailed = ctx =>
          {
              var logger = ctx.HttpContext.RequestServices
                  .GetRequiredService<ILogger<Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerHandler>>();
              logger.LogWarning(ctx.Exception,
                  "JWT authentication failed on {Method} {Path}: {Error}",
                  ctx.Request.Method, ctx.Request.Path, ctx.Exception.GetType().Name);
              return System.Threading.Tasks.Task.CompletedTask;
          },
          OnTokenValidated = ctx =>
          {
              var logger = ctx.HttpContext.RequestServices
                  .GetRequiredService<ILogger<Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerHandler>>();
              var nameId = ctx.Principal?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                        ?? ctx.Principal?.FindFirst("nameid")?.Value
                        ?? ctx.Principal?.FindFirst("sub")?.Value
                        ?? "NOT FOUND";
              logger.LogDebug("JWT validated. NameIdentifier claim: {NameId}", nameId);
              return System.Threading.Tasks.Task.CompletedTask;
          }
      };
  });


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins(
                "https://app.braingainalbania.al",
                "https://braingainalbania.al",
                "http://localhost:4200",
                "https://localhost:4200"
            )
            .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS")
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddRateLimiter(options =>
{
    // POST /api/auth/login — max 5 per minute per IP
    options.AddPolicy("login_limit", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            }));

    // POST /api/auth/request-reset — max 3 per 15 min per IP
    options.AddPolicy("reset_limit", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 3,
                Window = TimeSpan.FromMinutes(15),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            }));

    // POST /api/auth/resend-verification — max 3 per 15 min per IP
    options.AddPolicy("verify_limit", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 3,
                Window = TimeSpan.FromMinutes(15),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            }));

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

builder.Services.AddHttpContextAccessor(); // Add this for URL generation
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAdminUserService, AdminUserService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<StripePaymentProvider>();
builder.Services.AddScoped<NovalnetPaymentProvider>();
builder.Services.AddScoped<PaddlePaymentProvider>();
builder.Services.AddScoped<IPaymentProvider>(sp => sp.GetRequiredService<StripePaymentProvider>());
builder.Services.AddScoped<IPaymentProvider>(sp => sp.GetRequiredService<NovalnetPaymentProvider>());
builder.Services.AddScoped<IPaymentProvider>(sp => sp.GetRequiredService<PaddlePaymentProvider>());
builder.Services.AddScoped<IPaymentProvider>(_ =>
{
    var cfg = _.GetRequiredService<IConfiguration>();
    var email = _.GetRequiredService<IEmailService>();
    var logger = _.GetRequiredService<ILogger<ManualPaymentProvider>>();
    return new ManualPaymentProvider(cfg, email, logger, PaymentProvider.BKT);
});
builder.Services.AddScoped<IPaymentProvider>(_ =>
{
    var cfg = _.GetRequiredService<IConfiguration>();
    var email = _.GetRequiredService<IEmailService>();
    var logger = _.GetRequiredService<ILogger<ManualPaymentProvider>>();
    return new ManualPaymentProvider(cfg, email, logger, PaymentProvider.Raiffeisen);
});
builder.Services.AddScoped<PaymentProviderFactory>();
builder.Services.AddHttpClient("Novalnet");
builder.Services.AddHttpClient("Paddle");
builder.Services.AddScoped<IDetailsService, DetailsService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<ILearnHubService, LearnHubService>();
builder.Services.AddScoped<IStudentPerformanceService, StudentPerformanceService>();
builder.Services.AddScoped<IPasswordValidationService, PasswordValidationService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ISubscriptionPackageService, SubscriptionPackageService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IAdminDashboardService, AdminDashboardService>();
builder.Services.AddScoped<ISupervisorService, SupervisorService>();
builder.Services.AddScoped<IFamilyService, FamilyService>();
builder.Services.AddScoped<IStudentProgressService, StudentProgressService>();
builder.Services.AddScoped<ISubscriptionAccessService, SubscriptionAccessService>();
builder.Services.AddScoped<FamilyPricingService>();
builder.Services.AddScoped<StripePricingService>();
builder.Services.AddHostedService<OrphanedFilesCleanupJob>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });



builder.Services.AddAutoMapper(typeof(LearnHubProfile).Assembly);

Stripe.StripeConfiguration.ApiKey = builder.Configuration["STRIPE_SECRET_KEY"];


var minLogLevel = builder.Environment.IsDevelopment()
    ? Serilog.Events.LogEventLevel.Debug
    : Serilog.Events.LogEventLevel.Warning;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Is(minLogLevel)
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog(); 



var app = builder.Build();

var forwardedOptions = new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
};
forwardedOptions.KnownNetworks.Clear();
forwardedOptions.KnownProxies.Clear();
app.UseForwardedHeaders(forwardedOptions);

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

// Security headers
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Append("X-Frame-Options", "DENY");
    context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
    context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
    if (!app.Environment.IsDevelopment())
        context.Response.Headers.Append("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
    await next();
});

app.UseCors("AllowFrontend");
app.UseStaticFiles();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

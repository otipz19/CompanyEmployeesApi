using AspNetCoreRateLimit;
using Contracts.LoggerService;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using Service.Parameters;
using Shared.DTO.Options;
using WebApi.Extensions;

namespace WebApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            LogManager.Setup().LoadConfigurationFromFile();

            builder.Services.AddControllersWithFormatters()
                .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

            builder.Services.ConfigureCors()
                .ConfigureIISIntegration();

            builder.Services.ConfigureVersioning();

            builder.Services.AddServices();

            builder.Services.AddRepositoryContext(builder.Configuration)
                .AddRepositories();

            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            builder.Services.AddMediaTypes();

            builder.Services.AddResponseCaching();
            builder.Services.ConfigureHttpCacheHeaders();

            builder.Services.ConfigureRateLimiting();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthentication();
            builder.Services.ConfigureIdentity();
            builder.Services.ConfigureJwt(builder.Configuration);
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            ILoggerManager logger = app.Services.GetRequiredService<ILoggerManager>();
            app.ConfigureExceptionHandler(logger);

            if (app.Environment.IsProduction())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All,
            });

            app.UseRateLimiter();

            app.UseResponseCaching();
            app.UseHttpCacheHeaders();

            app.UseIpRateLimiting();

            app.UseCors(ServiceExtensions.CorsPolicy);

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
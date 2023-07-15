using Contracts.LoggerService;
using Contracts.Repository;
using LoggerService;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace WebApi.Extentions
{
    public static class ServiceExtentions
    {
        public const string CorsPolicy = "CorsPolicy";

        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy, builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }

        public static IServiceCollection ConfigureIISIntegration(this IServiceCollection services)
        {
            return services.Configure<IISOptions>(options =>
            {

            });
        }

        public static IServiceCollection AddLoggerService(this IServiceCollection services)
        {
            return services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped<IRepositoryManager, RepositoryManager>();
        }
    }
}

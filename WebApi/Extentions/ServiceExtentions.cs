using Contracts.LoggerService;
using Contracts.Repository;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Repository;
using Service;
using Service.Contracts;
using System.Net.NetworkInformation;
using WebApi.Formatters;

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

        public static IServiceCollection AddRepositoryContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSqlServer<RepositoryContext>(configuration.GetConnectionString("localhost"));
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<IServiceManager, ServiceManager>()
                .AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static IMvcBuilder AddControllersWithFormatters(this IServiceCollection services)
        {
            return services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
                config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
            }).AddXmlDataContractSerializerFormatters()
            .AddMvcOptions(config =>
            {
                config.OutputFormatters.Add(new CsvOutputFormatter());
            });

            NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
            {
                IServiceProvider serviceProvider = new ServiceCollection()
                    .AddLogging()
                    .AddMvc()
                    .AddNewtonsoftJson()
                    .Services.BuildServiceProvider();

                return serviceProvider
                    .GetRequiredService<IOptions<MvcOptions>>()
                    .Value
                    .InputFormatters
                    .OfType<NewtonsoftJsonPatchInputFormatter>()
                    .First();
            }
        }
    }
}

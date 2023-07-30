using Service.Contracts.DataShaping;
using Contracts.LoggerService;
using Contracts.Repository;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository;
using Service;
using Service.Contracts;
using Service.DataShaping;
using Shared.DTO.Company;
using Shared.DTO.Employee;
using WebApi.Formatters;
using Presentation.ActionFilters;
using Contracts.Hateoas;
using WebApi.Utility;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public const string CorsPolicy = "CorsPolicy";

        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy, builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("X-Pagination"));
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
                .AddSingleton<ILoggerManager, LoggerManager>()
                .AddDataShaper()
                .AddScoped<IEmployeeLinksGenerator, EmployeeLinksGenerator>()
                .AddScoped<ICompanyLinksGenerator, CompanyLinksGenerator>();
        }

        public static IMvcBuilder AddControllersWithFormatters(this IServiceCollection services)
        {
            return services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
                config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
                config.OutputFormatters.Add(new CsvOutputFormatter());
            })
            .AddXmlDataContractSerializerFormatters()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
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

        public static IServiceCollection AddMediaTypes(this IServiceCollection services)
        {
            return services.Configure((Action<MvcOptions>)(config =>
            {
                var mediaTypes = new string[]
                {
                    "application/vnd.codemaze.hateoas",
                    "application/vnd.codemaze.apiroot",
                };

                AddToJsonFormatter(config, mediaTypes);
                AddToXmlFormatter(config, mediaTypes);

                static void AddToJsonFormatter(MvcOptions config, IEnumerable<string> mediaTypes)
                {
                    var jsonFormatter = config.OutputFormatters.OfType<SystemTextJsonOutputFormatter>().FirstOrDefault();
                    if (jsonFormatter is not null)
                    {
                        foreach(var mediaType in mediaTypes)
                        {
                            jsonFormatter.SupportedMediaTypes.Add(mediaType + "+json");
                        }
                    }
                }

                static void AddToXmlFormatter(MvcOptions config, IEnumerable<string> mediaTypes)
                {
                    var xmlFormatter = config.OutputFormatters.OfType<XmlDataContractSerializerOutputFormatter>().FirstOrDefault();
                    if (xmlFormatter is not null)
                    {
                        foreach(var mediaType in mediaTypes)
                        {
                            xmlFormatter.SupportedMediaTypes.Add(mediaType + "+xml");
                        }
                    }
                }
            }));
        }

        public static IServiceCollection AddFilters(this IServiceCollection services)
        {
            return services.AddScoped<ValidateMediaTypeAttribute>();
        }

        public static IServiceCollection ConfigureVersioning(this IServiceCollection services)
        {
            return services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
        }

        public static IServiceCollection ConfigureHttpCacheHeaders(this IServiceCollection services)
        {
            return services.AddHttpCacheHeaders(
                expirationOpt =>
                {
                    expirationOpt.CacheLocation = Marvin.Cache.Headers.CacheLocation.Public;
                    expirationOpt.MaxAge = 120;
                },
                validationOpt =>
                {
                    validationOpt.MustRevalidate = true;
                });
        }

        public static IServiceCollection ConfigureRateLimiting(this IServiceCollection services)
        {
            services.AddMemoryCache();

            services.Configure<IpRateLimitOptions>(opt =>
            {
                opt.GeneralRules = new() { new RateLimitRule()
                {
                    Endpoint = "*",
                    Limit = 3,
                    Period = "5m",
                } };
            });

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

            return services;
        }

        private static IServiceCollection AddDataShaper(this IServiceCollection services)
        {
            services.AddScoped<IDataShaper, DataShaper>();
            DataShaper.AddType<GetEmployeeDto>();
            DataShaper.AddType<GetCompanyDto>();
            return services;
        }
    }
}

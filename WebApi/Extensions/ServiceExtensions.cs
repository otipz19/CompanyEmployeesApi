﻿using Service.Contracts.DataShaping;
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
                .AddSingleton<ILoggerManager, LoggerManager>();
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

        public static IServiceCollection AddDataShaper(this IServiceCollection services)
        {
            services.AddScoped<IDataShaper, DataShaper>();
            DataShaper.AddType<GetEmployeeDto>();
            DataShaper.AddType<GetCompanyDto>();
            return services;
        }

        public static IServiceCollection AddMediaTypes(this IServiceCollection services)
        {
            return services.Configure<MvcOptions>(config =>
            {
                const string mediaType = "application/vnd.codemaze.hateoas+";

                var jsonFormatter = config.OutputFormatters.OfType<SystemTextJsonOutputFormatter>().FirstOrDefault();
                if(jsonFormatter is not null)
                {
                    jsonFormatter.SupportedMediaTypes.Add(mediaType + "json");
                }

                var xmlFormatter = config.OutputFormatters.OfType<XmlDataContractSerializerOutputFormatter>().FirstOrDefault();
                if(xmlFormatter is not null)
                {
                    xmlFormatter.SupportedMediaTypes.Add(mediaType + "xml");
                }
            });
        }

        public static IServiceCollection AddFilters(this IServiceCollection services)
        {
            return services.AddScoped<ValidateMediaTypeActionFilter>();
        }
    }
}

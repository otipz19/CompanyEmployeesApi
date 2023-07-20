using Contracts.LoggerService;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using WebApi.Extensions;
using WebApi.Formatters;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            LogManager.Setup().LoadConfigurationFromFile();

            builder.Services.AddControllersWithFormatters()
                .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

            builder.Services.ConfigureCors()
                .ConfigureIISIntegration();

            builder.Services.AddServices()
                .AddFilters();

            builder.Services.AddRepositoryContext(builder.Configuration)
                .AddRepositories();

            builder.Services.AddAutoMapper(typeof(Program).Assembly);

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

            app.UseCors(ServiceExtensions.CorsPolicy);

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
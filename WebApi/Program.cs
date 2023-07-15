using Microsoft.AspNetCore.HttpOverrides;
using WebApi.Extentions;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.ConfigureCors()
                .ConfigureIISIntegration();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            if(app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All,
            });

            app.UseCors(ServiceExtentions.CorsPolicy);

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
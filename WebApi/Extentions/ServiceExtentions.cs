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
    }
}

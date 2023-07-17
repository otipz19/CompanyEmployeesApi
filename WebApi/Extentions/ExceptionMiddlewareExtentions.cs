using Contracts.LoggerService;
using Entities.ErrorModel;
using Microsoft.AspNetCore.Diagnostics;

namespace WebApi.Extentions
{
    public static class ExceptionMiddlewareExtentions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
        {
            app.UseExceptionHandler(app =>
            {
                app.Run(async context =>
                {
                    context.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    var handler = context.Features.Get<IExceptionHandlerFeature>();
                    if(handler != null)
                    {
                        Exception exception = handler.Error;

                        logger.LogError($"Something went wrong: {exception}");

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error",
                        }.ToString());
                    }
                });
            });
        }
    }
}

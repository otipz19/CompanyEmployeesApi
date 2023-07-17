using Contracts.LoggerService;
using Entities.ErrorModel;
using Entities.Exceptions;
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
                    context.Response.ContentType = "application/json";

                    var handler = context.Features.Get<IExceptionHandlerFeature>();
                    if(handler != null)
                    {
                        logger.LogError($"Something went wrong: {handler.Error}");

                        context.Response.StatusCode = handler.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            BadRequestException => StatusCodes.Status400BadRequest,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        var errorDetails = new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = context.Response.StatusCode == StatusCodes.Status500InternalServerError ?
                                "Internal Server Error" : handler.Error.Message,
                        };

                        await context.Response.WriteAsync(errorDetails.ToString());
                    }
                });
            });
        }
    }
}

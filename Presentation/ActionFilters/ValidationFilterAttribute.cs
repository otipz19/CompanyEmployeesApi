using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"];
            var action = context.RouteData.Values["action"];

            IEnumerable<KeyValuePair<string, object?>> dtoArguments = context.ActionArguments
                .Where(a => a.Key.ToLower().Contains("dto"));

            var nullArguments = new List<string>();

            foreach(var dtoArgument in dtoArguments)
            {
                if (dtoArgument.Value is null)
                {
                    nullArguments.Add(dtoArgument.Key);
                }
            }

            if (nullArguments.Any())
            {
                var responseBody = new
                {
                    Controller = controller!.ToString(),
                    Action = action!.ToString(),
                    Errors = nullArguments.Select(a => $"{a} argument must be not null").ToArray(),
                };

                context.Result = new BadRequestObjectResult(responseBody);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }  
    }
}

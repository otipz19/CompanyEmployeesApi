using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.DTO;
using Microsoft.Net.Http.Headers;

namespace Presentation.ActionFilters
{
    public class ValidateMediaTypeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Accept", out var acceptHeader))
            {
                context.Result = new BadRequestObjectResult("Accept header is missing");
                return;
            }

            string? mediaType = acceptHeader.FirstOrDefault();
            if(!MediaTypeHeaderValue.TryParse(mediaType, out var outMediaType))
            {
                context.Result = new BadRequestObjectResult("Unrecognized media type");
                return;
            }

            context.HttpContext.Items.Add(Constants.MediaTypeItemsKey, outMediaType);
        }
    }
}

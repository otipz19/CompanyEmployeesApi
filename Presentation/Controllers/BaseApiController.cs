using Entities.Responses.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Entities.ErrorModel;
using Microsoft.AspNetCore.Http;

namespace Presentation.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        public ActionResult ProcessError(ErrorApiResponse response)
        {
            return response switch
            {
                NotFoundApiResponse => NotFound(new ErrorDetails
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = response.Message,
                }),
                BadRequestApiResponse => BadRequest(new ErrorDetails
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = response.Message,
                }),
                _ => StatusCode(StatusCodes.Status500InternalServerError, "Unhandled error")
            };
        }
    }
}

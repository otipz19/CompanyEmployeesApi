using Entities.Responses.Abstractions;

namespace Entities.Responses.Company
{
    public class IdParametersBadRequest : BadRequestApiResponse
    {
        public IdParametersBadRequest() : base("Parameter ids is null")
        {
        }
    }
}

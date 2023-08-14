using Entities.Responses.Abstractions;

namespace Entities.Responses.Employee
{
    public class AgeRangeBadRequest : BadRequestApiResponse
    {
        public AgeRangeBadRequest() : base("Min age must be lower than max age")
        {
        }
    }
}

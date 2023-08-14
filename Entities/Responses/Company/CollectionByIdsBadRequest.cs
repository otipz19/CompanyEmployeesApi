using Entities.Responses.Abstractions;

namespace Entities.Responses.Company
{
    public class CollectionByIdsBadRequest : BadRequestApiResponse
    {
        public CollectionByIdsBadRequest() : base("Collection count dismatch comparing to ids")
        {
        }
    }
}

using Entities.Responses.Abstractions;

namespace Entities.Responses.Company
{
    public class CompaniesCollectionBadRequest : BadRequestApiResponse
    {
        public CompaniesCollectionBadRequest() : base("Companies collection is null")
        {
        }
    }
}

using Entities.Responses.Abstractions;

namespace Entities.Responses.Company
{
    public class CompanyNotFound : NotFoundApiResponse
    {
        public CompanyNotFound(Guid id) : base($"Company with id: {id} was not found")
        {
        }
    }
}

using Entities.Models;
using Shared.DTO.RequestFeatures;

namespace Repository.Extensions
{
    internal static class CompanyRepositoryExtentions
    {
        public static IQueryable<Company> SearchCompanies(this IQueryable<Company> query,
            CompanyRequestParameters requestParameters)
        {
            if (requestParameters.SearchTerm is null)
            {
                return query;
            }

            string searchTerm = requestParameters.SearchTerm.ToLower().Trim();

            return query.Where(c => c.Name.ToLower().Contains(searchTerm)
                || c.Address.ToLower().Contains(searchTerm)
                || c.Country.ToLower().Contains(searchTerm));
        }
    }
}

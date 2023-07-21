using Entities.Models;
using Repository.Ordering;
using Shared.DTO.RequestFeatures;
using System.Linq.Dynamic.Core;

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

        public static IQueryable<Company> OrderCompanies(this IQueryable<Company> query,
            CompanyRequestParameters requestParameters)
        {
            if (string.IsNullOrWhiteSpace(requestParameters.OrderBy))
            {
                return query.OrderBy(c => c.Name);
            }

            string? orderQuery = OrderQueryBuilder<Company>.Build(requestParameters);

            if(string.IsNullOrEmpty(orderQuery))
            {
                return query.OrderBy(c => c.Name);
            }

            return query.OrderBy(orderQuery);
        }
    }
}

using Entities.Models;
using Shared.DTO.RequestFeatures;

namespace Repository.Extensions
{
    internal static class EmployeeRepositoryExtensions
    {
        public static IQueryable<Employee> FilterEmployees(this IQueryable<Employee> query,
            EmployeeRequestParameters requestParameters)
        {
            return query.Where(e => e.Age >= requestParameters.MinAge && e.Age <= requestParameters.MaxAge);
        }

        public static IQueryable<Employee> SearchEmployees(this IQueryable<Employee> query,
            EmployeeRequestParameters requestParameters)
        {
            if (requestParameters.SearchTerm is null)
            {
                return query;
            }

            string searchTerm = requestParameters.SearchTerm.ToLower().Trim();

            return query.Where(e => e.Name.ToLower().Contains(searchTerm) || e.Position.ToLower().Contains(searchTerm));
        }
    }
}

using Entities.Models;
using Repository.Ordering;
using Shared.DTO.RequestFeatures;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

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

        public static IQueryable<Employee> OrderEmployees(this IQueryable<Employee> query,
            EmployeeRequestParameters requestParameters)
        {
            if (string.IsNullOrWhiteSpace(requestParameters.OrderBy))
            {
                return query.OrderBy(e => e.Name);
            }

            string? orderQuery = OrderQueryBuilder<Employee>.Build(requestParameters);

            if(string.IsNullOrWhiteSpace(orderQuery))
            {
                return query.OrderBy(e => e.Name);
            }

            return query.OrderBy(orderQuery);
        }
    }
}

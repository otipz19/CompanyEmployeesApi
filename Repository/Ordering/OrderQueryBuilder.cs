using Entities.Models;
using Shared.DTO.RequestFeatures;
using System.Reflection;
using System.Text;

namespace Repository.Ordering
{
    public static class OrderQueryBuilder<T>
        where T : class
    {
        public static string? Build(BaseRequestParameters requestParameters)
        {
            string[] orderParams = requestParameters.OrderBy.Split(',', StringSplitOptions.RemoveEmptyEntries);

            PropertyInfo[] properties = typeof(T).GetProperties();

            StringBuilder orderQueryBuilder = new();

            foreach (string param in orderParams)
            {
                string propertyNameFromParam = param.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];

                PropertyInfo? property = properties
                    .FirstOrDefault(p => p.Name.Equals(propertyNameFromParam, StringComparison.OrdinalIgnoreCase));

                if (property != null)
                {
                    string orderDirection = param.EndsWith("desc") ? "descending" : "ascending";
                    orderQueryBuilder.Append($"{property.Name} {orderDirection}, ");
                }
            }

            string? orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            return orderQuery;
        }
    }
}

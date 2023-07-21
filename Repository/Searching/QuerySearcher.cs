using Shared.DTO.RequestFeatures;
using System.Linq.Expressions;
using System.Reflection;

namespace Repository.Searching
{
    internal class QuerySearcher<T>
        where T : class
    {
        private List<Func<T, string>> propertiesToSearch = new();

        public QuerySearcher<T> ByProperty(Func<T, string> propertieSelector)
        {
            propertiesToSearch.Add(propertieSelector);
            return this;
        }

        public IQueryable<T> Search(IQueryable<T> query, BaseRequestParameters requestParameters)
        {
            if(requestParameters.SearchTerm is null)
            {
                return query;
            }

            string searchTerm = requestParameters.SearchTerm.Trim().ToLower();

            foreach(var property in propertiesToSearch)
            {
                //query = query.Where(e => property(e).ToLower().Contains(searchTerm));
                query = query.Where(BuildExpression(property));
            }
          
            propertiesToSearch.Clear();
            return query;
        }

        private Expression<Func<T, bool>> BuildExpression(Func<T, string> propertySelector)
        {
            var eParameter = Expression.Parameter(typeof(T), "e");

            var propertySelectorMethod = propertySelector.GetMethodInfo();
            var propertyGet = Expression.Call(propertySelectorMethod, eParameter);

            var toLowerMethod = typeof(string).GetMethod("ToLower")!;
            var toLower = Expression.Call(toLowerMethod, propertyGet);

            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
            var contains = Expression.Call(containsMethod, toLower);

            var lambda = Expression.Lambda<Func<T, bool>>(contains, eParameter);
            return lambda;
        }
    }
}

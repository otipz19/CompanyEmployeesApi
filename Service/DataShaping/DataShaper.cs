using Contracts.DataShaping;
using System.Dynamic;
using System.Reflection;

namespace Service.DataShaping
{
    public class DataShaper
    {
        private static readonly Dictionary<Type, PropertyInfo[]> _propertiesOfRegisteredTypes = new();

        public static void AddType<T>()
        {
            AddType(typeof(T));
        }

        public static void AddType(Type type)
        {
            _propertiesOfRegisteredTypes.Add(type, type.GetProperties());
        }

        public IEnumerable<ExpandoObject> ShapeData<T>(IEnumerable<T> items, string fieldsString)
        {
            var properties = GetRequiredProperties(fieldsString, typeof(T));
            return FetchData(items, properties);
        }

        public ExpandoObject ShapeData<T>(T item, string fieldsString)
        {
            var properties = GetRequiredProperties(fieldsString, typeof(T));
            return FetchData(item, properties);
        }

        /// <param name="fieldsString">Fields parameter from URI query</param>
        /// <returns>
        /// Collection of properties of T type, that match to fields specified in query.
        /// If query string is incorrect, returns collection of all properties of T type.
        /// </returns>
        private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString, Type type)
        {
            PropertyInfo[] allProperties = _propertiesOfRegisteredTypes[type];

            if (string.IsNullOrWhiteSpace(fieldsString))
            {
                return allProperties;
            }

            string[] parsedFields = fieldsString
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim()).ToArray();
            if (!parsedFields.Any())
            {
                return allProperties;
            }

            List<PropertyInfo> requiredProperties = new();
            foreach(var field in parsedFields)
            {
                PropertyInfo? requiredProperty = allProperties
                    .FirstOrDefault(p => p.Name.Equals(field, StringComparison.OrdinalIgnoreCase));
                if(requiredProperty != null)
                {
                    requiredProperties.Add(requiredProperty);
                }
            }

            return requiredProperties;
        }

        private IEnumerable<ExpandoObject> FetchData<T>(IEnumerable<T> items, IEnumerable<PropertyInfo> properties)
        {
            return items.Select(i => FetchData(i, properties)).ToArray();
        }

        private ExpandoObject FetchData<T>(T item, IEnumerable<PropertyInfo> properties)
        {
            var expandoObject = new ExpandoObject();

            foreach(var property in properties)
            {
                var value = property.GetValue(item);
                expandoObject.TryAdd(property.Name, value);
            }

            return expandoObject;
        }
    }
}

using Service.Contracts.DataShaping;
using Shared.DTO.Expando;
using System.Reflection;

namespace Service.DataShaping
{
    public class DataShaper : IDataShaper
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

        public IEnumerable<IShapedObject> ShapeData<T>(IEnumerable<T> items, string? fieldsString)
        {
            var properties = GetRequiredProperties(fieldsString, typeof(T));
            return FetchData(items, properties);
        }

        public IShapedObject ShapeData<T>(T item, string? fieldsString)
        {
            var properties = GetRequiredProperties(fieldsString, typeof(T));
            return FetchData(item, properties);
        }

        /// <param name="fieldsString">Fields parameter from URI query</param>
        /// <returns>
        /// Collection of properties of T type, that match to fields specified in query.
        /// If query string is incorrect, returns collection of all properties of T type.
        /// </returns>
        private IEnumerable<PropertyInfo> GetRequiredProperties(string? fieldsString, Type type)
        {
            if (!_propertiesOfRegisteredTypes.ContainsKey(type))
            {
                AddType(type);
            }

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

        private IEnumerable<ShapedObject> FetchData<T>(IEnumerable<T> items, IEnumerable<PropertyInfo> properties)
        {
            return items.Select(i => FetchData(i, properties)).ToArray();
        }

        private ShapedObject FetchData<T>(T item, IEnumerable<PropertyInfo> properties)
        {
            var shapedObject = new ShapedObject();

            foreach(var property in properties)
            {
                var value = property.GetValue(item);
                shapedObject.TryAdd(property.Name, value);
            }

            return shapedObject;
        }
    }
}

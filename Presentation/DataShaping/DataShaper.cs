using Shared.DTO.RequestFeatures;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace Presentation.DataShaping
{
    internal static class DataShaper<T>
        where T : class
    {
        public static IEnumerable<object> Shape(IEnumerable<T> items, BaseRequestParameters requestParameters)
        {
            //Check if fields query parameter is provided or items is not empty
            if (string.IsNullOrWhiteSpace(requestParameters.Fields) || !items.Any())
            {
                return items;
            }

            //Parse fields query parameter
            string[] parsedFields = requestParameters.Fields.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (!parsedFields.Any())
            {
                return items;
            }

            //Find properties of resource that match to fields from query parameter
            PropertyInfo[] propertiesOfT = typeof(T).GetProperties();
            List<PropertyInfo> propertiesToShape = new();
            foreach(string parsedField in parsedFields)
            {
                PropertyInfo? matchingProperty = propertiesOfT
                    .FirstOrDefault(p => p.Name.Equals(parsedField, StringComparison.OrdinalIgnoreCase));
                if(matchingProperty != null)
                {
                    propertiesToShape.Add(matchingProperty);
                }
            }

            //Build dynamic type with System.Linq.Dynamic.Core
            List<DynamicProperty> dynamicProperties = new();
            foreach(PropertyInfo prop in propertiesToShape)
            {
                dynamicProperties.Add(new DynamicProperty(prop.Name, prop.PropertyType));
            }
            Type dynamicType = DynamicClassFactory.CreateType(dynamicProperties);

            //Create array of dynamic type
            var dynamicClasses = new DynamicClass[items.Count()];
            for (int i = 0; i < dynamicClasses.Length; i++)
            {
                dynamicClasses[i] = (Activator.CreateInstance(dynamicType) as DynamicClass)!;
            }

            //Set properties' values of dynamic array
            List<T> itemsList = items.ToList();
            for(int i = 0; i < dynamicClasses.Length; i++)
            {
                foreach(var prop in propertiesToShape)
                {
                    dynamicClasses[i].SetDynamicPropertyValue(prop.Name, prop.GetValue(itemsList[i])!);
                }
            }

            return dynamicClasses;
        }
    }
}

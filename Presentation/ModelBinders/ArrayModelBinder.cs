using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.Reflection;

namespace Presentation.ModelBinders
{
    public class ArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            //Check if parameter type is IEnumerable
            if (!bindingContext.ModelMetadata.IsEnumerableType)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            //Extract value from request
            string providedValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();
            if (providedValue is null)
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            //Get type of IEnumerable<T> element
            Type genericType = bindingContext.ModelType.GetTypeInfo().GetGenericArguments()[0];
            
            //Get converter to element's type
            TypeConverter converter = TypeDescriptor.GetConverter(genericType);

            //Convert string from request to objects array
            object?[] objectArray = providedValue.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => converter.ConvertFromString(s))
                .ToArray();

            //Convert objects array to array of element's type 
            Array? resultArray = Array.CreateInstance(genericType, objectArray.Length);
            objectArray.CopyTo(resultArray, 0);

            bindingContext.Model = resultArray;
            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model); 
            return Task.CompletedTask;
        }
    }
}

using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Shared.DTO.Company;
using System.Collections;
using System.Reflection;
using System.Text;

namespace WebApi.Formatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            //StringBuilder builder = new StringBuilder();
            //if (context.Object is CompanyDto company)
            //{
            //    WriteCsv(builder, company);
            //}
            //else if (context.Object is IEnumerable<CompanyDto> companies)
            //{
            //    foreach(var c in companies)
            //    {
            //        WriteCsv(builder, c);
            //    }
            //}

            //await context.HttpContext.Response.WriteAsync(builder.ToString());

            

            if(context.Object is not null)
            {
                Type objectType = context.Object.GetType();
                Type? elementType = null;
                bool isCollection = false;

                foreach(var it in objectType.GetInterfaces())
                {
                    if(it.IsGenericType && it.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    {
                        elementType = it.GetGenericArguments()[0];
                        isCollection = true;
                        break;
                    }
                }

                StringBuilder builder = new StringBuilder();

                if (!isCollection)
                {
                    elementType = objectType;
                    PropertyInfo[] propertyInfos = elementType.GetProperties();
                    WriteCsvByPropertyInfos(builder, propertyInfos, context.Object);
                }
                else
                {
                    PropertyInfo[] propertyInfos = elementType!.GetProperties();
                    foreach(var item in (IEnumerable)context.Object)
                    {
                        WriteCsvByPropertyInfos(builder, propertyInfos, item);
                    }
                }
                
                await context.HttpContext.Response.WriteAsync(builder.ToString());
            }
        }

        //protected override bool CanWriteType(Type? type)
        //{
        //    if (typeof(CompanyDto).IsAssignableFrom(type) || typeof(IEnumerable<CompanyDto>).IsAssignableFrom(type))
        //    {
        //        return base.CanWriteType(type);
        //    }

        //    return false;
        //}

        private void WriteCsv(StringBuilder builder, GetCompanyDto company)
        {
            builder.AppendLine($"{company.Id}, '{company.Name}', '{company.FullAddress}'");
        }

        private void WriteCsvByPropertyInfos(StringBuilder builder, PropertyInfo[] propertyInfos, object obj)
        {
            foreach(var propertyInfo in propertyInfos)
            {
                builder.Append($"{propertyInfo.GetValue(obj)}, ");
            }
            builder.AppendLine();
        }
    }
}

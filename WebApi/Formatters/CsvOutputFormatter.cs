using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Shared.DTO;
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
            StringBuilder builder = new StringBuilder();
            if (context.Object is CompanyDto company)
            {
                WriteCsv(builder, company);
            }
            else if (context.Object is IEnumerable<CompanyDto> companies)
            {
                foreach(var c in companies)
                {
                    WriteCsv(builder, c);
                }
            }

            await context.HttpContext.Response.WriteAsync(builder.ToString());
        }

        protected override bool CanWriteType(Type? type)
        {
            if (typeof(CompanyDto).IsAssignableFrom(type) || typeof(IEnumerable<CompanyDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }

        private void WriteCsv(StringBuilder builder, CompanyDto company)
        {
            builder.AppendLine($"{company.Id}, '{company.Name}', '{company.FullAddress}'");
        }
    }
}

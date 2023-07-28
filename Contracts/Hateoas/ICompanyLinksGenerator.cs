using Entities.LinkModels;
using Microsoft.AspNetCore.Http;
using Entities.DataShaping;

namespace Contracts.Hateoas
{
    public interface ICompanyLinksGenerator
    {
        public LinkResponse GenerateLinks(IEnumerable<ShapedObject> shapedCompanies, string? fields, HttpContext context);
    }
}

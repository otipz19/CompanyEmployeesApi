using Entities.LinkModels;
using Microsoft.AspNetCore.Http;
using Entities.DataShaping;

namespace Contracts.Hateoas
{
    public interface IEmployeeLinksGenerator
    {
        public LinkResponse GenerateLinks(IEnumerable<ShapedObject> shapedEmployees, string? fields,
            Guid companyId, HttpContext context);
    }
}

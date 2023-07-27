using Entities.LinkModels;
using Shared.DTO.Employee;
using Microsoft.AspNetCore.Http;

namespace Contracts.Hateoas
{
    public interface IEmployeeLinksGenerator
    {
        public LinkResponse GenerateLinks(IEnumerable<GetEmployeeDto> employeeDtos, string fields,
            Guid companyId, HttpContext context);
    }
}

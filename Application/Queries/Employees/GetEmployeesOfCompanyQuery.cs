using Entities.LinkModels;
using MediatR;
using Shared.DTO.Employee;
using Shared.DTO.RequestFeatures.Paging;

namespace Application.Queries.Employees
{
    public sealed record GetEmployeesOfCompanyQuery(Guid CompanyId, LinkEmployeesParameters LinkRequestParameters)
        : IRequest<(LinkResponse response, PagingMetaData metaData)>
    {
    }
}

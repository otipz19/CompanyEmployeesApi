using Entities.Models;
using MediatR;
using Shared.DTO.Employee;

namespace Application.Queries.Employees
{
    public sealed record GetEmployeeForPatchQuery(Guid CompanyId, Guid EmployeeId)
        : IRequest<(UpdateEmployeeDto dto, Employee entity)>
    {
    }
}

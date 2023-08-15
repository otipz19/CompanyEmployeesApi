using MediatR;
using Shared.DTO.Employee;

namespace Application.Queries.Employees
{
    public sealed record GetEmployeeQuery(Guid CompanyId, Guid EmployeeId)
        : IRequest<GetEmployeeDto>
    {
    }
}

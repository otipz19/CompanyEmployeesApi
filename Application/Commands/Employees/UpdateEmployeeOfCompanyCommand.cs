using MediatR;
using Shared.DTO.Employee;

namespace Application.Commands.Employees
{
    public sealed record UpdateEmployeeOfCompanyCommand(Guid CompanyId, Guid EmployeeId, UpdateEmployeeDto Dto)
        : IRequest
    {
    }
}

using MediatR;
using Shared.DTO.Employee;

namespace Application.Commands.Employees
{
    public sealed record UpdateEmployeeCommand(Guid CompanyId, Guid EmployeeId, UpdateEmployeeDto Dto)
        : IRequest
    {
    }
}

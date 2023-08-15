using MediatR;
using Shared.DTO.Employee;

namespace Application.Commands.Employees
{
    public sealed record CreateEmployeeCommand(Guid CompanyId, CreateEmployeeDto Dto)
        : IRequest<GetEmployeeDto>
    {
    }
}

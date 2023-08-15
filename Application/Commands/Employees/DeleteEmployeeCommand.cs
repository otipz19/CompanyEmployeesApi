using MediatR;

namespace Application.Commands.Employees
{
    public sealed record DeleteEmployeeCommand(Guid CompanyId, Guid EmployeeId)
        : IRequest
    {
    }
}

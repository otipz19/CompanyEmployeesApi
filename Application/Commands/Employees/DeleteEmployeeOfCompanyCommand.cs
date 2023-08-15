using MediatR;

namespace Application.Commands.Employees
{
    public sealed record DeleteEmployeeOfCompanyCommand(Guid CompanyId, Guid EmployeeId)
        : IRequest
    {
    }
}

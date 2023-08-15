using MediatR;
using Shared.DTO.Employee;

namespace Application.Queries.Employees
{
    public sealed record GetEmployeeOfCompanyQuery(Guid CompanyId, Guid EmployeeId)
        : IRequest<GetEmployeeDto>
    {
    }
}

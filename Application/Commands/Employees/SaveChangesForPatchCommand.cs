using Entities.Models;
using MediatR;
using Shared.DTO.Employee;

namespace Application.Commands.Employees
{
    public sealed record SaveChangesForPatchCommand(UpdateEmployeeDto Dto, Employee Entity)
        : IRequest
    {
    }
}

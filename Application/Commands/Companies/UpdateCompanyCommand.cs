using MediatR;
using Shared.DTO.Company;

namespace Application.Commands.Companies
{
    public sealed record UpdateCompanyCommand(Guid Id, UpdateCompanyDto Dto) : IRequest
    {
    }
}

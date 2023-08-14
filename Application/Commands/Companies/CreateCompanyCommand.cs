using MediatR;
using Shared.DTO.Company;

namespace Application.Commands.Company
{
    public sealed record CreateCompanyCommand(CreateCompanyDto Dto) : IRequest<GetCompanyDto>
    {
    }
}

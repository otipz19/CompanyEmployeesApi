using MediatR;
using Shared.DTO.Company;

namespace Application.Commands.Companies
{
    public sealed record CreateCompaniesCollectionCommand(IEnumerable<CreateCompanyDto> Dtos)
        : IRequest<IEnumerable<GetCompanyDto>>
    {
    }
}

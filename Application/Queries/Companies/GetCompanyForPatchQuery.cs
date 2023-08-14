using MediatR;
using Shared.DTO.Company;

namespace Application.Queries.Companies
{
    public sealed record GetCompanyForPatchQuery(Guid Id) : IRequest<(UpdateCompanyDto dto, Entities.Models.Company entity)>
    {
    }
}

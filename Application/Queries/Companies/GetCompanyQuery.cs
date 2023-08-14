using MediatR;
using Shared.DTO.Company;

namespace Application.Queries.Companies
{
    public sealed record GetCompanyQuery(Guid Id) : IRequest<GetCompanyDto>
    {
    }
}

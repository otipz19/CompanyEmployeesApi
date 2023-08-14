using MediatR;
using Shared.DTO.Company;

namespace Application.Queries.Company
{
    public sealed record GetCompanyQuery(Guid Id) : IRequest<GetCompanyDto>
    {
    }
}

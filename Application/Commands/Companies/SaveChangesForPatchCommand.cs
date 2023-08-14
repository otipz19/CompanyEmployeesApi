using MediatR;
using Shared.DTO.Company;

namespace Application.Commands.Companies
{
    public sealed record SaveChangesForPatchCommand(UpdateCompanyDto Dto, Entities.Models.Company Entity) : IRequest
    {
    }
}

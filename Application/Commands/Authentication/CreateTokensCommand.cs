using Entities.Models;
using MediatR;
using Shared.DTO.Authentication;

namespace Application.Commands.Authentication
{
    public sealed record CreateTokensCommand(User User)
        : IRequest<TokensDto>
    {
    }
}

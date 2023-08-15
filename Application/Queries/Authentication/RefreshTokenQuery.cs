using MediatR;
using Shared.DTO.Authentication;

namespace Application.Queries.Authentication
{
    public sealed record RefreshTokenQuery(TokensDto Tokens)
        : IRequest<TokensDto>
    {
    }
}

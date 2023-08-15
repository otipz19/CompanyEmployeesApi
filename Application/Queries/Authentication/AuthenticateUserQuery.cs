using Entities.Models;
using MediatR;
using Shared.DTO.Authentication;

namespace Application.Queries.Authentication
{
    public sealed record AuthenticateUserQuery(AuthenticateUserDto Dto)
        : IRequest<(bool isSuccess, User? user)>
    {
    }
}

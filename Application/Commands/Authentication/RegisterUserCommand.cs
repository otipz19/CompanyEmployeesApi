using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.DTO.Authentication;

namespace Application.Commands.Authentication
{
    public sealed record RegisterUserCommand(RegisterUserDto Dto)
        : IRequest<IdentityResult>
    {
    }
}

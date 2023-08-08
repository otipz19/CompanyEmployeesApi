using Microsoft.AspNetCore.Identity;
using Shared.DTO.Authentication;

namespace Service.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(RegisterUserDto dto);
    }
}
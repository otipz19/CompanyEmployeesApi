using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Shared.DTO.Authentication;

namespace Service.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(RegisterUserDto dto);

        Task<(bool isSuccess, User? user)> AuthenticateUser(AuthenticateUserDto dto);

        Task<TokensDto> CreateTokens(User user);

        Task<TokensDto> RefreshToken(TokensDto tokens);
    }
}
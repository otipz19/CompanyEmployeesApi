using Application.Commands.Authentication;
using Application.Handlers.Base.Authentication;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Shared.DTO.Authentication;
using Shared.DTO.Options;
using System.Security.Cryptography;

namespace Application.Handlers.CommandHandlers.Authentication
{
    internal sealed class CreateTokensHandler
        : BaseTokensHandler, IRequestHandler<CreateTokensCommand, TokensDto>
    {
        private readonly UserManager<User> _userManager;

        public CreateTokensHandler(UserManager<User> userManager, IOptions<JwtSettings> jwtOptions)
            : base(jwtOptions, userManager)
        {
            _userManager = userManager;
        }

        public async Task<TokensDto> Handle(CreateTokensCommand request, CancellationToken cancellationToken)
        {
            string accessToken = await CreateAccessToken(request.User);

            string refreshToken = GenerateRefreshToken();
            request.User.RefreshToken = refreshToken;
            request.User.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(request.User);

            return new TokensDto(accessToken, refreshToken);
        }

        private string GenerateRefreshToken()
        {
            var randomNum = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNum);
            return Convert.ToBase64String(randomNum);
        }
    }
}

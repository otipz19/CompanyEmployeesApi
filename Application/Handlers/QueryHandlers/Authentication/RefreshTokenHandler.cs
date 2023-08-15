using Application.Handlers.Base.Authentication;
using Application.JwtParameters;
using Application.Queries.Authentication;
using Entities.Exceptions;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.DTO.Authentication;
using Shared.DTO.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Handlers.QueryHandlers.Authentication
{
    internal sealed class RefreshTokenHandler
        : BaseTokensHandler, IRequestHandler<RefreshTokenQuery, TokensDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;

        public RefreshTokenHandler(UserManager<User> userManager, IOptions<JwtSettings> jwtOptions)
            : base(jwtOptions, userManager)
        {
            _userManager = userManager;
            _jwtSettings = jwtOptions.Value;
        }

        public async Task<TokensDto> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            ClaimsPrincipal principal = GetClaimsPrincipalFromExpiredToken(request.Tokens.AccessToken);

            if (principal.Identity is null || principal.Identity.Name is null)
            {
                throw new RefreshTokenBadRequestException();
            }

            User? user = await _userManager.FindByNameAsync(principal.Identity.Name);

            if (user is null || user.RefreshToken != request.Tokens.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new RefreshTokenBadRequestException();
            }

            return new TokensDto(await CreateAccessToken(user), request.Tokens.RefreshToken);
        }

        private ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token)
        {
            var validationParameters = new DefaultTokenValidationParameters(_jwtSettings);

            var tokenHadler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            ClaimsPrincipal principal;
            try
            {
                principal = tokenHadler.ValidateToken(token, validationParameters, out securityToken);
            }
            catch
            {
                throw new SecurityTokenBadRequestException();
            }

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg
                .Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase))
            {
                throw new SecurityTokenBadRequestException();
            }

            return principal;
        }
    }
}

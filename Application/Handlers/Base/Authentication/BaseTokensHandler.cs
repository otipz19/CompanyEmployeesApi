using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.DTO.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Handlers.Base.Authentication
{
    internal abstract class BaseTokensHandler
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;

        protected BaseTokensHandler(IOptions<JwtSettings> jwtOptions, UserManager<User> userManager)
        {
            _userManager = userManager;
            _jwtSettings = jwtOptions.Value;
        }

        protected async Task<string> CreateAccessToken(User user)
        {
            SigningCredentials credentials = GetSigningCredentials();
            IEnumerable<Claim> claims = await GetClaims(user);
            JwtSecurityToken token = GenerateToken(credentials, claims);
            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return accessToken;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            return new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        }

        private async Task<IEnumerable<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName!),
            };

            IEnumerable<string> roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateToken(SigningCredentials credentials, IEnumerable<Claim> claims)
        {
            DateTime expires = DateTime.Now.AddMinutes(_jwtSettings.ExpiresInMinutes);
            return new JwtSecurityToken(
                signingCredentials: credentials,
                claims: claims,
                expires: expires,
                issuer: _jwtSettings.ValidIssuer,
                audience: _jwtSettings.ValidAudience);
        }
    }
}

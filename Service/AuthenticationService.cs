using AutoMapper;
using Contracts.LoggerService;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Service.Parameters;
using Shared.DTO.Authentication;
using Shared.DTO.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(
            UserManager<User> userManager,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager,
            ILoggerManager logger,
            IOptions<JwtSettings> jwtOptions)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _logger = logger;
            _jwtSettings = jwtOptions.Value;
        }

        public async Task<IdentityResult> RegisterUser(RegisterUserDto dto)
        {
            User user = _mapper.Map<User>(dto);

            IdentityResult result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                foreach (string role in dto.Roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        throw new RoleDoesNotExistsException(role);
                    }
                }

                await _userManager.AddToRolesAsync(user, dto.Roles);
            }

            return result;
        }

        public async Task<(bool isSuccess, User? user)> AuthenticateUser(AuthenticateUserDto dto)
        {
            User? user = await _userManager.FindByNameAsync(dto.UserName);

            bool result = user is not null && await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!result)
            {
                _logger.LogWarn($"Authentication failed for {dto.UserName}");
            }

            return (result, user);
        }

        public async Task<TokensDto> CreateToken(User user)
        {
            SigningCredentials credentials = GetSigningCredentials();
            IEnumerable<Claim> claims = await GetClaims(user);
            JwtSecurityToken token = GenerateToken(credentials, claims);
            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            string refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(user);

            return new TokensDto(accessToken, refreshToken);
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

        private string GenerateRefreshToken()
        {
            var randomNum = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNum);
            return Convert.ToBase64String(randomNum);
        }

        private ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token)
        {
            var validationParameters = new DefaultTokenValidationParameters(_jwtSettings);

            var tokenHadler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            ClaimsPrincipal principal = tokenHadler.ValidateToken(token, validationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg
                .Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}

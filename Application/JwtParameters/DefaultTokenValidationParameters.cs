using Microsoft.IdentityModel.Tokens;
using Shared.DTO.Options;
using System.Text;

namespace Application.JwtParameters
{
    public class DefaultTokenValidationParameters : TokenValidationParameters
    {
        public DefaultTokenValidationParameters(JwtSettings jwtSettings)
        {
            ValidateIssuer = true;
            ValidateAudience = true;
            ValidateLifetime = true;
            ValidateIssuerSigningKey = true;
            ValidIssuer = jwtSettings.ValidIssuer;
            ValidAudience = jwtSettings.ValidAudience;
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
        }
    }
}

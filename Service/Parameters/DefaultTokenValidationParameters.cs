using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.DTO.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Parameters
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

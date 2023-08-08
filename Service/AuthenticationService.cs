using AutoMapper;
using Contracts.LoggerService;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using Shared.DTO.Authentication;

namespace Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AuthenticationService(
            UserManager<User> userManager,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> RegisterUser(RegisterUserDto dto)
        {
            User user = _mapper.Map<User>(dto);

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                foreach(string role in dto.Roles)
                {
                    if(! await _roleManager.RoleExistsAsync(role))
                    {
                        throw new RoleDoesNotExistsException(role);
                    }
                }

                await _userManager.AddToRolesAsync(user, dto.Roles);
            }

            return result;
        }
    }
}

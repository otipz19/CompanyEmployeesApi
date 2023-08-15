using Application.Commands.Authentication;
using AutoMapper;
using Entities.Exceptions;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Handlers.CommandHandlers.Authentication
{
    internal sealed class RegisterUserHandler
        : IRequestHandler<RegisterUserCommand, IdentityResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RegisterUserHandler(IMapper mapper, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(request.Dto);

            IdentityResult result = await _userManager.CreateAsync(user, request.Dto.Password);

            if (result.Succeeded)
            {
                foreach (string role in request.Dto.Roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _userManager.DeleteAsync(user);
                        throw new RoleDoesNotExistsException(role);
                    }
                }

                await _userManager.AddToRolesAsync(user, request.Dto.Roles);
            }

            return result;
        }
    }
}

using Application.Queries.Authentication;
using Contracts.LoggerService;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Handlers.QueryHandlers.Authentication
{
    internal sealed class AuthenticateUserHandler
        : IRequestHandler<AuthenticateUserQuery, (bool isSuccess, User? user)>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILoggerManager _logger;

        public AuthenticateUserHandler(ILoggerManager logger, UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<(bool isSuccess, User? user)> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByNameAsync(request.Dto.UserName);

            bool result = user is not null && await _userManager.CheckPasswordAsync(user, request.Dto.Password);

            if (!result)
            {
                _logger.LogWarn($"Authentication failed for {request.Dto.UserName}");
            }

            return (result, user);
        }
    }
}

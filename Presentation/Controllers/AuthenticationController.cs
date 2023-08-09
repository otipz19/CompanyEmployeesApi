using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Service.Contracts;
using Shared.DTO.Authentication;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AuthenticationController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        [ValidateArguments]
        public async Task<ActionResult> RegisterUser(RegisterUserDto dto)
        {
            IdentityResult result = await _serviceManager.AuthenticationService.RegisterUser(dto);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("login")]
        [ValidateArguments]
        public async Task<ActionResult> AuthenticateUser(AuthenticateUserDto dto)
        {
            var result = await _serviceManager.AuthenticationService.AuthenticateUser(dto);

            if (!result.isSuccess)
            {
                return Unauthorized();
            }

            TokensDto tokens = await _serviceManager.AuthenticationService.CreateToken(result.user!);
            return Ok(tokens);
        }
    }
}

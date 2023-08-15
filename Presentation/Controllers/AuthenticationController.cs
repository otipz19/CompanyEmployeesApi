using Application.Commands.Authentication;
using Application.Queries.Authentication;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Shared.DTO.Authentication;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthenticationController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ValidateArguments]
        public async Task<ActionResult> RegisterUser(RegisterUserDto dto)
        {
            var command = new RegisterUserCommand(dto);
            IdentityResult result = await _sender.Send(command);

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
            var query = new AuthenticateUserQuery(dto);
            var result = await _sender.Send(query);

            if (!result.isSuccess)
            {
                return Unauthorized();
            }

            var command = new CreateTokensCommand(result.user);
            TokensDto tokens = await _sender.Send(command);
            return Ok(tokens);
        }
    }
}

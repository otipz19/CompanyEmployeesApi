using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Service.Contracts;
using Shared.DTO.Authentication;

namespace Presentation.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public TokenController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        [ValidateArguments]
        public async Task<ActionResult> RefreshToken(TokensDto dto)
        {
            TokensDto refreshedTokens = await _serviceManager.AuthenticationService.RefreshToken(dto);

            return Ok(refreshedTokens);
        }
    }
}

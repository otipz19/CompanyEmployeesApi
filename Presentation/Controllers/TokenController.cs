using Application.Queries.Authentication;
using MediatR;
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
        private readonly ISender _sender;

        public TokenController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ValidateArguments]
        public async Task<ActionResult> RefreshToken(TokensDto dto)
        {
            var query = new RefreshTokenQuery(dto);
            TokensDto refreshedTokens = await _sender.Send(query);

            return Ok(refreshedTokens);
        }
    }
}

using Entities.Responses.Abstractions;

namespace Entities.Responses.Authentication
{
    public class RefreshTokenBadRequest : BadRequestApiResponse
    {
        public RefreshTokenBadRequest() : base("Invalid values into refresh tokens")
        {
        }
    }
}

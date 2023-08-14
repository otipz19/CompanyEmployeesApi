using Entities.Responses.Abstractions;

namespace Entities.Responses.Authentication
{
    public class SecurityTokenBadRequest : BadRequestApiResponse
    {
        public SecurityTokenBadRequest() : base("Invalid access token")
        {
        }
    }
}

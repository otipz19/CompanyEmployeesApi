using Entities.Responses.Abstractions;

namespace Entities.Responses.Authentication
{
    public class RoleDoesNotExistsBadRequest : BadRequestApiResponse
    {
        public RoleDoesNotExistsBadRequest(string role) : base($"Role {role} does not exists")
        {
        }
    }
}

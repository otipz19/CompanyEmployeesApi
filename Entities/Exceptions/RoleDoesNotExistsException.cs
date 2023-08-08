namespace Entities.Exceptions
{
    public class RoleDoesNotExistsException : BadRequestException
    {
        public RoleDoesNotExistsException(string role) : base($"Role {role} does not exists")
        {
        }
    }
}

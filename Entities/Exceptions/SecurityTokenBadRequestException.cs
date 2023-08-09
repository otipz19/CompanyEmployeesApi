namespace Entities.Exceptions
{
    public class SecurityTokenBadRequestException : BadRequestException
    {
        public SecurityTokenBadRequestException()
            : base("Invalid access token")
        {
            
        }
    }
}

namespace Entities.Exceptions
{
    public class AgeRangeBadRequestException : BadRequestException
    {
        public AgeRangeBadRequestException()
            : base("Min age must be lower than max age")
        {
        }
    }
}

namespace Entities.Exceptions
{
    public class CollectionByIdsBadRequestException : BadRequestException
    {
        public CollectionByIdsBadRequestException() 
            : base("Collection count dismatch comparing to ids")
        {
        }
    }
}

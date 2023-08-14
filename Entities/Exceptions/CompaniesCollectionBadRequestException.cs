namespace Entities.Exceptions
{
    public class CompaniesCollectionBadRequestException : BadRequestException
    {
        public CompaniesCollectionBadRequestException()
            : base("Companies collection is null")
        {
        }
    }
}

namespace Entities.Exceptions
{
    public class CompaniesCollectionBadRequest : BadRequestException
    {
        public CompaniesCollectionBadRequest()
            : base("Companies collection is null")
        {
        }
    }
}

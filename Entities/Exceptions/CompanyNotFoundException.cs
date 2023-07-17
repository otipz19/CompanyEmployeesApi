namespace Entities.Exceptions
{
    public class CompanyNotFoundException : NotFoundException
    {
        public CompanyNotFoundException(Guid id) 
            : base($"Company with id: {id} was not found")
        {
        }
    }
}

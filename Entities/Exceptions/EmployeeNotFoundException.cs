namespace Entities.Exceptions
{
    public class EmployeeNotFoundException : NotFoundException
    {
        public EmployeeNotFoundException(Guid companyId, Guid employeeId) 
            : base($"Employee with id: {employeeId} was not found for company with id: {companyId}")
        {
        }
    }
}

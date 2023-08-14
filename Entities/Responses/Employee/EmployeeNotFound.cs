using Entities.Responses.Abstractions;

namespace Entities.Responses.Employee
{
    public class EmployeeNotFound : NotFoundApiResponse
    {
        public EmployeeNotFound(Guid companyId, Guid employeeId) 
            : base($"Employee with id: {employeeId} was not found for company with id: {companyId}")
        {
        }
    }
}

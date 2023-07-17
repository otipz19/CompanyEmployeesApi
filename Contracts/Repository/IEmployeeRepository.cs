using Entities.Models;

namespace Contracts.Repository
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetAllEmployeesForCompany(Guid companyId, bool asNoTracking);
    }
}

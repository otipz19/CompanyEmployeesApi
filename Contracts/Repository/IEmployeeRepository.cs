using Entities.Models;

namespace Contracts.Repository
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetAllEmployeesForCompany(Guid companyId, bool asNoTracking);

        public Task<Employee?> GetEmployeeForCompany(Guid companyId, Guid employeeId, bool asNoTracking);

        public void CreateEmployee(Employee employee, Guid companyId);
    }
}

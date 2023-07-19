using Entities.Models;

namespace Contracts.Repository
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetAllEmployeesOfCompany(Guid companyId, bool asNoTracking);

        public Task<Employee?> GetEmployeeOfCompany(Guid companyId, Guid employeeId, bool asNoTracking);

        public void CreateEmployee(Employee employee, Guid companyId);

        public void DeleteEmployee(Employee employee);
    }
}

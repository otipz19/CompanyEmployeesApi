using Entities.Models;
using Shared.DTO.RequestFeatures;
using Shared.DTO.RequestFeatures.Paging;

namespace Contracts.Repository
{
    public interface IEmployeeRepository
    {
        public Task<PagedList<Employee>> GetEmployeesOfCompany(Guid companyId,
            EmployeeRequestParameters requestParameters,
            bool asNoTracking);

        public Task<Employee?> GetEmployeeOfCompany(Guid companyId, Guid employeeId, bool asNoTracking);

        public void CreateEmployee(Employee employee, Guid companyId);

        public void DeleteEmployee(Employee employee);
    }
}

using Contracts.Repository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.DTO.RequestFeatures;
using Shared.DTO.RequestFeatures.Paging;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateEmployee(Employee employee, Guid companyId)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }

        public async Task<PagedList<Employee>> GetEmployeesOfCompany(Guid companyId,
            EmployeeRequestParameters requestParameters,
            bool asNoTracking)
        {
            IQueryable<Employee> employees = GetByCondition(e => e.CompanyId == companyId, asNoTracking)
                .OrderBy(e => e.Name)
                .FilterEmployees(requestParameters)
                .SearchEmployees(requestParameters)
                .OrderEmployees(requestParameters);

            var pagedResult = await PagedList<Employee>.CreateAsync(employees, requestParameters);
            return pagedResult;
        }

        public async Task<Employee?> GetEmployeeOfCompany(Guid companyId, Guid employeeId, bool asNoTracking)
        {
            Employee? employee = await GetByCondition(e => e.CompanyId == companyId && e.Id == employeeId, asNoTracking)
                .SingleOrDefaultAsync();

            return employee;
        }
    }
}

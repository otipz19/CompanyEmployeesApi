using Contracts.Repository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesForCompany(Guid companyId, bool asNoTracking)
        {
            IEnumerable<Employee> employees = await GetByCondition(e => e.CompanyId == companyId, asNoTracking)
                .OrderBy(e => e.Name)
                .ToListAsync();

            return employees;
        }

        public async Task<Employee?> GetEmployeeForCompany(Guid companyId, Guid employeeId, bool asNoTracking)
        {
            Employee? employee = await GetByCondition(e => e.CompanyId == companyId && e.Id == employeeId, asNoTracking)
                .SingleOrDefaultAsync();

            return employee;
        }
    }
}

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
    }
}

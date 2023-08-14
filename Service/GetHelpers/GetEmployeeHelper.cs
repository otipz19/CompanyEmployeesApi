using Contracts.Repository;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts.GetHelpers;

namespace Service.GetHelpers
{
    public class GetEmployeeHelper : IGetEmployeeHelper
    {
        private readonly IRepositoryManager _repositories;

        public GetEmployeeHelper(IRepositoryManager repositories)
        {
            _repositories = repositories;
        }

        public async Task<Employee> GetEmployeeIfExistsAsNoTracking(Guid companyId, Guid employeeId)
        {
            return await GetEmployeeIfExists(companyId, employeeId, asNoTracking: true);
        }

        public async Task<Employee> GetEmployeeIfExists(Guid companyId, Guid employeeId)
        {
            return await GetEmployeeIfExists(companyId, employeeId, asNoTracking: false);
        }

        private async Task<Employee> GetEmployeeIfExists(Guid companyId, Guid employeeId, bool asNoTracking)
        {
            Employee? employee = await _repositories.Employees.GetEmployeeOfCompany(companyId, employeeId, asNoTracking);
            if (employee is null)
            {
                throw new EmployeeNotFoundException(companyId, employeeId);
            }
            return employee;
        }
    }
}

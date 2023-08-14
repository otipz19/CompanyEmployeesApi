using Entities.Models;

namespace Service.Contracts.GetHelpers
{
    public interface IGetEmployeeHelper
    {
        public Task<Employee> GetEmployeeIfExistsAsNoTracking(Guid companyId, Guid employeeId);

        public Task<Employee> GetEmployeeIfExists(Guid companyId, Guid employeeId);
    }
}

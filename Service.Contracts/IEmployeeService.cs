using Shared.DTO;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<EmployeeDto>> GetAllEmployeesForCompany(Guid companyId, bool asNoTracking); 

        public Task<EmployeeDto> GetEmployeeForCompany(Guid companyId, Guid employeeId, bool asNoTracking);
    }
}

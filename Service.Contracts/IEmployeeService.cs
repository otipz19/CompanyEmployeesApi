using Shared.DTO.Employee;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<GetEmployeeDto>> GetAllEmployeesForCompany(Guid companyId, bool asNoTracking); 

        public Task<GetEmployeeDto> GetEmployeeForCompany(Guid companyId, Guid employeeId, bool asNoTracking);

        public Task<GetEmployeeDto> CreateEmployeeForCompany(CreateEmployeeDto dto, Guid companyId);

        public Task DeleteEmployeeForCompany(Guid companyId, Guid employeeId);

        public Task UpdateEmployeeForCompany(Guid companyId, Guid employeeId, UpdateEmployeeDto dto);
    }
}

using Shared.DTO.Employee;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<GetEmployeeDto>> GetAllEmployeesOfCompany(Guid companyId); 

        public Task<GetEmployeeDto> GetEmployeeOfCompany(Guid companyId, Guid employeeId);

        public Task<GetEmployeeDto> CreateEmployeeOfCompany(CreateEmployeeDto dto, Guid companyId);

        public Task DeleteEmployeeOfCompany(Guid companyId, Guid employeeId);

        public Task UpdateEmployeeOfCompany(Guid companyId, Guid employeeId, UpdateEmployeeDto dto);
    }
}

using Entities.Models;
using Shared.DTO.Employee;
using Shared.DTO.RequestFeatures;
using Shared.DTO.RequestFeatures.Paging;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        public Task<PagedList<GetEmployeeDto>> GetEmployeesOfCompany(Guid companyId,
            EmployeeRequestParameters pagingParameters);

        public Task<GetEmployeeDto> GetEmployeeOfCompany(Guid companyId, Guid employeeId);

        public Task<GetEmployeeDto> CreateEmployeeOfCompany(CreateEmployeeDto dto, Guid companyId);

        public Task DeleteEmployeeOfCompany(Guid companyId, Guid employeeId);

        public Task UpdateEmployeeOfCompany(Guid companyId, Guid employeeId, UpdateEmployeeDto dto);

        public Task<(UpdateEmployeeDto dto, Employee entity)> GetEmployeeForPatch(Guid companyId, Guid employeeId);

        public Task SaveChangesForPatch(UpdateEmployeeDto dto, Employee entity);
    }
}

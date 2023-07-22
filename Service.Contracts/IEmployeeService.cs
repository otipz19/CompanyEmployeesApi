using Entities.Models;
using Shared.DTO.Employee;
using Shared.DTO.RequestFeatures;
using Shared.DTO.RequestFeatures.Paging;
using System.Dynamic;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        public Task<(IEnumerable<ExpandoObject> items, PagingMetaData metaData)> GetEmployeesOfCompany(Guid companyId,
            EmployeeRequestParameters requestParameters);

        public Task<GetEmployeeDto> GetEmployeeOfCompany(Guid companyId, Guid employeeId);

        public Task<GetEmployeeDto> CreateEmployeeOfCompany(CreateEmployeeDto dto, Guid companyId);

        public Task DeleteEmployeeOfCompany(Guid companyId, Guid employeeId);

        public Task UpdateEmployeeOfCompany(Guid companyId, Guid employeeId, UpdateEmployeeDto dto);

        public Task<(UpdateEmployeeDto dto, Employee entity)> GetEmployeeForPatch(Guid companyId, Guid employeeId);

        public Task SaveChangesForPatch(UpdateEmployeeDto dto, Employee entity);
    }
}

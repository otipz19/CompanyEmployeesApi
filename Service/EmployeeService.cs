using AutoMapper;
using Contracts.LoggerService;
using Contracts.Repository;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTO.Employee;
using Shared.DTO.RequestFeatures;
using Shared.DTO.RequestFeatures.Paging;

namespace Service
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        public EmployeeService(IRepositoryManager repositoryManager, IMapper mapper)
            : base(repositoryManager, mapper) { }

        public async Task<GetEmployeeDto> CreateEmployeeOfCompany(CreateEmployeeDto dto, Guid companyId)
        {
            await GetCompanyIfExistsAsNoTracking(companyId);

            Employee employee = _mapper.Map<Employee>(dto);

            _repositories.Employees.CreateEmployee(employee, companyId);
            await _repositories.SaveChangesAsync();

            return _mapper.Map<GetEmployeeDto>(employee);
        }

        public async Task UpdateEmployeeOfCompany(Guid companyId, Guid employeeId, UpdateEmployeeDto dto)
        {
            await GetCompanyIfExistsAsNoTracking(companyId);

            Employee employee = await GetEmployeeIfExists(companyId, employeeId);

            _mapper.Map(dto, employee);

            await _repositories.SaveChangesAsync();
        }

        public async Task DeleteEmployeeOfCompany(Guid companyId, Guid employeeId)
        {
            await GetCompanyIfExistsAsNoTracking(companyId);

            Employee employee = await GetEmployeeIfExists(companyId, employeeId);

            _repositories.Employees.DeleteEmployee(employee);
            await _repositories.SaveChangesAsync();
        }

        public async Task<PagedList<GetEmployeeDto>> GetEmployeesOfCompany(Guid companyId,
            EmployeeRequestParameters requestParameters)
        {
            if (!requestParameters.IsValidAgeRange)
            {
                throw new AgeRangeBadRequestException();
            }

            await GetCompanyIfExistsAsNoTracking(companyId);

            PagedList<Employee> pagedEmployees = await _repositories.Employees
                .GetEmployeesOfCompany(companyId, requestParameters, asNoTracking: true);

            IEnumerable<GetEmployeeDto> employeeDtos = _mapper.Map<IEnumerable<GetEmployeeDto>>(pagedEmployees.Items);
            PagedList<GetEmployeeDto> pagedDtos = new(employeeDtos, pagedEmployees.MetaData);
            return pagedDtos;
        }

        public async Task<GetEmployeeDto> GetEmployeeOfCompany(Guid companyId, Guid employeeId)
        {
            await GetCompanyIfExistsAsNoTracking(companyId);

            Employee employee = await GetEmployeeIfExists(companyId, employeeId);

            GetEmployeeDto employeeDto = _mapper.Map<GetEmployeeDto>(employee);
            return employeeDto;
        }

        public async Task<(UpdateEmployeeDto dto, Employee entity)> GetEmployeeForPatch(Guid companyId, Guid employeeId)
        {
            await GetCompanyIfExistsAsNoTracking(companyId);

            Employee entity = await GetEmployeeIfExists(companyId, employeeId);

            UpdateEmployeeDto dto  = _mapper.Map<UpdateEmployeeDto>(entity);

            return (dto, entity);
        }

        public async Task SaveChangesForPatch(UpdateEmployeeDto dto, Employee entity)
        {
            _mapper.Map(dto, entity);
            await _repositories.SaveChangesAsync();
        }
    }
}

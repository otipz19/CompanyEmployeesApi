using AutoMapper;
using Contracts.LoggerService;
using Contracts.Repository;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTO.Employee;

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

        public async Task<IEnumerable<GetEmployeeDto>> GetAllEmployeesOfCompany(Guid companyId)
        {
            await GetCompanyIfExistsAsNoTracking(companyId);

            IEnumerable<Employee> employees = await _repositories.Employees
                .GetAllEmployeesOfCompany(companyId, asNoTracking: true);

            IEnumerable<GetEmployeeDto> employeeDtos = _mapper.Map<IEnumerable<GetEmployeeDto>>(employees);
            return employeeDtos;
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

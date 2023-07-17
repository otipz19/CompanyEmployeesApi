using AutoMapper;
using Contracts.LoggerService;
using Contracts.Repository;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTO.Employee;

namespace Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repositories;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EmployeeService(ILoggerManager loggerManager,
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _logger = loggerManager;
            _repositories = repositoryManager;
            _mapper = mapper;
        }

        public async Task<GetEmployeeDto> CreateEmployeeForCompany(CreateEmployeeDto dto, Guid companyId)
        {
            await CheckCompanyExists(companyId);

            Employee employee = _mapper.Map<Employee>(dto);

            _repositories.Employees.CreateEmployee(employee, companyId);
            await _repositories.SaveChangesAsync();

            return _mapper.Map<GetEmployeeDto>(employee);
        }

        public async Task<IEnumerable<GetEmployeeDto>> GetAllEmployeesForCompany(Guid companyId, bool asNoTracking)
        {
            await CheckCompanyExists(companyId);

            IEnumerable<Employee> employees = await _repositories.Employees
                .GetAllEmployeesForCompany(companyId, asNoTracking);

            IEnumerable<GetEmployeeDto> employeeDtos = _mapper.Map<IEnumerable<GetEmployeeDto>>(employees);
            return employeeDtos;
        }

        public async Task<GetEmployeeDto> GetEmployeeForCompany(Guid companyId, Guid employeeId, bool asNoTracking)
        {
            await CheckCompanyExists(companyId);

            Employee? employee = await _repositories.Employees.GetEmployeeForCompany(companyId, employeeId, asNoTracking);
            if(employee is null)
            {
                throw new EmployeeNotFoundException(companyId, employeeId);
            }

            GetEmployeeDto employeeDto = _mapper.Map<GetEmployeeDto>(employee);
            return employeeDto;
        }

        private async Task<Company> CheckCompanyExists(Guid companyId)
        {
            Company? company = await _repositories.Companies.GetCompany(companyId, asNoTracking: true);

            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            return company;
        }
    }
}

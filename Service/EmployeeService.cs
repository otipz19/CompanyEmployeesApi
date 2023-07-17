using AutoMapper;
using Contracts.LoggerService;
using Contracts.Repository;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTO;

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

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesForCompany(Guid companyId, bool asNoTracking)
        {
            await CheckCompanyExists(companyId);

            IEnumerable<Employee> employees = await _repositories.Employees
                .GetAllEmployeesForCompany(companyId, asNoTracking);

            IEnumerable<EmployeeDto> employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return employeeDtos;
        }

        public async Task<EmployeeDto> GetEmployeeForCompany(Guid companyId, Guid employeeId, bool asNoTracking)
        {
            await CheckCompanyExists(companyId);

            Employee? employee = await _repositories.Employees.GetEmployeeForCompany(companyId, employeeId, asNoTracking);
            if(employee is null)
            {
                throw new EmployeeNotFoundException(companyId, employeeId);
            }

            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }

        private async Task CheckCompanyExists(Guid companyId)
        {
            Company? company = await _repositories.Companies.GetCompany(companyId, asNoTracking: true);

            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }
        }
    }
}

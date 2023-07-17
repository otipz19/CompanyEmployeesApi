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
            Company company = await _repositories.Companies.GetCompany(companyId, asNoTracking);

            if(company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            IEnumerable<Employee> employees = await _repositories.Employees
                .GetAllEmployeesForCompany(companyId, asNoTracking);

            IEnumerable<EmployeeDto> employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return employeeDtos;
        }
    }
}

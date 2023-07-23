using AutoMapper;
using Service.Contracts.DataShaping;
using Contracts.Repository;
using Entities.Exceptions;
using Entities.Models;

namespace Service
{
    public abstract class BaseService
    {
        protected readonly IRepositoryManager _repositories;
        protected readonly IMapper _mapper;
        protected readonly IDataShaper _dataShaper;

        protected BaseService(IRepositoryManager repositories,
            IMapper mapper,
            IDataShaper dataShaper)
        {
            _repositories = repositories;
            _mapper = mapper;
            _dataShaper = dataShaper;
        }

        protected async Task<Company> GetCompanyIfExistsAsNoTracking(Guid companyId)
        {
            return await GetCompanyIfExists(companyId, asNoTracking: true);
        }

        protected async Task<Company> GetCompanyIfExists(Guid companyId)
        {
            return await GetCompanyIfExists(companyId, asNoTracking: false);
        }

        private async Task<Company> GetCompanyIfExists(Guid companyId, bool asNoTracking)
        {
            Company? company = await _repositories.Companies.GetCompany(companyId, asNoTracking);
            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }
            return company;
        }

        protected async Task<Employee> GetEmployeeIfExistsAsNoTracking(Guid companyId, Guid employeeId)
        {
            return await GetEmployeeIfExists(companyId, employeeId, asNoTracking: true);
        }

        protected async Task<Employee> GetEmployeeIfExists(Guid companyId, Guid employeeId)
        {
            return await GetEmployeeIfExists(companyId, employeeId, asNoTracking: false);
        }

        private async Task<Employee> GetEmployeeIfExists(Guid companyId, Guid employeeId, bool asNoTracking)
        {
            Employee? employee = await _repositories.Employees.GetEmployeeOfCompany(companyId, employeeId, asNoTracking);
            if (employee is null)
            {
                throw new EmployeeNotFoundException(companyId, employeeId);
            }
            return employee;
        }
    }
}

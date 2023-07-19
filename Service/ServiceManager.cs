using AutoMapper;
using Contracts.LoggerService;
using Contracts.Repository;
using Service.Contracts;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<ICompanyService> _companyService;

        public ServiceManager(IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _employeeService = new Lazy<IEmployeeService>(
                () => new EmployeeService(repositoryManager, mapper));
            _companyService = new Lazy<ICompanyService>(
                () => new CompanyService(repositoryManager, mapper));
        }

        public ICompanyService CompanyService => _companyService.Value;

        public IEmployeeService EmployeeService => _employeeService.Value;
    }
}

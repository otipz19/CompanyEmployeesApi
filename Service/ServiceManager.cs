using Contracts.LoggerService;
using Contracts.Repository;
using Service.Contracts;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<ICompanyService> _companyService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
        {
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(loggerManager, repositoryManager));
            _companyService = new Lazy<ICompanyService>(() => new CompanyService(loggerManager, repositoryManager));
        }

        public ICompanyService CompanyService => _companyService.Value;

        public IEmployeeService EmployeeService => _employeeService.Value;
    }
}

using Contracts.LoggerService;
using Contracts.Repository;
using Service.Contracts;

namespace Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;

        public EmployeeService(ILoggerManager loggerManager,
            IRepositoryManager repositoryManager)
        {
            _loggerManager = loggerManager;
            _repositoryManager = repositoryManager;
        }
    }
}

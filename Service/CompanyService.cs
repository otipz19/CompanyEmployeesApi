using Contracts.LoggerService;
using Contracts.Repository;
using Service.Contracts;

namespace Service
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;

        public CompanyService(ILoggerManager loggerManager,
            IRepositoryManager repositoryManager)
        {
            _loggerManager = loggerManager;
            _repositoryManager = repositoryManager;
        }
    }
}

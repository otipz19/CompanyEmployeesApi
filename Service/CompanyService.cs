using AutoMapper;
using Contracts.LoggerService;
using Contracts.Repository;
using Entities.Models;
using Service.Contracts;
using Shared.DTO;

namespace Service
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repositories;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public CompanyService(ILoggerManager loggerManager,
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _loggerManager = loggerManager;
            _repositories = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompanies(bool asNoTracking)
        {
            try
            {
                var companies = await _repositories.Companies.GetAllCompanies(asNoTracking);
                var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
                return companiesDto;
            }
            catch(Exception ex)
            {
                _loggerManager.LogError($"Something went wrong in {nameof(GetAllCompanies)} service method: {ex}");
                throw;
            }
        }
    }
}

using AutoMapper;
using Contracts.LoggerService;
using Contracts.Repository;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTO.Company;

namespace Service
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repositories;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompanyService(ILoggerManager loggerManager,
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _logger = loggerManager;
            _repositories = repositoryManager;
            _mapper = mapper;
        }

        public async Task<GetCompanyDto> CreateCompany(CreateCompanyDto dto)
        {
            Company company = _mapper.Map<Company>(dto);

            _repositories.Companies.CreateCompany(company);
            await _repositories.SaveChangesAsync();

            return _mapper.Map<GetCompanyDto>(company);
        }

        public async Task<IEnumerable<GetCompanyDto>> CreateCompaniesCollection(IEnumerable<CreateCompanyDto> dtos)
        {
            if (dtos is null)
                throw new CompaniesCollectionBadRequest();

            IEnumerable<Company> companies = _mapper.Map<IEnumerable<Company>>(dtos);

            foreach(var company in companies)
            {
                _repositories.Companies.CreateCompany(company);
            }
            await _repositories.SaveChangesAsync();

            return _mapper.Map<IEnumerable<GetCompanyDto>>(companies);
        }

        public async Task<IEnumerable<GetCompanyDto>> GetAllCompanies(bool asNoTracking)
        {
            IEnumerable<Company> companies = await _repositories.Companies.GetAllCompanies(asNoTracking);
            IEnumerable<GetCompanyDto> companiesDto = _mapper.Map<IEnumerable<GetCompanyDto>>(companies);
            return companiesDto;
        }

        public async Task<IEnumerable<GetCompanyDto>> GetCompaniesByIds(IEnumerable<Guid>? ids, bool asNoTracking)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            IEnumerable<Company> companies = await _repositories.Companies.GetCompaniesByIds(ids, asNoTracking);

            if (companies.Count() != ids.Count())
                throw new CollectionByIdsBadRequestException();

            IEnumerable<GetCompanyDto> getCompanyDtos = _mapper.Map<IEnumerable<GetCompanyDto>>(companies);
            return getCompanyDtos;
        }

        public async Task<GetCompanyDto> GetCompany(Guid id, bool asNoTracking)
        {
            Company company = await CheckCompanyExists(id, asNoTracking);

            var companieDto = _mapper.Map<GetCompanyDto>(company);
            return companieDto;
        }

        public async Task DeleteCompany(Guid id)
        {
            Company company = await CheckCompanyExists(id, asNoTracking: false);

            _repositories.Companies.DeleteCompany(company);
            await _repositories.SaveChangesAsync();
        }

        private async Task<Company> CheckCompanyExists(Guid id, bool asNoTracking)
        {
            Company? company = await _repositories.Companies.GetCompany(id, asNoTracking);

            if (company is null)
            {
                throw new CompanyNotFoundException(id);
            }

            return company;
        }
    }
}

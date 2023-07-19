using AutoMapper;
using Contracts.LoggerService;
using Contracts.Repository;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTO.Company;
using System.Formats.Asn1;

namespace Service
{
    public class CompanyService : BaseService, ICompanyService
    {
        public CompanyService(IRepositoryManager repositoryManager, IMapper mapper)
            : base(repositoryManager, mapper) { }

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

        public async Task UpdateCompany(Guid id, UpdateCompanyDto dto)
        {
            Company company = await GetCompanyIfExists(id);

            _mapper.Map(dto, company);

            await _repositories.SaveChangesAsync();
        }

        public async Task DeleteCompany(Guid id)
        {
            Company company = await GetCompanyIfExists(id);

            _repositories.Companies.DeleteCompany(company);
            await _repositories.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetCompanyDto>> GetAllCompanies()
        {
            IEnumerable<Company> companies = await _repositories.Companies.GetAllCompanies(asNoTracking: true);
            IEnumerable<GetCompanyDto> companiesDto = _mapper.Map<IEnumerable<GetCompanyDto>>(companies);
            return companiesDto;
        }

        public async Task<IEnumerable<GetCompanyDto>> GetCompaniesByIds(IEnumerable<Guid>? ids)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            IEnumerable<Company> companies = await _repositories.Companies.GetCompaniesByIds(ids, asNoTracking: true);

            if (companies.Count() != ids.Count())
                throw new CollectionByIdsBadRequestException();

            IEnumerable<GetCompanyDto> getCompanyDtos = _mapper.Map<IEnumerable<GetCompanyDto>>(companies);
            return getCompanyDtos;
        }

        public async Task<GetCompanyDto> GetCompany(Guid id)
        {
            Company company = await GetCompanyIfExists(id);

            var companieDto = _mapper.Map<GetCompanyDto>(company);
            return companieDto;
        }
    }
}

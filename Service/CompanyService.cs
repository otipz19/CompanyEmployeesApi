using AutoMapper;
using Contracts.LoggerService;
using Contracts.Repository;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTO.Company;
using Shared.DTO.RequestFeatures.Paging;
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

        public async Task<PagedList<GetCompanyDto>> GetCompanies(CompanyPagingParameters pagingParameters) 
        {
            PagedList<Company> pagedCompanies = await _repositories.Companies
                .GetCompanies(asNoTracking: true, pagingParameters);

            IEnumerable<GetCompanyDto> companiesDtos = _mapper.Map<IEnumerable<GetCompanyDto>>(pagedCompanies.Items);
            var pagedDtos = new PagedList<GetCompanyDto>(companiesDtos, pagedCompanies.MetaData);

            return pagedDtos;
        }

        public async Task<PagedList<GetCompanyDto>> GetCompaniesByIds(IEnumerable<Guid>? ids,
            CompanyPagingParameters pagingParameters)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            PagedList<Company> pagedCompanies = await _repositories.Companies
                .GetCompaniesByIds(ids, asNoTracking: true, pagingParameters);

            if (pagedCompanies.MetaData.TotalCount != ids.Count())
                throw new CollectionByIdsBadRequestException();

            IEnumerable<GetCompanyDto> companiesDtos = _mapper.Map<IEnumerable<GetCompanyDto>>(pagedCompanies.Items);
            var pagedDtos = new PagedList<GetCompanyDto>(companiesDtos, pagedCompanies.MetaData);

            return pagedDtos;
        }

        public async Task<GetCompanyDto> GetCompany(Guid id)
        {
            Company company = await GetCompanyIfExists(id);

            var companieDto = _mapper.Map<GetCompanyDto>(company);
            return companieDto;
        }

        public async Task<(UpdateCompanyDto dto, Company entity)> GetCompanyForPatch(Guid id)
        {
            Company company = await GetCompanyIfExists(id);
            UpdateCompanyDto dto = _mapper.Map<UpdateCompanyDto>(company);
            return (dto , company);
        }

        public async Task SaveChangesForPatch(UpdateCompanyDto dto, Company entity)
        {
            _mapper.Map(dto, entity);
            await _repositories.SaveChangesAsync();
        }
    }
}

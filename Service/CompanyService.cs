using AutoMapper;
using Contracts.Repository;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTO.Company;
using Shared.DTO.RequestFeatures;
using Shared.DTO.RequestFeatures.Paging;
using Service.Contracts.DataShaping;
using Entities.DataShaping;
using Entities.LinkModels;
using Contracts.Hateoas;

namespace Service
{
    public class CompanyService : BaseService, ICompanyService
    {
        private readonly ICompanyLinksGenerator _linksGenerator;

        public CompanyService(
            IRepositoryManager repositoryManager,
            IMapper mapper,
            IDataShaper dataShaper,
            ICompanyLinksGenerator linksGenerator)
            : base(repositoryManager, mapper, dataShaper)
        {
            _linksGenerator = linksGenerator;
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

            foreach (var company in companies)
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

        public async Task<(LinkResponse response, PagingMetaData metaData)> GetCompanies(LinkCompaniesParameters linkParameters)
        {
            PagedList<Company> pagedCompanies = await _repositories.Companies
                .GetCompanies(asNoTracking: true, linkParameters.RequestParameters);

            return GetCompanies(pagedCompanies, linkParameters);
        }

        public async Task<(LinkResponse response, PagingMetaData metaData)> GetCompaniesByIds(IEnumerable<Guid>? ids,
            LinkCompaniesParameters linkParameters)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            PagedList<Company> pagedCompanies = await _repositories.Companies
                .GetCompaniesByIds(ids, asNoTracking: true, linkParameters.RequestParameters);

            if (pagedCompanies.MetaData.TotalCount != ids.Count())
                throw new CollectionByIdsBadRequestException();

            return GetCompanies(pagedCompanies, linkParameters);
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
            return (dto, company);
        }

        public async Task SaveChangesForPatch(UpdateCompanyDto dto, Company entity)
        {
            _mapper.Map(dto, entity);
            await _repositories.SaveChangesAsync();
        }

        private (LinkResponse response, PagingMetaData metaData) GetCompanies(
            PagedList<Company> pagedCompanies,
            LinkCompaniesParameters linkParameters)
        {
            IEnumerable<GetCompanyDto> companiesDtos = _mapper.Map<IEnumerable<GetCompanyDto>>(pagedCompanies.Items);

            var shapedDtos = _dataShaper.ShapeData(companiesDtos, linkParameters.RequestParameters.Fields);

            var linkResponse = _linksGenerator.GenerateLinks(
                shapedDtos,
                linkParameters.RequestParameters.Fields,
                linkParameters.Context);

            return (linkResponse, pagedCompanies.MetaData);
        }
    }
}

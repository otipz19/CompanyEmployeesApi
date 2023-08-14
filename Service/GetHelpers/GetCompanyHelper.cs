using AutoMapper;
using Contracts.Hateoas;
using Contracts.Repository;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Service.Contracts.DataShaping;
using Service.Contracts.GetHelpers;
using Shared.DTO.Company;
using Shared.DTO.RequestFeatures.Paging;

namespace Service.GetHelpers
{
    public class GetCompanyHelper : IGetCompanyHelper
    {
        private readonly IRepositoryManager _repositories;
        private readonly IMapper _mapper;
        private readonly IDataShaper _dataShaper;
        private readonly ICompanyLinksGenerator _linksGenerator;

        public GetCompanyHelper(
            IRepositoryManager repositories,
            IMapper mapper,
            IDataShaper dataShaper,
            ICompanyLinksGenerator linksGenerator)
        {
            _repositories = repositories;
            _mapper = mapper;
            _dataShaper = dataShaper;
            _linksGenerator = linksGenerator;
        }

        public async Task<Company> GetCompanyIfExistsAsNoTracking(Guid companyId)
        {
            return await GetCompanyIfExists(companyId, asNoTracking: true);
        }

        public async Task<Company> GetCompanyIfExists(Guid companyId)
        {
            return await GetCompanyIfExists(companyId, asNoTracking: false);
        }

        public (LinkResponse response, PagingMetaData metaData) ShapeAndGenerateLinks(
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

        private async Task<Company> GetCompanyIfExists(Guid companyId, bool asNoTracking)
        {
            Company? company = await _repositories.Companies.GetCompany(companyId, asNoTracking);
            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }
            return company;
        }
    }
}

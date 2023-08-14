using Entities.Models;
using Entities.DataShaping;
using Shared.DTO.Company;
using Shared.DTO.RequestFeatures;
using Shared.DTO.RequestFeatures.Paging;
using Entities.LinkModels;
using Entities.Responses.Abstractions;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        public Task<(LinkResponse response, PagingMetaData metaData)> GetCompanies(LinkCompaniesParameters linkParameters);

        public Task<(LinkResponse response, PagingMetaData metaData)> GetCompaniesByIds(IEnumerable<Guid>? ids,
           LinkCompaniesParameters linkParameters);

        public Task<BaseApiResponse> GetCompany(Guid id);

        public Task<BaseApiResponse> CreateCompany(CreateCompanyDto dto);

        public Task<IEnumerable<GetCompanyDto>> CreateCompaniesCollection(IEnumerable<CreateCompanyDto> dtos);

        public Task<BaseApiResponse> UpdateCompany(Guid id, UpdateCompanyDto dto);

        public Task<BaseApiResponse> DeleteCompany(Guid id);

        public Task<(UpdateCompanyDto dto, Company entity)> GetCompanyForPatch(Guid id);

        public Task SaveChangesForPatch(UpdateCompanyDto dto, Company entity);
    }
}

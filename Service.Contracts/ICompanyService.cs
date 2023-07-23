using Entities.Models;
using Service.Contracts.DataShaping;
using Shared.DTO.Company;
using Shared.DTO.RequestFeatures;
using Shared.DTO.RequestFeatures.Paging;
using System.Dynamic;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        public Task<(IEnumerable<IShapedObject> items, PagingMetaData metaData)> GetCompanies(
            CompanyRequestParameters requestParameters);

        public Task<(IEnumerable<IShapedObject> items, PagingMetaData metaData)> GetCompaniesByIds(IEnumerable<Guid> ids,
            CompanyRequestParameters requestParameters);

        public Task<GetCompanyDto> GetCompany(Guid id);

        public Task<GetCompanyDto> CreateCompany(CreateCompanyDto dto);

        public Task<IEnumerable<GetCompanyDto>> CreateCompaniesCollection(IEnumerable<CreateCompanyDto> dtos);

        public Task UpdateCompany(Guid id, UpdateCompanyDto dto);

        public Task DeleteCompany(Guid id);

        public Task<(UpdateCompanyDto dto, Company entity)> GetCompanyForPatch(Guid id);

        public Task SaveChangesForPatch(UpdateCompanyDto dto, Company entity);
    }
}

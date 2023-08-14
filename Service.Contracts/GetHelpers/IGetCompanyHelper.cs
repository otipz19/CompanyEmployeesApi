using Entities.LinkModels;
using Entities.Models;
using Microsoft.AspNetCore.Routing;
using Service.Contracts.DataShaping;
using Shared.DTO.Company;
using Shared.DTO.RequestFeatures.Paging;

namespace Service.Contracts.GetHelpers
{
    public interface IGetCompanyHelper
    {
        public Task<Company> GetCompanyIfExistsAsNoTracking(Guid companyId);

        public Task<Company> GetCompanyIfExists(Guid companyId);

        public (LinkResponse response, PagingMetaData metaData) ShapeAndGenerateLinks(
            PagedList<Company> pagedCompanies,
            LinkCompaniesParameters linkParameters);
    }
}

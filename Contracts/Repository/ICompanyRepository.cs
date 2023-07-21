using Entities.Models;
using Shared.DTO.RequestFeatures;
using Shared.DTO.RequestFeatures.Paging;

namespace Contracts.Repository
{
    public interface ICompanyRepository
    {
        public Task<PagedList<Company>> GetCompanies(bool asNoTracking, CompanyRequestParameters requestParameters);

        public Task<PagedList<Company>> GetCompaniesByIds(IEnumerable<Guid> ids, bool asNoTracking,
            CompanyRequestParameters requestParameters);

        public Task<Company?> GetCompany(Guid id, bool asNoTracking);

        public void CreateCompany(Company company);

        public void DeleteCompany(Company company);
    }
}

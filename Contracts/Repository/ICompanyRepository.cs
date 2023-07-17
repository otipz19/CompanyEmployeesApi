using Entities.Models;

namespace Contracts.Repository
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetAllCompanies(bool asNoTracking);

        public Task<IEnumerable<Company>> GetCompaniesByIds(IEnumerable<Guid> ids, bool asNoTracking);

        public Task<Company?> GetCompany(Guid id, bool asNoTracking);

        public void CreateCompany(Company company);
    }
}

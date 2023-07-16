using Entities.Models;

namespace Contracts.Repository
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetAllCompanies(bool asNoTracking);
    }
}

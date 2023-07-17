using Contracts.Repository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Company>> GetAllCompanies(bool asNoTracking)
        {
            return await GetAll(asNoTracking)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<Company?> GetCompany(Guid id, bool asNoTracking)
        {
            Company? company = await GetByCondition(c => c.Id == id, asNoTracking)
                .SingleOrDefaultAsync();
            return company;
        }
    }
}

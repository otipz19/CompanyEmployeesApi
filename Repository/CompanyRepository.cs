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

        public void CreateCompany(Company company)
        {
            Create(company);
        }

        public async Task<IEnumerable<Company>> GetAllCompanies(bool asNoTracking)
        {
            return await GetAll(asNoTracking)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Company>> GetCompaniesByIds(IEnumerable<Guid> ids, bool asNoTracking)
        {
            IEnumerable<Company> companies = await GetByCondition(c => ids.Contains(c.Id), asNoTracking)
                .OrderBy(c => c.Name)
                .ToListAsync();
            return companies;
        }

        public async Task<Company?> GetCompany(Guid id, bool asNoTracking)
        {
            Company? company = await GetByCondition(c => c.Id == id, asNoTracking)
                .SingleOrDefaultAsync();
            return company;
        }
    }
}

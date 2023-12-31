﻿using Contracts.Repository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.DTO.RequestFeatures;
using Shared.DTO.RequestFeatures.Paging;

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

        public void DeleteCompany(Company company)
        {
            Delete(company);
        }

        public async Task<PagedList<Company>> GetCompanies(bool asNoTracking, CompanyRequestParameters requestParameters)
        {
            IQueryable<Company> companies = GetAll(asNoTracking)
                .OrderBy(c => c.Name)
                .SearchCompanies(requestParameters)
                .OrderCompanies(requestParameters);

            var pagedResult = await PagedList<Company>.CreateAsync(companies, requestParameters);
            return pagedResult;
        }

        public async Task<PagedList<Company>> GetCompaniesByIds(IEnumerable<Guid> ids, bool asNoTracking,
            CompanyRequestParameters requestParameters)
        {
            IQueryable<Company> companies = GetByCondition(c => ids.Contains(c.Id), asNoTracking)
                .OrderBy(c => c.Name)
                .SearchCompanies(requestParameters);

            var pagedResult = await PagedList<Company>.CreateAsync(companies, requestParameters);
            return pagedResult;
        }

        public async Task<Company?> GetCompany(Guid id, bool asNoTracking)
        {
            Company? company = await GetByCondition(c => c.Id == id, asNoTracking)
                .SingleOrDefaultAsync();
            return company;
        }
    }
}

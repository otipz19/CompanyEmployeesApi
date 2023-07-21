﻿using Entities.Models;
using Shared.DTO.Company;
using Shared.DTO.RequestFeatures;
using Shared.DTO.RequestFeatures.Paging;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        public Task<PagedList<GetCompanyDto>> GetCompanies(CompanyRequestParameters pagingParameters);

        public Task<PagedList<GetCompanyDto>> GetCompaniesByIds(IEnumerable<Guid> ids,
            CompanyRequestParameters pagingParameters);

        public Task<GetCompanyDto> GetCompany(Guid id);

        public Task<GetCompanyDto> CreateCompany(CreateCompanyDto dto);

        public Task<IEnumerable<GetCompanyDto>> CreateCompaniesCollection(IEnumerable<CreateCompanyDto> dtos);

        public Task UpdateCompany(Guid id, UpdateCompanyDto dto);

        public Task DeleteCompany(Guid id);

        public Task<(UpdateCompanyDto dto, Company entity)> GetCompanyForPatch(Guid id);

        public Task SaveChangesForPatch(UpdateCompanyDto dto, Company entity);
    }
}

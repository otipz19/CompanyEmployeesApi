using Shared.DTO.Company;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        public Task<IEnumerable<GetCompanyDto>> GetAllCompanies();

        public Task<IEnumerable<GetCompanyDto>> GetCompaniesByIds(IEnumerable<Guid> ids);

        public Task<GetCompanyDto> GetCompany(Guid id);

        public Task<GetCompanyDto> CreateCompany(CreateCompanyDto dto);

        public Task<IEnumerable<GetCompanyDto>> CreateCompaniesCollection(IEnumerable<CreateCompanyDto> dtos);

        public Task UpdateCompany(Guid id, UpdateCompanyDto dto);

        public Task DeleteCompany(Guid id);
    }
}

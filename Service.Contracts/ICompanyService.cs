using Shared.DTO.Company;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        public Task<IEnumerable<GetCompanyDto>> GetAllCompanies(bool asNoTracking);

        public Task<IEnumerable<GetCompanyDto>> GetCompaniesByIds(IEnumerable<Guid> ids, bool asNoTracking);

        public Task<GetCompanyDto> GetCompany(Guid id, bool asNoTracking);

        public Task<GetCompanyDto> CreateCompany(CreateCompanyDto dto);

        public Task<IEnumerable<GetCompanyDto>> CreateCompaniesCollection(IEnumerable<CreateCompanyDto> dtos);
    }
}

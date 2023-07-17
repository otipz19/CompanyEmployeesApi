using Shared.DTO;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        public Task<IEnumerable<CompanyDto>> GetAllCompanies(bool asNoTracking);

        public Task<CompanyDto> GetCompany(Guid id, bool asNoTracking);
    }
}

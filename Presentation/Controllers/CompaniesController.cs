using Microsoft.AspNetCore.Mvc;
using Presentation.ModelBinders;
using Service.Contracts;
using Shared.DTO.Company;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _services;

        public CompaniesController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult> GetCompanies()
        {
            IEnumerable<GetCompanyDto> companies = await _services.CompanyService.GetAllCompanies(asNoTracking: true);
            return Ok(companies);
        }

        [HttpGet("collection/({ids})")]
        public async Task<ActionResult> GetCompaniesByIds([ModelBinder(BinderType = typeof(ArrayModelBinder))]
            IEnumerable<Guid> ids)
        {
            IEnumerable<GetCompanyDto> companies = await _services.CompanyService.GetCompaniesByIds(ids, asNoTracking: true);
            return Ok(companies);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetCompany(Guid id)
        {
            GetCompanyDto company = await _services.CompanyService.GetCompany(id, asNoTracking: true);
            return Ok(company);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCompany(CreateCompanyDto createDto)
        {
            GetCompanyDto result = await _services.CompanyService.CreateCompany(createDto);
            return CreatedAtAction(nameof(GetCompany), new { id = result.Id }, result);
        }

        [HttpPost("collection")]
        public async Task<ActionResult> CreateCompanies(IEnumerable<CreateCompanyDto> createDtos)
        {
            IEnumerable<GetCompanyDto> result = await _services.CompanyService.CreateCompaniesCollection(createDtos);
            string ids = string.Join(',', result.Select(c => c.Id.ToString()));
            return CreatedAtAction(nameof(GetCompaniesByIds), new { ids = ids }, result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteCompany(Guid id)
        {
            await _services.CompanyService.DeleteCompany(id);
            return NoContent();
        }
    }
}

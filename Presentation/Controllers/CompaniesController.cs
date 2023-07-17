using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> GetCompaniesByIds(IEnumerable<Guid> ids)
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
        public async Task<ActionResult> CreateCompany([FromBody] CreateCompanyDto company)
        {
            GetCompanyDto result = await _services.CompanyService.CreateCompany(company);
            return CreatedAtAction(nameof(GetCompany), new { id = result.Id }, result);
        }
    }
}

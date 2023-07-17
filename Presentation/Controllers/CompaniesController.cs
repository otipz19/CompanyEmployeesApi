using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

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
            var companies = await _services.CompanyService.GetAllCompanies(asNoTracking: true);
            return Ok(companies);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetCompany(Guid id)
        {
            var company = await _services.CompanyService.GetCompany(id, asNoTracking: true);
            return Ok(company);
        }
    }
}

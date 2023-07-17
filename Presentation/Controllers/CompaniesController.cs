using Microsoft.AspNetCore.Http;
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
            return Ok(await _services.CompanyService.GetAllCompanies(asNoTracking: true));
        }
    }
}

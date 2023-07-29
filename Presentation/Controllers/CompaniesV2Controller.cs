using Entities.LinkModels;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Service.Contracts;
using Shared.DTO.RequestFeatures;

namespace Presentation.Controllers
{
    [ApiVersion("2.0", Deprecated = true)]
    [Route("api/companies")]
    [ApiController]
    public class CompaniesV2Controller : ControllerBase
    {
        private readonly IServiceManager _services;

        public CompaniesV2Controller(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        [HttpHead]
        [ValidateMediaType]
        public async Task<ActionResult> GetCompanies([FromQuery] CompanyRequestParameters requestParameters)
        {
            var companies = await _services.CompanyService
                .GetCompanies(new LinkCompaniesParameters(requestParameters, HttpContext));

            return Ok(companies.response.HasLinks ? companies.response.LinkedEntities : companies.response.ShapedEntities);
        }
    }
}

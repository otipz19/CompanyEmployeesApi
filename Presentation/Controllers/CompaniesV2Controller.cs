using Application.Queries.Companies;
using Entities.LinkModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Shared.DTO.RequestFeatures;

namespace Presentation.Controllers
{
    [ApiVersion("2.0", Deprecated = true)]
    [Route("api/companies")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    public class CompaniesV2Controller : ControllerBase
    {
        private readonly ISender _sender;

        public CompaniesV2Controller(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [HttpHead]
        [ValidateMediaType]
        public async Task<ActionResult> GetCompanies([FromQuery] CompanyRequestParameters requestParameters)
        {
            var query = new GetCompaniesQuery(new LinkCompaniesParameters(requestParameters, HttpContext));
            var companies = await _sender.Send(query);

            return Ok(companies.response.HasLinks ? companies.response.LinkedEntities : companies.response.ShapedEntities);
        }
    }
}

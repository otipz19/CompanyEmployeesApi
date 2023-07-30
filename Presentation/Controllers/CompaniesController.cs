using Entities.LinkModels;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Presentation.ActionFilters;
using Presentation.ModelBinders;
using Service.Contracts;
using Shared.DTO.Company;
using Shared.DTO.RequestFeatures;
using System.Text.Json;

namespace Presentation.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/companies")]
    [EnableRateLimiting("fixed")]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _services;

        public CompaniesController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        [HttpHead]
        [ValidateMediaType]
        public async Task<ActionResult> GetCompanies([FromQuery]CompanyRequestParameters requestParameters)
        {
            var companies = await _services.CompanyService
                .GetCompanies(new LinkCompaniesParameters(requestParameters, HttpContext));

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(companies.metaData));

            return Ok(companies.response.HasLinks ? companies.response.LinkedEntities : companies.response.ShapedEntities);
        }

        [HttpGet("collection/({ids})")]
        [HttpHead("collection/({ids})")]
        [ValidateMediaType]
        public async Task<ActionResult> GetCompaniesByIds([FromQuery]CompanyRequestParameters requestParameters,
            [FromBody][ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            var companies = await _services.CompanyService
                .GetCompaniesByIds(ids, new LinkCompaniesParameters(requestParameters, HttpContext));

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(companies.metaData));

            return Ok(companies.response.HasLinks ? companies.response.LinkedEntities : companies.response.ShapedEntities);
        }

        [HttpGet("{id:guid}")]
        [HttpHead("{id:guid}")]
        [HttpCacheValidation(MustRevalidate = false)]
        [HttpCacheExpiration(MaxAge = 600)]
        public async Task<ActionResult> GetCompany(Guid id)
        {
            GetCompanyDto company = await _services.CompanyService.GetCompany(id);
            return Ok(company);
        }

        [HttpPost]
        [ValidateArguments]
        public async Task<ActionResult> CreateCompany(CreateCompanyDto createDto)
        {
            GetCompanyDto result = await _services.CompanyService.CreateCompany(createDto);
            return CreatedAtAction(nameof(GetCompany), new { id = result.Id }, result);
        }

        [HttpPost("collection")]
        [ValidateArguments]
        public async Task<ActionResult> CreateCompanies(IEnumerable<CreateCompanyDto> createDtos)
        {
            IEnumerable<GetCompanyDto> result = await _services.CompanyService.CreateCompaniesCollection(createDtos);
            string ids = string.Join(',', result.Select(c => c.Id.ToString()));
            return CreatedAtAction(nameof(GetCompaniesByIds), new { ids = ids }, result);
        }

        [HttpPut("{id:guid}")]
        [ValidateArguments]
        public async Task<ActionResult> UpdateCompany(Guid id, UpdateCompanyDto updateDto)
        {
            await _services.CompanyService.UpdateCompany(id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteCompany(Guid id)
        {
            await _services.CompanyService.DeleteCompany(id);
            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult> PartiallyUpdateCompany(Guid id, JsonPatchDocument<UpdateCompanyDto> patchDoc)
        {
            if (patchDoc is null)
            {
                return BadRequest();
            }

            var toPatch = await _services.CompanyService.GetCompanyForPatch(id);

            patchDoc.ApplyTo(toPatch.dto, ModelState);
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            await _services.CompanyService.SaveChangesForPatch(toPatch.dto, toPatch.entity);
            return NoContent();
        }

        [HttpOptions]
        public ActionResult GetCompaniesOptions()
        {
            HttpContext.Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }

        [HttpOptions("{id:guid}")]
        public ActionResult GetCompanyOptions()
        {
            HttpContext.Response.Headers.Add("Allow", "GET, OPTIONS, PUT, PATCH, DELETE");
            return Ok();
        }
    }
}

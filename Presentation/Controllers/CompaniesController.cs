using Application.Commands.Companies;
using Application.Commands.Company;
using Application.Queries.Companies;
using Entities.LinkModels;
using Marvin.Cache.Headers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Presentation.ModelBinders;
using Shared.DTO.Company;
using Shared.DTO.RequestFeatures;
using System.Text.Json;

namespace Presentation.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/companies")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class CompaniesController : ControllerBase
    {
        private readonly ISender _sender;

        public CompaniesController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Gets list of companies
        /// </summary>
        /// <returns>List of companies</returns>
        /// <response code="200">Returns list of companies</response>
        /// <response code="401">If client unauthorized</response>
        [HttpGet]
        [HttpHead]
        [ValidateMediaType]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType((StatusCodes.Status401Unauthorized))]
        public async Task<ActionResult> GetCompanies([FromQuery]CompanyRequestParameters requestParameters)
        {
            var query = new GetCompaniesQuery(new LinkCompaniesParameters(requestParameters, HttpContext));
            var companies = await _sender.Send(query);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(companies.metaData));

            return Ok(companies.response.HasLinks ? companies.response.LinkedEntities : companies.response.ShapedEntities);
        }

        [HttpGet("collection/({ids})")]
        [HttpHead("collection/({ids})")]
        [ValidateMediaType]
        public async Task<ActionResult> GetCompaniesByIds([FromQuery]CompanyRequestParameters requestParameters,
            [FromBody][ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            var query = new GetCompaniesByIdsQuery(ids, new LinkCompaniesParameters(requestParameters, HttpContext));
            var companies = await _sender.Send(query);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(companies.metaData));

            return Ok(companies.response.HasLinks ? companies.response.LinkedEntities : companies.response.ShapedEntities);
        }

        [HttpGet("{id:guid}")]
        [HttpHead("{id:guid}")]
        [HttpCacheValidation(MustRevalidate = true)]
        [HttpCacheExpiration(MaxAge = 600)]
        public async Task<ActionResult> GetCompany(Guid id)
        {
            var query = new GetCompanyQuery(id);
            GetCompanyDto company = await _sender.Send(query);
            return Ok(company);
        }

        [HttpPost]
        [ValidateArguments]
        public async Task<ActionResult> CreateCompany(CreateCompanyDto createDto)
        {
            var command = new CreateCompanyCommand(createDto);
            GetCompanyDto result = await _sender.Send(command);
            return CreatedAtAction(nameof(GetCompany), new { id = result.Id }, result);
        }

        [HttpPost("collection")]
        [ValidateArguments]
        public async Task<ActionResult> CreateCompanies(IEnumerable<CreateCompanyDto> createDtos)
        {
            var command = new CreateCompaniesCollectionCommand(createDtos);
            IEnumerable<GetCompanyDto> result = await _sender.Send(command);
            string ids = string.Join(',', result.Select(c => c.Id.ToString()));
            return CreatedAtAction(nameof(GetCompaniesByIds), new { ids = ids }, result);
        }

        [HttpPut("{id:guid}")]
        [ValidateArguments]
        public async Task<ActionResult> UpdateCompany(Guid id, UpdateCompanyDto updateDto)
        {
            var command = new UpdateCompanyCommand(id, updateDto);
            await _sender.Send(command);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteCompany(Guid id)
        {
            var command = new DeleteCompanyCommand(id);
            await _sender.Send(command);
            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult> PartiallyUpdateCompany(Guid id, JsonPatchDocument<UpdateCompanyDto> patchDoc)
        {
            if (patchDoc is null)
            {
                return BadRequest();
            }

            var getQuery = new GetCompanyForPatchQuery(id);
            var toPatch = await _sender.Send(getQuery);

            patchDoc.ApplyTo(toPatch.dto, ModelState);
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var saveCommand = new SaveChangesForPatchCommand(toPatch.dto, toPatch.entity);
            await _sender.Send(saveCommand);
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

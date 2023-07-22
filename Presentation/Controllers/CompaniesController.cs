using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Presentation.ModelBinders;
using Service.Contracts;
using Shared.DTO.Company;
using Shared.DTO.RequestFeatures;
using System.Text.Json;

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
        public async Task<ActionResult> GetCompanies([FromQuery]CompanyRequestParameters requestParameters)
        {
            var companies = await _services.CompanyService.GetCompanies(requestParameters);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(companies.metaData));

            return Ok(companies.items);
        }

        [HttpGet("collection/({ids})")]
        public async Task<ActionResult> GetCompaniesByIds([FromQuery]CompanyRequestParameters requestParameters,
            [FromBody][ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            var companies = await _services.CompanyService.GetCompaniesByIds(ids, requestParameters);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(companies.metaData));

            return Ok(companies.items);
        }

        [HttpGet("{id:guid}")]
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
    }
}

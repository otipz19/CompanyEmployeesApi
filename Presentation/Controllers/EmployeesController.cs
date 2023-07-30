using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.Employee;
using Presentation.ActionFilters;
using System.Text.Json;
using Shared.DTO.RequestFeatures;
using Entities.LinkModels;

namespace Presentation.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/companies/{companyId}/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _services;

        public EmployeesController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        [HttpHead]
        [ValidateMediaType]
        public async Task<ActionResult> GetEmployeesOfCompany(Guid companyId,
            [FromQuery] EmployeeRequestParameters requestParameters)
        {
            var result = await _services.EmployeeService
                .GetEmployeesOfCompany(companyId, new LinkEmployeesParameters(requestParameters, HttpContext));

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));

            return Ok(result.response.HasLinks ? result.response.LinkedEntities : result.response.ShapedEntities);
        }

        [HttpGet("{id:guid}")]
        [HttpHead("{id:guid}")]
        public async Task<ActionResult> GetEmployeeOfCompany(Guid companyId, Guid id)
        {
            GetEmployeeDto employee = await _services.EmployeeService.GetEmployeeOfCompany(companyId, id);
            return Ok(employee);
        }

        [HttpPost]
        [ValidateArguments]
        public async Task<ActionResult> CreateEmployee(Guid companyId, CreateEmployeeDto createDto)
        {
            GetEmployeeDto result = await _services.EmployeeService.CreateEmployeeOfCompany(createDto, companyId);
            return CreatedAtAction(nameof(GetEmployeeOfCompany), new { companyId = companyId, id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        [ValidateArguments]
        public async Task<ActionResult> UpdateEmployee(Guid companyId, Guid id, UpdateEmployeeDto updateDto)
        {
            await _services.EmployeeService.UpdateEmployeeOfCompany(companyId, id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteEmployee(Guid companyId, Guid id)
        {
            await _services.EmployeeService.DeleteEmployeeOfCompany(companyId, id);
            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult> PartiallyUpdateEmployee(Guid companyId, Guid id,
            JsonPatchDocument<UpdateEmployeeDto>? patchDoc)
        {
            if (patchDoc is null)
            {
                return BadRequest();
            }

            var toPatch = await _services.EmployeeService.GetEmployeeForPatch(companyId, id);

            patchDoc.ApplyTo(toPatch.dto, ModelState);
            TryValidateModel(toPatch.dto);
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            await _services.EmployeeService.SaveChangesForPatch(toPatch.dto, toPatch.entity);
            return NoContent();
        }

        [HttpOptions]
        public ActionResult GetEmployeesOptions()
        {
            HttpContext.Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }

        [HttpOptions("{id:guid}")]
        public ActionResult GetEmployeeOptions()
        {
            HttpContext.Response.Headers.Add("Allow", "GET, OPTIONS, PUT, PATCH, DELETE");
            return Ok();
        }
    }
}

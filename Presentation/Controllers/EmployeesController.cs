using Application.Commands.Employees;
using Application.Queries.Employees;
using Entities.LinkModels;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Shared.DTO.Employee;
using Shared.DTO.RequestFeatures;
using System.Text.Json;

namespace Presentation.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/companies/{companyId}/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly ISender _sender;

        public EmployeesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [HttpHead]
        [ValidateMediaType]
        public async Task<ActionResult> GetEmployeesOfCompany(Guid companyId,
            [FromQuery] EmployeeRequestParameters requestParameters)
        {
            var query = new GetEmployeesQuery(companyId, new LinkEmployeesParameters(requestParameters, HttpContext));
            var result = await _sender.Send(query);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));

            return Ok(result.response.HasLinks ? result.response.LinkedEntities : result.response.ShapedEntities);
        }

        [HttpGet("{id:guid}")]
        [HttpHead("{id:guid}")]
        public async Task<ActionResult> GetEmployeeOfCompany(Guid companyId, Guid id)
        {
            var query = new GetEmployeeQuery(companyId, id);
            GetEmployeeDto employee = await _sender.Send(query);
            return Ok(employee);
        }

        [HttpPost]
        [ValidateArguments]
        public async Task<ActionResult> CreateEmployee(Guid companyId, CreateEmployeeDto createDto)
        {
            var command = new CreateEmployeeCommand(companyId, createDto);
            GetEmployeeDto result = await _sender.Send(command);
            return CreatedAtAction(nameof(GetEmployeeOfCompany), new { companyId = companyId, id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        [ValidateArguments]
        public async Task<ActionResult> UpdateEmployee(Guid companyId, Guid id, UpdateEmployeeDto updateDto)
        {
            var command = new UpdateEmployeeCommand(companyId, id, updateDto);
            await _sender.Send(command);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteEmployee(Guid companyId, Guid id)
        {
            var command = new DeleteEmployeeCommand(companyId, id);
            await _sender.Send(command);
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

            var getQuery = new GetEmployeeForPatchQuery(companyId, id);
            var toPatch = await _sender.Send(getQuery);

            patchDoc.ApplyTo(toPatch.dto, ModelState);
            TryValidateModel(toPatch.dto);
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var saveCommand = new SaveChangesForPatchCommand(toPatch.dto, toPatch.entity);
            await _sender.Send(saveCommand);
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

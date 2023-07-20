using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.Employee;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

namespace Presentation.Controllers
{
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
        public async Task<ActionResult> GetEmployeesForCompany(Guid companyId)
        {
            IEnumerable<GetEmployeeDto> employees = await _services.EmployeeService.GetAllEmployeesOfCompany(companyId);
            return Ok(employees);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetEmployeeForCompany(Guid companyId, Guid id)
        {
            GetEmployeeDto employee = await _services.EmployeeService.GetEmployeeOfCompany(companyId, id);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult> CreateEmployee(Guid companyId, CreateEmployeeDto? createDto)
        {
            if (createDto is null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            GetEmployeeDto result = await _services.EmployeeService.CreateEmployeeOfCompany(createDto, companyId);
            return CreatedAtAction(nameof(GetEmployeeForCompany), new { companyId = companyId, id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateEmployee(Guid companyId, Guid id, UpdateEmployeeDto? updateDto)
        {
            if(updateDto is null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

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
            JsonPatchDocument<UpdateEmployeeDto> patchDoc)
        {
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
    }
}

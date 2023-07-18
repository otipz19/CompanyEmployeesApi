using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.Employee;

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
            IEnumerable<GetEmployeeDto> employees = await _services.EmployeeService
                .GetAllEmployeesForCompany(companyId, asNoTracking: true);
            return Ok(employees);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetEmployeeForCompany(Guid companyId, Guid id)
        {
            GetEmployeeDto employee = await _services.EmployeeService
                .GetEmployeeForCompany(companyId, id, asNoTracking: true);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult> CreateEmployee(CreateEmployeeDto createDto, Guid companyId)
        {
            GetEmployeeDto result = await _services.EmployeeService.CreateEmployeeForCompany(createDto, companyId);
            return CreatedAtAction(nameof(GetEmployeeForCompany), new { companyId = companyId, id = result.Id }, result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteEmployee(Guid companyId, Guid id)
        {
            await _services.EmployeeService.DeleteEmployeeForCompany(companyId, id);
            return NoContent();
        }
    }
}

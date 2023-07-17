﻿using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO;

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
            IEnumerable<EmployeeDto> employees = await _services.EmployeeService
                .GetAllEmployeesForCompany(companyId, asNoTracking: true);
            return Ok(employees);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetEmployeeForCompany(Guid companyId, Guid id)
        {
            EmployeeDto employee = await _services.EmployeeService
                .GetEmployeeForCompany(companyId, id, asNoTracking: true);
            return Ok(employee);
        }
    }
}

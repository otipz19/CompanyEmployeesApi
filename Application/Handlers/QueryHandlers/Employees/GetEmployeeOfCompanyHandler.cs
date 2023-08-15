﻿using Application.Queries.Employees;
using AutoMapper;
using Entities.Models;
using MediatR;
using Service.Contracts.GetHelpers;
using Shared.DTO.Employee;

namespace Application.Handlers.QueryHandlers.Employees
{
    internal sealed class GetEmployeeOfCompanyHandler
        : IRequestHandler<GetEmployeeOfCompanyQuery, GetEmployeeDto>
    {
        private readonly IGetCompanyHelper _getCompanyHelper;
        private readonly IGetEmployeeHelper _getEmployeeHelper;
        private readonly IMapper _mapper;

        public GetEmployeeOfCompanyHandler(
            IGetCompanyHelper getCompanyHelper,
            IGetEmployeeHelper getEmployeeHelper,
            IMapper mapper)
        {
            _getCompanyHelper = getCompanyHelper;
            _getEmployeeHelper = getEmployeeHelper;
            _mapper = mapper;
        }

        public async Task<GetEmployeeDto> Handle(GetEmployeeOfCompanyQuery request, CancellationToken cancellationToken)
        {
            await _getCompanyHelper.GetCompanyIfExistsAsNoTracking(request.CompanyId);

            Employee employee = await _getEmployeeHelper.GetEmployeeIfExists(request.CompanyId, request.EmployeeId);

            GetEmployeeDto employeeDto = _mapper.Map<GetEmployeeDto>(employee);
            return employeeDto;
        }
    }
}

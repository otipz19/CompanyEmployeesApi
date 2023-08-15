using Application.Queries.Employees;
using AutoMapper;
using Entities.Models;
using MediatR;
using Service.Contracts.GetHelpers;
using Shared.DTO.Employee;

namespace Application.Handlers.QueryHandlers.Employees
{
    internal sealed class GetEmployeeForPatchHandler
        : IRequestHandler<GetEmployeeForPatchQuery, (UpdateEmployeeDto dto, Employee entity)>
    {
        private readonly IGetCompanyHelper _getCompanyHelper;
        private readonly IGetEmployeeHelper _getEmployeeHelper;
        private readonly IMapper _mapper;

        public GetEmployeeForPatchHandler(IGetCompanyHelper getCompanyHelper, IGetEmployeeHelper employeeHelper, IMapper mapper)
        {
            _getCompanyHelper = getCompanyHelper;
            _getEmployeeHelper = employeeHelper;
            _mapper = mapper;
        }

        public async Task<(UpdateEmployeeDto dto, Employee entity)> Handle(
            GetEmployeeForPatchQuery request,
            CancellationToken cancellationToken)
        {
            await _getCompanyHelper.GetCompanyIfExistsAsNoTracking(request.CompanyId);

            Employee entity = await _getEmployeeHelper.GetEmployeeIfExists(request.CompanyId, request.EmployeeId);

            UpdateEmployeeDto dto = _mapper.Map<UpdateEmployeeDto>(entity);
            return (dto, entity);
        }
    }
}

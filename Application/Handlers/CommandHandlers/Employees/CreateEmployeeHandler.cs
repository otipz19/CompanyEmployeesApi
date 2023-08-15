using Application.Commands.Employees;
using AutoMapper;
using Contracts.Repository;
using Entities.Models;
using MediatR;
using Service.Contracts.GetHelpers;
using Shared.DTO.Employee;

namespace Application.Handlers.CommandHandlers.Employees
{
    internal sealed class CreateEmployeeHandler
        : IRequestHandler<CreateEmployeeCommand, GetEmployeeDto>
    {
        private readonly IGetCompanyHelper _getCompanyHelper;
        private readonly IRepositoryManager _repositories;
        private readonly IMapper _mapper;

        public CreateEmployeeHandler(
            IGetCompanyHelper getCompanyHelper,
            IRepositoryManager repositories,
            IMapper mapper)
        {
            _getCompanyHelper = getCompanyHelper;
            _repositories = repositories;
            _mapper = mapper;
        }

        public async Task<GetEmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _getCompanyHelper.GetCompanyIfExistsAsNoTracking(request.CompanyId);

            Employee entity = _mapper.Map<Employee>(request.Dto);

            _repositories.Employees.CreateEmployee(entity, request.CompanyId);
            await _repositories.SaveChangesAsync();

            GetEmployeeDto getDto = _mapper.Map<GetEmployeeDto>(entity);
            return getDto;
        }
    }
}

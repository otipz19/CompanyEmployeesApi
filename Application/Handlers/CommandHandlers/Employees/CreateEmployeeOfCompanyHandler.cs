using Application.Commands.Employees;
using AutoMapper;
using Contracts.Repository;
using Entities.Models;
using MediatR;
using Service.Contracts.GetHelpers;
using Shared.DTO.Employee;

namespace Application.Handlers.CommandHandlers.Employees
{
    internal sealed class CreateEmployeeOfCompanyHandler
        : IRequestHandler<CreateEmployeeOfCompanyCommand, GetEmployeeDto>
    {
        private readonly IGetCompanyHelper _getCompanyHelper;
        private readonly IRepositoryManager _repositories;
        private readonly IMapper _mapper;

        public CreateEmployeeOfCompanyHandler(
            IGetCompanyHelper getCompanyHelper,
            IRepositoryManager repositories,
            IMapper mapper)
        {
            _getCompanyHelper = getCompanyHelper;
            _repositories = repositories;
            _mapper = mapper;
        }

        public async Task<GetEmployeeDto> Handle(CreateEmployeeOfCompanyCommand request, CancellationToken cancellationToken)
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

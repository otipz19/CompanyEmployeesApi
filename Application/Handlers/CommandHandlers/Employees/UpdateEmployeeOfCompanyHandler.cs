using Application.Commands.Employees;
using AutoMapper;
using Contracts.Repository;
using Entities.Models;
using MediatR;
using Service.Contracts.GetHelpers;

namespace Application.Handlers.CommandHandlers.Employees
{
    internal sealed class UpdateEmployeeOfCompanyHandler
        : IRequestHandler<UpdateEmployeeOfCompanyCommand>
    {
        private readonly IRepositoryManager _repositories;
        private readonly IGetCompanyHelper _getCompanyHelper;
        private readonly IGetEmployeeHelper _getEmployeeHelper;
        private readonly IMapper _mapper;

        public UpdateEmployeeOfCompanyHandler(
            IRepositoryManager repositories,
            IGetCompanyHelper getCompanyHelper,
            IGetEmployeeHelper getEmployeeHelper,
            IMapper mapper)
        {
            _repositories = repositories;
            _getCompanyHelper = getCompanyHelper;
            _getEmployeeHelper = getEmployeeHelper;
            _mapper = mapper;
        }

        public async Task Handle(UpdateEmployeeOfCompanyCommand request, CancellationToken cancellationToken)
        {
            await _getCompanyHelper.GetCompanyIfExistsAsNoTracking(request.CompanyId);

            Employee entity = await _getEmployeeHelper.GetEmployeeIfExists(request.CompanyId, request.EmployeeId);

            _mapper.Map(request.Dto, entity);

            await _repositories.SaveChangesAsync();
        }
    }
}

using Application.Commands.Employees;
using Contracts.Repository;
using Entities.Models;
using MediatR;
using Service.Contracts.GetHelpers;

namespace Application.Handlers.CommandHandlers.Employees
{
    internal sealed class DeleteEmployeeHandler
        : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IGetEmployeeHelper _getEmployeeHelper;
        private readonly IGetCompanyHelper _getCompanyHelper;
        private readonly IRepositoryManager _repositories;

        public DeleteEmployeeHandler(
            IGetEmployeeHelper getEmployeeHelper,
            IGetCompanyHelper getCompanyHelper,
            IRepositoryManager repositories)
        {
            _getEmployeeHelper = getEmployeeHelper;
            _getCompanyHelper = getCompanyHelper;
            _repositories = repositories;
        }

        public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _getCompanyHelper.GetCompanyIfExistsAsNoTracking(request.CompanyId);
            Employee entity = await _getEmployeeHelper.GetEmployeeIfExists(request.CompanyId, request.EmployeeId);

            _repositories.Employees.DeleteEmployee(entity);
            await _repositories.SaveChangesAsync(); 
        }
    }
}

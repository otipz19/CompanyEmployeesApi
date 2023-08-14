using Application.Commands.Companies;
using Contracts.Repository;
using Entities.Models;
using MediatR;
using Service.Contracts.GetHelpers;

namespace Application.Handlers.CommandHandlers.Companies
{
    internal sealed class DeleteCompanyHandler : IRequestHandler<DeleteCompanyCommand>
    {
        private readonly IRepositoryManager _repositories;
        private readonly IGetCompanyHelper _getHelper;

        public DeleteCompanyHandler(IRepositoryManager repositories, IGetCompanyHelper getHelper)
        {
            _repositories = repositories;
            _getHelper = getHelper;
        }

        public async Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            Company company = await _getHelper.GetCompanyIfExists(request.Id);

            _repositories.Companies.DeleteCompany(company);
            await _repositories.SaveChangesAsync();
        }
    }
}

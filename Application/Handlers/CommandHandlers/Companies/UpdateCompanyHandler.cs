using Application.Commands.Companies;
using AutoMapper;
using Contracts.Repository;
using Entities.Models;
using MediatR;
using Service.Contracts.GetHelpers;

namespace Application.Handlers.CommandHandlers.Companies
{
    internal sealed class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand>
    {
        private readonly IRepositoryManager _repositories;
        private readonly IMapper _mapper;
        private readonly IGetCompanyHelper _getHelper;

        public UpdateCompanyHandler(IRepositoryManager repositories, IGetCompanyHelper getHelper, IMapper mapper)
        {
            _repositories = repositories;
            _getHelper = getHelper;
            _mapper = mapper;
        }

        public async Task Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            Company company = await _getHelper.GetCompanyIfExists(request.Id);

            _mapper.Map(request.Dto, company);
            await _repositories.SaveChangesAsync();
        }
    }
}

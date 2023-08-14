using Application.Commands.Companies;
using AutoMapper;
using Contracts.Repository;
using MediatR;

namespace Application.Handlers.CommandHandlers.Companies
{
    internal sealed class SaveChangesForPatchHandler : IRequestHandler<SaveChangesForPatchCommand>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositories;

        public SaveChangesForPatchHandler(IMapper mapper, IRepositoryManager repositories)
        {
            _mapper = mapper;
            _repositories = repositories;
        }

        public async Task Handle(SaveChangesForPatchCommand request, CancellationToken cancellationToken)
        {
            _mapper.Map(request.Dto, request.Entity);
            await _repositories.SaveChangesAsync();
        }
    }
}

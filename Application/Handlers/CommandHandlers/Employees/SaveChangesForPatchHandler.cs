using Application.Commands.Employees;
using AutoMapper;
using Contracts.Repository;
using MediatR;

namespace Application.Handlers.CommandHandlers.Employees
{
    internal sealed class SaveChangesForPatchHandler
        : IRequestHandler<SaveChangesForPatchCommand>
    {
        private readonly IRepositoryManager _repositories;
        private readonly IMapper _mapper;

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

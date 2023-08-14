using Application.Commands.Company;
using AutoMapper;
using Contracts.Repository;
using Entities.Models;
using MediatR;
using Shared.DTO.Company;

namespace Application.Handlers.CommandHandlers.Companies
{
    internal sealed class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, GetCompanyDto>
    {
        private readonly IRepositoryManager _repositories;
        private readonly IMapper _mapper;

        public CreateCompanyHandler(IMapper mapper, IRepositoryManager repositories)
        {
            _mapper = mapper;
            _repositories = repositories;
        }

        public async Task<GetCompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = _mapper.Map<Company>(request.Dto);

            _repositories.Companies.CreateCompany(company);
            await _repositories.SaveChangesAsync();

            return _mapper.Map<GetCompanyDto>(company);
        }
    }
}

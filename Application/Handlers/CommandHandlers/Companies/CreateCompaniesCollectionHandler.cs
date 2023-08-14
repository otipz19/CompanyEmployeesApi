using Application.Commands.Companies;
using AutoMapper;
using Contracts.Repository;
using Entities.Exceptions;
using Entities.Models;
using MediatR;
using Shared.DTO.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.CommandHandlers.Companies
{
    internal sealed class CreateCompaniesCollectionHandler
        : IRequestHandler<CreateCompaniesCollectionCommand, IEnumerable<GetCompanyDto>>
    {
        private readonly IRepositoryManager _repositories;
        private readonly IMapper _mapper;

        public CreateCompaniesCollectionHandler(IRepositoryManager repositories, IMapper mapper)
        {
            _repositories = repositories;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetCompanyDto>> Handle(
            CreateCompaniesCollectionCommand request,
            CancellationToken cancellationToken)
        {
            if (request.Dtos is null)
                throw new CompaniesCollectionBadRequest();

            IEnumerable<Company> companies = _mapper.Map<IEnumerable<Company>>(request.Dtos);

            foreach (var company in companies)
            {
                _repositories.Companies.CreateCompany(company);
            }
            await _repositories.SaveChangesAsync();

            return _mapper.Map<IEnumerable<GetCompanyDto>>(companies);
        }
    }
}

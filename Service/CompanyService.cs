﻿using AutoMapper;
using Contracts.LoggerService;
using Contracts.Repository;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DTO;

namespace Service
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repositories;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompanyService(ILoggerManager loggerManager,
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _logger = loggerManager;
            _repositories = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompanies(bool asNoTracking)
        {
            var companies = await _repositories.Companies.GetAllCompanies(asNoTracking);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return companiesDto;
        }

        public async Task<CompanyDto> GetCompany(Guid id, bool asNoTracking)
        {
            var company = await _repositories.Companies.GetCompany(id, asNoTracking);

            if(company is null)
            {
                throw new CompanyNotFoundException(id);
            }

            var companieDto = _mapper.Map<CompanyDto>(company);
            return companieDto;
        }
    }
}

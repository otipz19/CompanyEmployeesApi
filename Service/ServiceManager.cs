﻿using AutoMapper;
using Service.Contracts.DataShaping;
using Contracts.Repository;
using Service.Contracts;
using Contracts.Hateoas;
using Microsoft.AspNetCore.Identity;
using Entities.Models;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(
            IRepositoryManager repositoryManager,
            IMapper mapper,
            IDataShaper dataShaper,
            IEmployeeLinksGenerator employeeLinksGenerator,
            ICompanyLinksGenerator companyLinksGenerator,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _employeeService = new Lazy<IEmployeeService>(
                () => new EmployeeService(repositoryManager, mapper, dataShaper, employeeLinksGenerator));
            _companyService = new Lazy<ICompanyService>(
                () => new CompanyService(repositoryManager, mapper, dataShaper, companyLinksGenerator));
            _authenticationService = new Lazy<IAuthenticationService>(
                () => new AuthenticationService(userManager, mapper, roleManager));
        }

        public ICompanyService CompanyService => _companyService.Value;

        public IEmployeeService EmployeeService => _employeeService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}

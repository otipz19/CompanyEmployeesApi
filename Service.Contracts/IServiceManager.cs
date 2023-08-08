namespace Service.Contracts
{
    public interface IServiceManager
    {
        public ICompanyService CompanyService { get; }

        public IEmployeeService EmployeeService { get; }

        public IAuthenticationService AuthenticationService { get; }
    }
}

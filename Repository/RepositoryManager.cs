using Contracts.Repository;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<ICompanyRepository> _companyRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _companyRepository = new Lazy<ICompanyRepository> (() => new CompanyRepository(context));
            _employeeRepository = new Lazy<IEmployeeRepository> (() => new EmployeeRepository(context));
        }

        public ICompanyRepository Companies => _companyRepository.Value;

        public IEmployeeRepository Employees => _employeeRepository.Value;

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

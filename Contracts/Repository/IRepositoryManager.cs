namespace Contracts.Repository
{
    public interface IRepositoryManager
    {
        public ICompanyRepository Companies { get; }

        public IEmployeeRepository Employees { get; }

        public void SaveChanges();

        public Task SaveChangesAsync();
    }
}

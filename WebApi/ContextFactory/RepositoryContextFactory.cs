using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;

namespace WebApi.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var builder = new DbContextOptionsBuilder<RepositoryContext>();
            builder.UseSqlServer(config.GetConnectionString("localhost"),
                builder =>
                {
                    builder.MigrationsAssembly("WebApi");
                });

            return new RepositoryContext(builder.Options);
        }
    }
}

using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(
                new Employee()
                {
                    Id = Guid.NewGuid(),
                    Name = "Josh",
                    Age = 100,
                    Position = "Lazy man",
                    CompanyId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                new Employee()
                {
                    Id = Guid.NewGuid(),
                    Name = "John",
                    Age = 27,
                    Position = "Developer",
                    CompanyId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                new Employee()
                {
                    Id = Guid.NewGuid(),
                    Name = "Matt",
                    Age = 25,
                    Position = "QA",
                    CompanyId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                },
                new Employee()
                {
                    Id = Guid.NewGuid(),
                    Name = "Frank",
                    Age = 28,
                    Position = "Lazy man",
                    CompanyId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                },
                new Employee()
                {
                    Id = Guid.NewGuid(),
                    Name = "Bob",
                    Age = 31,
                    Position = "Developer",
                    CompanyId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                },
                new Employee()
                {
                    Id = Guid.NewGuid(),
                    Name = "Jade",
                    Age = 25,
                    Position = "QA",
                    CompanyId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                });
        }
    }
}

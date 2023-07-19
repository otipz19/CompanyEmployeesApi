using Shared.DTO.Employee;

namespace Shared.DTO.Company
{
    public record UpdateCompanyDto
    {
        public string? Name { get; init; }

        public string? Address { get; init; }

        public string? Country { get; init; }

        public IEnumerable<CreateEmployeeDto>? Employees { get; init; }
    }
}

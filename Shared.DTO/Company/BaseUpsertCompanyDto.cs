using Shared.DTO.Employee;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Company
{
    public abstract record BaseUpsertCompanyDto
    {
        [Required]
        [MaxLength(60)]
        public string? Name { get; init; }

        [Required]
        [MaxLength(60)]
        public string? Address { get; init; }

        public string? Country { get; init; }

        public IEnumerable<CreateEmployeeDto>? Employees { get; init; }
    }
}

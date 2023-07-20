using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Employee
{
    public abstract record BaseUpsertEmployeeDto
    {
        [Required]
        [MaxLength(30)]
        public string? Name { get; init; }

        [Required]
        public int? Age { get; init; }

        [Required]
        [MaxLength(20)]
        public string? Position { get; init; }
    }
}

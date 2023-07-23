using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Employee
    {
        [Column("EmployeeId")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        [Required]
        [MaxLength(20)]
        public string Position { get; set; } = string.Empty;

        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }

        public Company? Company { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Authentication
{
    public record RegisterUserDto
    {
        [Required]
        public string UserName { get; init; } = default!;

        [Required]
        public string Password { get; init; } = default!;

        public string? FirstName { get; init; }

        public string? LastName { get; init; }

        public string? Email { get; init; }

        public string? PhoneNumber { get; init; }

        public ICollection<string> Roles { get; init; } = new List<string>();
    }
}

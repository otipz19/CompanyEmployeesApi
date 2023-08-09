using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Authentication
{
    public class AuthenticateUserDto
    {
        [Required]
        public string UserName { get; init; } = default!;

        [Required]
        public string Password { get; init; } = default!;
    }
}

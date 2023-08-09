namespace Shared.DTO.Options
{
    public class JwtSettings
    {
        public string ValidIssuer { get; set; } = default!;

        public string ValidAudience { get; set; } = default!;

        public string SecretKey { get; set; } = default!;

        public int ExpiresInMinutes { get; set; }
    }
}

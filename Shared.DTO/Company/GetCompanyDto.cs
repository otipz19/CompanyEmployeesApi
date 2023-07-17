namespace Shared.DTO.Company
{
    public record GetCompanyDto
    {
        public Guid Id { get; init; }

        public string? Name { get; init; }

        public string? FullAddress { get; init; }
    }
}
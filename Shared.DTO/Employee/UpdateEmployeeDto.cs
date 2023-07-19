namespace Shared.DTO.Employee
{
    public record UpdateEmployeeDto
    {
        public string? Name { get; init; }

        public int Age { get; init; }

        public string? Position { get; init; }
    }
}

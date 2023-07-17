namespace Shared.DTO.Employee
{
    public record CreateEmployeeDto
    {
        public string? Name { get; init; }

        public string? Position { get; init; }

        public int Age { get; init; }
    }
}

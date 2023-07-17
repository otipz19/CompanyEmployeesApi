namespace Shared.DTO.Employee
{
    public record GetEmployeeDto
    {
        public Guid Id { get; init; }

        public string? Name { get; init; }

        public int Age { get; init; }

        public string? Position { get; init; }
    }
}

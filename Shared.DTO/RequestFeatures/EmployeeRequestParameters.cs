namespace Shared.DTO.RequestFeatures
{
    public class EmployeeRequestParameters : BaseRequestParameters
    {
        public uint MinAge { get; init; }

        public uint MaxAge { get; init; } = int.MaxValue;

        public bool IsValidAgeRange => MinAge < MaxAge;

        public string? SearchTerm { get; init; }
    }
}

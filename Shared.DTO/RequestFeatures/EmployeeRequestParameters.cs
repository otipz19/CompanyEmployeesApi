namespace Shared.DTO.RequestFeatures
{
    public class EmployeeRequestParameters : BaseRequestParameters
    {
        public uint MinAge { get; set; }

        public uint MaxAge { get; set; } = int.MaxValue;

        public bool IsValidAgeRange => MinAge < MaxAge;
    }
}

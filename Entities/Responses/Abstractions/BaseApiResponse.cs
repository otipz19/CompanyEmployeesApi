namespace Entities.Responses.Abstractions
{
    public abstract class BaseApiResponse
    {
        protected BaseApiResponse(bool success)
        {
            Success = success;
        }

        public bool Success { get; init; }
    }
}

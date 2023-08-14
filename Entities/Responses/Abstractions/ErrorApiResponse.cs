namespace Entities.Responses.Abstractions
{
    public abstract class ErrorApiResponse : BaseApiResponse
    {
        protected ErrorApiResponse(string message) : base(false)
        {
            Message = message;
        }

        public string Message { get; init; }
    }
}

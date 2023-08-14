namespace Entities.Responses.Abstractions
{
    public abstract class NotFoundApiResponse : ErrorApiResponse
    {
        protected NotFoundApiResponse(string message) : base(message)
        {
        }
    }
}

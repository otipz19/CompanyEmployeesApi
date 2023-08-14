namespace Entities.Responses.Abstractions
{
    public abstract class BadRequestApiResponse : ErrorApiResponse
    {
        protected BadRequestApiResponse(string message) : base(message)
        {
        }
    }
}

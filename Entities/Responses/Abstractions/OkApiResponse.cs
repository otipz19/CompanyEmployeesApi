namespace Entities.Responses.Abstractions
{
    public class OkApiResponse<TResult> : BaseApiResponse
    {
        public OkApiResponse(TResult result) : base(true)
        {
            Result = result;
        }

        public TResult Result { get; init; }
    }
}

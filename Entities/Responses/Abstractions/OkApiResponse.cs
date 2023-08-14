namespace Entities.Responses.Abstractions
{
    public class OkApiResponse : BaseApiResponse
    {
        public OkApiResponse(object result) : base(true)
        {
            Result = result;
        }

        public object Result { get; init; }
    }
}

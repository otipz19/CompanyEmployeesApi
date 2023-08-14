using Entities.Responses.Abstractions;

namespace Entities.Responses.Common
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

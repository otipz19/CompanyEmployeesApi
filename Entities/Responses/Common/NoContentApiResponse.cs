using Entities.Responses.Abstractions;

namespace Entities.Responses.Common
{
    public class NoContentApiResponse : BaseApiResponse
    {
        public NoContentApiResponse() : base(true)
        {
        }
    }
}

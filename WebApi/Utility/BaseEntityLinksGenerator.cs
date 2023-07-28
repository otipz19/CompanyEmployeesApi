using Contracts.Hateoas;
using Entities.DataShaping;
using Entities.LinkModels;
using Shared.DTO;
using Microsoft.Net.Http.Headers;
using Presentation.Controllers;

namespace WebApi.Utility
{
    public abstract class BaseEntityLinksGenerator
    {    
        protected bool IsHateoasRequested(HttpContext context)
        {
            MediaTypeHeaderValue? mediaType = context.Items[Constants.MediaTypeItemsKey] as MediaTypeHeaderValue;

            return mediaType?.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.OrdinalIgnoreCase) ?? false;
        }

        protected LinkResponse GetShapedEntities(IEnumerable<ShapedObject> items)
        {
            return new LinkResponse()
            {
                ShapedEntities = items,
                HasLinks = false,
            };
        }
    }
}

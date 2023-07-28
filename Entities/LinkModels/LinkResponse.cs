using Entities.DataShaping;

namespace Entities.LinkModels
{
    public class LinkResponse
    {
        public bool HasLinks { get; set; }

        public IEnumerable<ShapedObject> ShapedEntities { get; set; } = default!;

        public LinkCollectionWrapper<ShapedObject> LinkedEntities { get; set; } = default!;
    }
}

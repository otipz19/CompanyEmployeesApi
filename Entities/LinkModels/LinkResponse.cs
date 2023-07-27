using Entities.DataShaping;

namespace Entities.LinkModels
{
    public class LinkResponse
    {
        public bool HasLinks { get; set; }

        public IEnumerable<ShapedEntity> ShapedEntities { get; set; } = default!;

        public LinkCollectionWrapper<ShapedEntity> LinkedEntities { get; set; } = default!;
    }
}

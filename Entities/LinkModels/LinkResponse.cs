using Entities.DataShaping;

namespace Entities.LinkModels
{
    public class LinkResponse
    {
        public bool HasLinks { get; set; }

        public List<ShapedObject> ShapedObjects { get; set; } = new();

        public LinkCollectionWrapper<ShapedObject> LinkedObjects { get; set; } = new();
    }
}

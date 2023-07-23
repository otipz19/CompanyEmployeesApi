namespace Entities.LinkModels
{
    public class LinkCollectionWrapper<T> : BaseLinksResourse
    {
        public LinkCollectionWrapper()
        {
            Value = new List<T>();
        }

        public LinkCollectionWrapper(List<T> value)
        {
            Value = value;
        }

        public List<T> Value { get; init; }
    }
}

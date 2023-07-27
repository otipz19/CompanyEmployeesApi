namespace Entities.LinkModels
{
    public class LinkCollectionWrapper<T> : BaseLinksResourse
    {
        public LinkCollectionWrapper(IEnumerable<T> value)
        {
            Value = value;
        }

        public IEnumerable<T> Value { get; init; }
    }
}

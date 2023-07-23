namespace Entities.LinkModels
{
    public class Link
    {
        public Link(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }

        public string Href { get; init; }

        public string Rel { get; init; }

        public string Method { get; init; }
    }
}

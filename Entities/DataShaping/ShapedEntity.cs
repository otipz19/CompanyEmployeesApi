namespace Entities.DataShaping
{
    public class ShapedEntity
    {
        public ShapedObject ShapedObject { get; set; } = new ShapedObject();

        public Guid Id { get; set; }
    }
}

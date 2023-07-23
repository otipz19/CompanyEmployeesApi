using Entities.DataShaping;

namespace Service.Contracts.DataShaping
{
    public interface IDataShaper
    {
        public IEnumerable<ShapedEntity> ShapeData<T>(IEnumerable<T> items, string? fieldsString);

        public ShapedEntity ShapeData<T>(T item, string? fieldsString);
    }
}

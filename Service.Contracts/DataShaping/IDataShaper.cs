using Entities.DataShaping;

namespace Service.Contracts.DataShaping
{
    public interface IDataShaper
    {
        public IEnumerable<ShapedObject> ShapeData<T>(IEnumerable<T> items, string? fieldsString);

        public ShapedObject ShapeData<T>(T item, string? fieldsString);
    }
}

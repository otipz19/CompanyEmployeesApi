using Service.Contracts.DataShaping;
using System.Dynamic;

namespace Service.Contracts.DataShaping
{
    public interface IDataShaper
    {
        public IEnumerable<IShapedObject> ShapeData<T>(IEnumerable<T> items, string? fieldsString);

        public IShapedObject ShapeData<T>(T item, string? fieldsString);
    }
}

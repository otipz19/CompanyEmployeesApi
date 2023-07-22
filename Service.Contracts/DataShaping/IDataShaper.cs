using System.Dynamic;

namespace Contracts.DataShaping
{
    public interface IDataShaper
    {
        public IEnumerable<ExpandoObject> ShapeData<T>(IEnumerable<T> items, string fieldsString);

        public ExpandoObject ShapeData<T>(T item, string fieldsString);
    }
}

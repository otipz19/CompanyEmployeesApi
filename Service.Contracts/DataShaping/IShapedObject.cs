using System.Dynamic;
using System.Xml.Serialization;

namespace Service.Contracts.DataShaping
{
    public interface IShapedObject : IDictionary<string, object?>, IXmlSerializable
    {
        public bool TryGetMember(GetMemberBinder binder, out object? result);

        public bool TrySetMember(SetMemberBinder binder, object? value);
    }
}

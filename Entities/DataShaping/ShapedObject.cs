using Entities.LinkModels;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Entities.DataShaping
{
    public class ShapedObject : DynamicObject, IDictionary<string, object?>, IXmlSerializable
    {
        private const string RootElement = nameof(ShapedObject);

        private readonly IDictionary<string, object?> _expando;

        public ShapedObject()
        {
            _expando = new ExpandoObject();
        }

        #region DynamicObject members

        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            if(_expando.TryGetValue(binder.Name, out object? value))
            {
                result = value;
                return true;
            }

            return base.TryGetMember(binder, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
            _expando[binder.Name] = value;
            return true;
        }

        #endregion

        #region IXmlSerializable members

        public XmlSchema? GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement(RootElement);

            while (!reader.Name.Equals(RootElement))
            {
                reader.MoveToAttribute("type");
                string typeContent = reader.ReadContentAsString();
                Type? underlyingType = Type.GetType(typeContent);

                if (underlyingType != null)
                {
                    reader.MoveToContent();
                    this[reader.Name] = reader.ReadElementContentAs(underlyingType, null!);
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach(string key in Keys)
            {
                object? value = this[key];
                WriteElementToXml(key, value, writer);
            }
        }

        private void WriteElementToXml(string key, object? value, XmlWriter writer)
        {
            writer.WriteStartElement(key);

            if(value is not null)
            {
                if (value is List<Link> linkList)
                {
                    foreach(Link link in linkList)
                    {
                        writer.WriteStartElement(nameof(Link));

                        WriteElementToXml(nameof(link.Href), link.Href, writer);
                        WriteElementToXml(nameof(link.Rel), link.Rel, writer);
                        WriteElementToXml(nameof(link.Method), link.Method, writer);

                        writer.WriteEndElement();
                    }
                }
                else
                {
                    writer.WriteString(value.ToString());
                }
            }

            writer.WriteEndElement();
        }

        #endregion

        #region IDictionary members

        public object? this[string key]
        {
            get => _expando[key];
            set => _expando[key] = value;
        }

        public ICollection<string> Keys => _expando.Keys;

        public ICollection<object?> Values => _expando.Values;

        public int Count => _expando.Count;

        public bool IsReadOnly => _expando.IsReadOnly;

        public void Add(string key, object? value)
        {
            _expando.Add(key, value);
        }

        public void Add(KeyValuePair<string, object?> item)
        {
            _expando.Add(item);
        }

        public void Clear()
        {
            _expando.Clear();
        }

        public bool Contains(KeyValuePair<string, object?> item)
        {
            return _expando.Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return _expando.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, object?>[] array, int arrayIndex)
        {
            _expando.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, object?>> GetEnumerator()
        {
            return _expando.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Remove(string key)
        {
            return _expando.Remove(key);
        }

        public bool Remove(KeyValuePair<string, object?> item)
        {
            return _expando.Remove(item);
        }

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out object value)
        {
            return _expando.TryGetValue(key, out value);
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace EFQMWeb.Common.Util
{

    public class PureJson
    {
        public StringBuilder StringBuilder { get; set; }

        public PureJson()
        {
            StringBuilder = new StringBuilder();
        }
    }

    public class SPJsonObject : IDisposable
    {
        private JsonKeyValueWriter _json;

        public JsonKeyValueWriter GetWriter
        {
            get { return _json; }
        }

        public SPJsonObject(JsonKeyValueWriter json)
        {
            _json = json;
            _json.WriteStartObject();
        }

        public SPJsonObject(JsonKeyValueWriter json, string name)
        {
            _json = json;
            _json.WritePropertyName(name);
            _json.WriteStartObject();
        }

        public void Add(string key, object value)
        {
            _json.WriteKeyValue(key, value);
        }

        public void Dispose()
        {
            _json.WriteEndObject();
        }
    }

    public class SPJsonArray : IDisposable
    {
        private JsonKeyValueWriter _json;

        public JsonKeyValueWriter GetWriter
        {
            get { return _json; }
        }

        public SPJsonArray(JsonKeyValueWriter json)
        {
            _json = json;
            _json.WriteStartArray();
        }

        public SPJsonArray(JsonKeyValueWriter json, string name)
        {
            _json = json;
            _json.WritePropertyName(name);
            _json.WriteStartArray();
        }

        public void Dispose()
        {
            _json.WriteEndArray();
        }
    }

    public class JsonKeyValueWriter : JsonTextWriter
    {
        public JsonKeyValueWriter(TextWriter textWriter)
            : base(textWriter)
        {
        }

        public JsonKeyValueWriter(StringBuilder builder)
            : base(new StringWriter(builder))
        {
        }

        public void WriteKeyValue(string key, object value)
        {
            if (value != null)
                if (value.GetType() == typeof(Guid))
                {
                    value = ((Guid)value).ToString();
                }

            WritePropertyName(key);
            WriteValue(value);

            // Dodao ML radi beskonačnih muka po javascript parsiranju datuma (13.1.2013)
            if (value is DateTime)
            {
                WriteKeyValue(key + "__Date", ((DateTime)value).ToShortDateString());
                WriteKeyValue(key + "__Time", ((DateTime)value).ToShortTimeString());
            }
        }
    }
}
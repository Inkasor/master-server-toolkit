using MasterServerToolkit.Json;
using MasterServerToolkit.Networking;
using System;
using System.Collections.Generic;

namespace MasterServerToolkit.MasterServer
{
    public class ObservableListFloat : ObservableBaseList<float>
    {
        public ObservableListFloat(ushort key) : base(key) { }

        public ObservableListFloat(ushort key, List<float> defaultValues) : base(key, defaultValues) { }

        public override void Deserialize(string value)
        {
            FromJson(value);
        }

        public override string Serialize()
        {
            return ToJson().ToString();
        }

        public override MstJson ToJson()
        {
            var json = MstJson.EmptyArray;

            foreach (var v in _value)
            {
                json.Add(v);
            }

            return json;
        }

        public override void FromJson(MstJson json)
        {
            _value.Clear();

            foreach (var v in json)
            {
                _value.Add(v.FloatValue);
            }

            MarkAsDirty();
        }

        protected override float ReadValue(EndianBinaryReader reader)
        {
            return reader.ReadSingle();
        }

        protected override void WriteValue(float value, EndianBinaryWriter writer)
        {
            writer.Write(value);
        }

        public override void FromJson(string json)
        {
            FromJson(new MstJson(json));
        }
    }
}

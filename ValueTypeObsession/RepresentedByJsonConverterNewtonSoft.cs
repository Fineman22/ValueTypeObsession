using Newtonsoft.Json;
using System;

namespace ValueTypeObsession
{
    public class RepresentedByJsonConverterNewtonSoft : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.GetType().GetProperty("Value").GetValue(value, null));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var constructorType = objectType.BaseType.GenericTypeArguments[0];
            var value = Convert.ChangeType(serializer.Deserialize(reader), constructorType);
            return Activator.CreateInstance(objectType, value);
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }

}

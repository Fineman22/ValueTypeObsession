using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ValueTypeObsession
{
    public class RepresentedByJsonConverterSystemTextFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(RepresentedByBase).IsAssignableFrom(typeToConvert);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return (JsonConverter)Activator.CreateInstance(
            typeof(RepresentedByJsonConverterSystemText<>).MakeGenericType(typeToConvert));
        }
    }
    public class RepresentedByJsonConverterSystemText<T> : JsonConverter<T>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(T).IsAssignableFrom(typeToConvert);
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var constructorType = typeToConvert.BaseType.GenericTypeArguments[0];
            var valueRaw = JsonSerializer.Deserialize(ref reader, constructorType, options);
            var value = Convert.ChangeType(valueRaw, constructorType);
            return (T)Activator.CreateInstance(typeToConvert, value);
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.GetType().GetProperty("Value").GetValue(value, null), options);
        }
    }

}

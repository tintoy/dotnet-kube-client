using System;
using Newtonsoft.Json;

namespace KubeClient.Models.Converters
{
    /// <summary>
    ///     JSON converter for <see cref="MicroTimeV1"/>.
    /// </summary>
    public class MicroTimeV1Converter
        : JsonConverter
    {
        /// <summary>
        ///     The CLR <see cref="Type"/> corresponding to <see cref="MicroTimeV1"/>.
        /// </summary>
        static readonly Type MicroTimeV1Type = typeof(MicroTimeV1);

        /// <summary>
        ///     Create a new <see cref="MicroTimeV1"/>.
        /// </summary>
        public MicroTimeV1Converter()
        {
        }

        /// <summary>
        ///     Determine wither the converter can convert an object of the specified type to / from JSON.
        /// </summary>
        /// <param name="objectType">
        ///     The target object <see cref="Type"/>.
        /// </param>
        /// <returns>
        ///     <c>true</c>, if the converter can convert an object of the specified type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType) => objectType == MicroTimeV1Type;

        /// <summary>
        ///     Read (deserialise) an object from JSON.
        /// </summary>
        /// <param name="reader">
        ///     A <see cref="JsonReader"/> representing the JSON to read from.
        /// </param>
        /// <param name="objectType">
        ///     The target object <see cref="Type"/>.
        /// </param>
        /// <param name="existingValue">
        ///     The existing value (unused).
        /// </param>
        /// <param name="serializer">
        ///     A <see cref="JsonSerializer"/> that can be used for nested deserialisation.
        /// </param>
        /// <returns>
        ///     The deserialised object.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            
            if (objectType == null)
                throw new ArgumentNullException(nameof(objectType));
            
            if (objectType != MicroTimeV1Type)
                throw new NotSupportedException($"{GetType().FullName} cannot deserialise a value of type '{objectType.FullName}'.");

            switch (reader.TokenType)
            {
                case JsonToken.Null:
                {
                    return null;
                }
                case JsonToken.String:
                {
                    return new MicroTimeV1(
                        DateTime.Parse(
                            (string)reader.Value
                        )
                    );
                }
                default:
                {
                    throw new JsonException($"Unexpected token type '{reader.TokenType}' for {nameof(MicroTimeV1)} (expected one of [{JsonToken.Null}, {JsonToken.Integer}, {JsonToken.String}]).");
                }
            }
        }

        /// <summary>
        ///     Write (serialise) an object to JSON.
        /// </summary>
        /// <param name="writer">
        ///     A <see cref="JsonWriter"/> used to write the JSON.
        /// </param>
        /// <param name="value">
        ///     The value to serialise.
        /// </param>
        /// <param name="serializer">
        ///     A <see cref="JsonSerializer"/> that can be used for nested serialisation.
        /// </param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));
            
            if (serializer == null)
                throw new ArgumentNullException(nameof(serializer));
            
            if (value != null)
            {
                var microTime = (MicroTimeV1)value;
                writer.WriteValue(microTime.Value);
            }
            else
                writer.WriteNull();
        }
    }
}
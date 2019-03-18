using System;
using Newtonsoft.Json;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Converters
{
    /// <summary>
    ///     JSON converter for <see cref="Int32OrStringV1"/>.
    /// </summary>
    public class Int32OrStringV1Converter
        : JsonConverter, IYamlTypeConverter
    {
        /// <summary>
        ///     The CLR <see cref="Type"/> corresponding to <see cref="Int32OrStringV1"/>.
        /// </summary>
        static readonly Type Int32OrStringV1Type = typeof(Int32OrStringV1);

        /// <summary>
        ///     Create a new <see cref="Int32OrStringV1"/>.
        /// </summary>
        public Int32OrStringV1Converter()
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
        public override bool CanConvert(Type objectType) => objectType == Int32OrStringV1Type;

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
            
            if (objectType != Int32OrStringV1Type)
                throw new NotSupportedException($"{GetType().FullName} cannot deserialise a value of type '{objectType.FullName}'.");

            switch (reader.TokenType)
            {
                case JsonToken.Null:
                {
                    return null;
                }
                case JsonToken.Integer:
                {
                    return new Int32OrStringV1((int)(long)reader.Value);
                }
                case JsonToken.String:
                {
                    return new Int32OrStringV1((string)reader.Value);
                }
                default:
                {
                    throw new JsonException($"Unexpected token type '{reader.TokenType}' for {nameof(Int32OrStringV1)} (expected one of [{JsonToken.Null}, {JsonToken.Integer}, {JsonToken.String}]).");
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
            
            var typedValue = (Int32OrStringV1)value;
            if (typedValue.IsInt32)
            {
                writer.WriteValue(
                    (int)typedValue
                );
            }
            else if (typedValue.IsString)
            {
                writer.WriteValue(
                    (string)typedValue
                );
            }
            else
                writer.WriteNull();
        }

        /// <summary>
        ///     Determine whether the converter can convert an object of the specified type to / from YAML.
        /// </summary>
        /// <param name="type">
        ///     The target object <see cref="Type"/>.
        /// </param>
        /// <returns>
        ///     <c>true</c>, if the converter can convert an object of the specified type; otherwise, <c>false</c>.
        /// </returns>
        bool IYamlTypeConverter.Accepts(Type type) => CanConvert(type);

        /// <summary>
        ///     Read (deserialise) an object of the specified type from YAML.
        /// </summary>
        /// <param name="parser">
        ///     An <see cref="IParser"/> used to read the YAML.
        /// </param>
        /// <param name="objectType">
        ///     The target object <see cref="Type"/>.
        /// </param>
        /// <returns>
        ///     The deserialised object.
        /// </returns>
        object IYamlTypeConverter.ReadYaml(IParser parser, Type objectType)
        {
            if (parser == null)
                throw new ArgumentNullException(nameof(parser));

            if (objectType == null)
                throw new ArgumentNullException(nameof(objectType));
            
            if (objectType != Int32OrStringV1Type)
                throw new NotSupportedException($"{GetType().FullName} cannot deserialise a value of type '{objectType.FullName}'.");

            switch (parser.Current)
            {
                case Scalar scalar:
                {
                    if (scalar.Value == null)
                        return null;

                    return new Int32OrStringV1(scalar.Value);
                }
                default:
                {
                    throw new YamlException($"Unexpected parser event '{parser.Current.GetType().Name}'.");
                }
            }
        }

        /// <summary>
        ///     Write (serialise) an object to YAML.
        /// </summary>
        /// <param name="emitter">
        ///     An <see cref="IEmitter"/> used to generate the YAML.
        /// </param>
        /// <param name="value">
        ///     The target object to serialise.
        /// </param>
        /// <param name="objectType">
        ///     The target object <see cref="Type"/>.
        /// </param>
        void IYamlTypeConverter.WriteYaml(IEmitter emitter, object value, Type objectType)
        {
            if (emitter == null)
                throw new ArgumentNullException(nameof(emitter));
            
            if (objectType == null)
                throw new ArgumentNullException(nameof(objectType));
            
            if (objectType != Int32OrStringV1Type)
                throw new NotSupportedException($"{GetType().FullName} cannot serialise a value of type '{objectType.FullName}'.");

            if (value is Int32OrStringV1 int32OrStringV1)
            {
                emitter.Emit(new Scalar(
                    value: int32OrStringV1.ToString()
                ));
            }
            else
            {
                emitter.Emit(new Scalar(
                    value: null
                ));
            }
        }
    }
}
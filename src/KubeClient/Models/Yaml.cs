using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;

using KubeResourceClient = KubeClient.ResourceClients.KubeResourceClient;

namespace KubeClient.Models
{
    /// <summary>
    ///     Helper methods for YAML serialisation / deserialisation of models.
    /// </summary>
    public static class Yaml
    {
        /// <summary>
        ///     The buffer size used when reading from / writing to streams.
        /// </summary>
        const int StreamBufferSize = 1024;

        /// <summary>
        ///     The singleton YAML <see cref="Deserializer"/> used by static methods on <see cref="Yaml"/>.
        /// </summary>
        static readonly Deserializer YamlDeserialiser = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();

        /// <summary>
        ///     The singleton (JSON-compatible) YAML <see cref="Serializer"/> used by static methods on <see cref="Yaml"/>.
        /// </summary>
        static readonly Serializer YamlJsonSerialiser = new SerializerBuilder().JsonCompatible().Build();
        
        /// <summary>
        ///     The singleton <see cref="JsonSerializer"/> used by static methods on <see cref="Yaml"/>.
        /// </summary>
        static readonly JsonSerializer JsonSerializer = Newtonsoft.Json.JsonSerializer.Create(KubeResourceClient.SerializerSettings);

        /// <summary>
        ///     Convert YAML to JSON.
        /// </summary>
        /// <param name="yaml">
        ///     A <see cref="TextReader"/> containing the YAML to convert.
        /// </param>
        /// <returns>
        ///     A <see cref="JToken"/> representing the converted JSON.
        /// </returns>
        public static JToken ToJson(TextReader yaml)
        {
            if (yaml == null)
                throw new ArgumentNullException(nameof(yaml));
            
            object deserialisedYaml = YamlDeserialiser.Deserialize(yaml);
            
            using (MemoryStream buffer = new MemoryStream())
            {
                using (TextWriter jsonWriter = CreateTextWriter(buffer))
                {
                    YamlJsonSerialiser.Serialize(jsonWriter, deserialisedYaml);
                    jsonWriter.Flush();
                }

                buffer.Seek(0, SeekOrigin.Begin);

                using (JsonReader jsonReader = CreateJsonReader(buffer))
                {
                    return JToken.Load(jsonReader);
                }
            }
        }

        /// <summary>
        ///     Convert YAML to JSON.
        /// </summary>
        /// <param name="yaml">
        ///     A string containing the YAML to convert.
        /// </param>
        /// <param name="formatting">
        ///     A <see cref="Formatting"/> value indicating whether the converted JSON should be formatted (i.e. indented).
        /// </param>
        /// <returns>
        ///     A string containing the converted JSON.
        /// </returns>
        public static string ToJson(string yaml, Formatting formatting = Formatting.None)
        {
            if (yaml == null)
                throw new ArgumentNullException(nameof(yaml));
            
            object deserialisedYaml = YamlDeserialiser.Deserialize(
                new StringReader(yaml)
            );
            
            using (MemoryStream buffer = new MemoryStream())
            {
                using (TextWriter jsonWriter = CreateTextWriter(buffer))
                {
                    YamlJsonSerialiser.Serialize(jsonWriter, deserialisedYaml);
                    jsonWriter.Flush();
                }

                buffer.Seek(0, SeekOrigin.Begin);

                using (JsonReader jsonReader = CreateJsonReader(buffer))
                {
                    return JToken.Load(jsonReader).ToString(formatting);
                }
            }
        }

        /// <summary>
        ///     Deserialise a <typeparamref name="TModel"/> from YAML.
        /// </summary>
        /// <typeparam name="TModel">
        ///     The type of model to deserialise.
        /// </typeparam>
        /// <param name="yaml">
        ///     A <see cref="TextReader"/> containing the YAML.
        /// </param>
        /// <returns>
        ///     The deserialised <typeparamref name="TModel"/>.
        /// </returns>
        /// <remarks>
        ///     Delegates the actual deserialisation to JSON.NET, after converting the YAML to JSON.
        /// 
        ///     Not particularly efficient, but safe and reliable.
        /// </remarks>
        public static TModel Deserialize<TModel>(TextReader yaml)
        {
            if (yaml == null)
                throw new ArgumentNullException(nameof(yaml));

            object deserialisedYaml = YamlDeserialiser.Deserialize(yaml);

            using (MemoryStream buffer = new MemoryStream())
            {
                using (JsonWriter jsonWriter = CreateJsonWriter(buffer))
                {
                    JsonSerializer.Serialize(jsonWriter, deserialisedYaml);
                    jsonWriter.Flush();
                }

                buffer.Seek(0, SeekOrigin.Begin);

                using (JsonReader jsonReader = CreateJsonReader(buffer))
                {
                    return JsonSerializer.Deserialize<TModel>(jsonReader);
                }
            }
        }

        /// <summary>
        ///     Create a <see cref="JsonReader"/> that reads from the specified stream.
        /// </summary>
        /// <param name="stream">
        ///     The target stream.
        /// </param>
        /// <returns>
        ///     The new <see cref="JsonReader"/>.
        /// </returns>
        static JsonReader CreateJsonReader(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            
            TextReader textReader = null;
            JsonReader jsonReader = null;

            try
            {
                textReader = CreateTextReader(stream);
                jsonReader = new JsonTextReader(textReader);
            }
            catch (Exception)
            {
                using (jsonReader)
                using (textReader)
                {
                    throw;
                }
            }

            return jsonReader;
        }

        /// <summary>
        ///     Create a <see cref="JsonWriter"/> that writes to the specified stream.
        /// </summary>
        /// <param name="stream">
        ///     The target stream.
        /// </param>
        /// <returns>
        ///     The new <see cref="JsonWriter"/>.
        /// </returns>
        static JsonWriter CreateJsonWriter(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            
            TextWriter textWriter = null;
            JsonWriter jsonWriter = null;

            try
            {
                textWriter = CreateTextWriter(stream);
                jsonWriter = new JsonTextWriter(textWriter);
            }
            catch (Exception)
            {
                using (jsonWriter)
                using (textWriter)
                {
                    throw;
                }
            }

            return jsonWriter;
        }

        /// <summary>
        ///     Create a <see cref="TextReader"/> that reads from the specified stream.
        /// </summary>
        /// <param name="stream">
        ///     The target stream.
        /// </param>
        /// <returns>
        ///     The new <see cref="TextReader"/>.
        /// </returns>
        static TextReader CreateTextReader(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            
            return new StreamReader(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, bufferSize: StreamBufferSize, leaveOpen: true);
        }

        /// <summary>
        ///     Create a <see cref="TextWriter"/> that writes to the specified stream.
        /// </summary>
        /// <param name="stream">
        ///     The target stream.
        /// </param>
        /// <returns>
        ///     The new <see cref="TextWriter"/>.
        /// </returns>
        static TextWriter CreateTextWriter(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            
            return new StreamWriter(stream, Encoding.UTF8, bufferSize: StreamBufferSize, leaveOpen: true);
        }
    }
}
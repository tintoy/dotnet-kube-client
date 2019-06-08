using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace KubeClient.Models.Converters
{
    /// <summary>
    ///     Dynamic JSON converter for types derived from <see cref="KubeResourceV1"/>.
    /// </summary>
    public class DynamicKubeResourceConverter
        : JsonConverter
    {
        /// <summary>
        /// The CLR <see cref="Type"/> representing <see cref="KubeResourceV1"/>.
        /// </summary>
        static readonly TypeInfo KubeResourceV1Type = typeof(KubeResourceV1).GetTypeInfo();

        /// <summary>
        /// Registered model types, keyed by K8s kind and apiVersion.
        /// </summary>
        readonly Dictionary<(string kind, string apiVersion), Type> _modelTypesByKubeKind;

        /// <summary>
        /// Create a new <see cref="DynamicKubeResourceConverter"/>.
        /// </summary>
        /// <param name="modelTypeAssemblies">Assemblies containing model types supported for deserialisation.</param>
        public DynamicKubeResourceConverter(params Assembly[] modelTypeAssemblies)
            : this((IEnumerable<Assembly>)modelTypeAssemblies)
        {
        }

        /// <summary>
        /// Create a new <see cref="DynamicKubeResourceConverter"/>.
        /// </summary>
        /// <param name="modelTypeAssemblies">Assemblies containing model types supported for deserialisation.</param>
        public DynamicKubeResourceConverter(IEnumerable<Assembly> modelTypeAssemblies)
        {
            if (modelTypeAssemblies == null)
                throw new ArgumentNullException(nameof(modelTypeAssemblies));
            
            _modelTypesByKubeKind = ModelMetadata.KubeObject.BuildKindToTypeLookup(modelTypeAssemblies);
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
        public override bool CanConvert(Type objectType)
        {
            if (objectType == null)
                throw new ArgumentNullException(nameof(objectType));
            
            return KubeResourceV1Type.IsAssignableFrom(
                objectType.GetTypeInfo()
            );
        }

        /// <summary>
        /// This converter only supports deserialisation (not serialisation).
        /// </summary>
        public override bool CanWrite => false;

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
            
            TypeInfo objectTypeInfo = objectType.GetTypeInfo();
            if (!KubeResourceV1Type.IsAssignableFrom(objectTypeInfo))
                throw new NotSupportedException($"{GetType().FullName} cannot deserialise a value of type '{objectType.FullName}' (not derived from {typeof(KubeResourceV1).FullName}).");

            JObject json = JObject.Load(reader);

            string kind = json.Value<string>("kind");
            if (string.IsNullOrWhiteSpace(kind))
                throw new KubeClientException($"Cannot deserialise {nameof(KubeResourceV1)} from JSON because the 'kind' property is missing.");

            string apiVersion = json.Value<string>("apiVersion");
            if (string.IsNullOrWhiteSpace(apiVersion))
                throw new KubeClientException($"Cannot deserialise {nameof(KubeResourceV1)} from JSON because the 'apiVersion' property is missing.");
            
            Type modelType;
            if (!_modelTypesByKubeKind.TryGetValue((kind, apiVersion), out modelType))
                throw new KubeClientException($"Cannot deserialise {nameof(KubeResourceV1)} from JSON because no model type has been registered for '{apiVersion}/{kind}'.");

            // Ensure the registered model type is actually compatible with the model type being deserialised.
            if (!objectTypeInfo.IsAssignableFrom(modelType.GetTypeInfo()))
                throw new NotSupportedException($"{GetType().FullName} cannot deserialise a value of type '{modelType.FullName}' (not derived from {objectType.FullName}).");

            return serializer.Deserialize(json.CreateReader(), modelType);
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

            throw new NotSupportedException("This converter only supports deserialisation (not serialisation).");
        }
    }
}
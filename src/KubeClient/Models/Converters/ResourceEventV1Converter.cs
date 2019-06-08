using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace KubeClient.Models.Converters
{
    /// <summary>
    ///     Dynamic JSON converter for <see cref="ResourceEventV1{TResource}"/>.
    /// </summary>
    public class ResourceEventV1Converter
        : JsonConverter
    {
        /// <summary>
        /// The CLR <see cref="Type"/> representing <see cref="ResourceEventV1{TResource}"/>, specialised for <see cref="KubeResourceV1"/>.
        /// </summary>
        static readonly Type ResourceEventV1Type = typeof(ResourceEventV1<KubeResourceV1>);

        /// <summary>
        /// The CLR <see cref="Type"/> representing <see cref="KubeResourceV1"/>.
        /// </summary>
        static readonly TypeInfo KubeResourceV1Type = typeof(KubeResourceV1).GetTypeInfo();

        /// <summary>
        /// Registered model types, keyed by K8s kind and apiVersion.
        /// </summary>
        readonly Dictionary<(string kind, string apiVersion), Type> _modelTypesByKubeKind;

        /// <summary>
        /// Create a new <see cref="ResourceEventV1Converter"/>.
        /// </summary>
        /// <param name="modelTypeAssemblies">Assemblies containing model types supported for deserialisation.</param>
        public ResourceEventV1Converter(params Assembly[] modelTypeAssemblies)
            : this((IEnumerable<Assembly>)modelTypeAssemblies)
        {
        }

        /// <summary>
        /// Create a new <see cref="ResourceEventV1Converter"/>.
        /// </summary>
        /// <param name="modelTypeAssemblies">Assemblies containing model types supported for deserialisation.</param>
        public ResourceEventV1Converter(IEnumerable<Assembly> modelTypeAssemblies)
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
        public override bool CanConvert(Type objectType) => objectType == ResourceEventV1Type;

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

            if (objectType != ResourceEventV1Type)
                throw new NotSupportedException($"{GetType().Name} cannot deserialise a value of type '{objectType.FullName}' (only {typeof(ResourceEventV1<KubeResourceV1>).FullName} is supported).");

            JObject eventJson = JObject.Load(reader);

            var resourceEvent = new ResourceEventV1<KubeResourceV1>
            {
                EventType = DeserializeEventType(eventJson),
                Resource = DeserializeResource(eventJson, serializer)
            };
            
            return resourceEvent;
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

            throw new NotSupportedException($"{GetType().Name} only supports deserialisation (not serialisation).");
        }

        /// <summary>
        /// Deserialise the event's type from JSON.
        /// </summary>
        /// <param name="eventJson">The serialised event JSON.</param>
        /// <returns>A <see cref="ResourceEventType"/> value representing the event type.</returns>
        ResourceEventType DeserializeEventType(JObject eventJson)
        {
            if (eventJson == null)
                throw new ArgumentNullException(nameof(eventJson));

            if (eventJson.Property("type") == null)
                throw new NotSupportedException($"{GetType().Name} cannot deserialise JSON because it does not represent a valid {nameof(ResourceEventV1<KubeResourceV1>)} (missing 'type' property).");

            ResourceEventType eventType;
            string rawEventType = eventJson.Value<string>("type");
            if (!Enum.TryParse(rawEventType, ignoreCase: true, out eventType))
                throw new NotSupportedException($"{GetType().Name} cannot deserialise JSON because it does not represent a valid {nameof(ResourceEventV1<KubeResourceV1>)} ('type' property has invalid  value '{rawEventType}').");

            return eventType;
        }

        /// <summary>
        /// Get the event's serialised resource from JSON.
        /// </summary>
        /// <param name="eventJson">The serialised event JSON.</param>
        /// <returns>A <see cref="JObject"/> containing the serialised resource JSON, the resource kind, and the resource API version.</returns>
        (JObject resourceJson, string kind, string apiVersion) GetResourceJson(JObject eventJson)
        {
            if (eventJson == null)
                throw new ArgumentNullException(nameof(eventJson));

            // "object" property might not be present at all.

            JObject resourceJson = eventJson.Value<JObject>("object");
            if (resourceJson == null)
                return (resourceJson: null, kind: null, apiVersion: null);

            string kind = resourceJson.Value<string>("kind");
            if (string.IsNullOrWhiteSpace(kind))
                throw new KubeClientException($"{GetType().Name} cannot deserialise {nameof(KubeResourceV1)} from JSON because the 'kind' property is missing.");

            string apiVersion = resourceJson.Value<string>("apiVersion");
            if (string.IsNullOrWhiteSpace(apiVersion))
                throw new KubeClientException($"{GetType().Name} cannot deserialise {nameof(KubeResourceV1)} from JSON because the 'apiVersion' property is missing.");

            return (resourceJson, kind, apiVersion);
        }

        /// <summary>
        /// Get the CLR type representing the model for the specified resource kind and API version.
        /// </summary>
        /// <param name="kind">The K8s resource kind.</param>
        /// <param name="apiVersion">The K8s API version.</param>
        /// <returns>The model <see cref="Type"/>.</returns>
        Type GetModelType(string kind, string apiVersion)
        {
            if (String.IsNullOrWhiteSpace(kind))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(kind)}.", nameof(kind));

            if (String.IsNullOrWhiteSpace(apiVersion))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(apiVersion)}.", nameof(apiVersion));

            Type modelType;
            if (!_modelTypesByKubeKind.TryGetValue((kind, apiVersion), out modelType))
                throw new KubeClientException($"{GetType().Name} cannot deserialise {nameof(KubeResourceV1)} from JSON because no model type has been registered for '{apiVersion}/{kind}'.");

            // Ensure the registered model type is actually compatible with the model type being deserialised.
            if (!KubeResourceV1Type.IsAssignableFrom(modelType.GetTypeInfo()))
                throw new NotSupportedException($"{GetType().Name} cannot deserialise a value of type '{modelType.FullName}' (not derived from {KubeResourceV1Type.FullName}).");

            return modelType;
        }

        /// <summary>
        /// Deserialise the event's resource from JSON.
        /// </summary>
        /// <param name="eventJson">The serialised event JSON.</param>
        /// <param name="serializer">The JSON serialiser used to deserialise the resource.</param>
        /// <returns>The deserialised resource.</returns>
        KubeResourceV1 DeserializeResource(JObject eventJson, JsonSerializer serializer)
        {
            (JObject resourceJson, string kind, string apiVersion) = GetResourceJson(eventJson);
            if (resourceJson == null)
                return null; // AF: I think it's possible for the event's resource to be null in some cases.

            Type modelType = GetModelType(kind, apiVersion);

            return (KubeResourceV1)serializer.Deserialize(resourceJson.CreateReader(), modelType);
        }
    }
}
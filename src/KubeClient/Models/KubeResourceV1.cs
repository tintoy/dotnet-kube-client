using System;
using System.Collections.Concurrent;
using System.Reflection;
using Newtonsoft.Json;

namespace KubeClient.Models
{
    /// <summary>
    ///     The base class for Kubernetes resources.
    /// </summary>
    public abstract class KubeResourceV1
        : KubeObjectV1
    {
        /// <summary>
        ///     Standard object's metadata. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#metadata
        /// </summary>
        [JsonProperty("metadata")]
        public ObjectMetaV1 Metadata { get; set; }
    }

    /// <summary>
    ///     The base class for Kubernetes models.
    /// </summary>
    public class KubeObjectV1
    {
        /// <summary>
        ///     Model type metadata.
        /// </summary>
        static readonly ConcurrentDictionary<Type, (string kind, string apiVersion)> ModelMetadata = new ConcurrentDictionary<Type, (string kind, string apiVersion)>();

        /// <summary>
        ///     Create a new <see cref="KubeObjectV1"/>, automatically initialising <see cref="Kind"/> and <see cref="ApiVersion"/> (if possible).
        /// </summary>
        protected KubeObjectV1()
        {
            (Kind, ApiVersion) = ModelMetadata.GetOrAdd(GetType(), modelType =>
            {
                var kubeResourceAttribute = modelType.GetTypeInfo().GetCustomAttribute<KubeResourceAttribute>();
                if (kubeResourceAttribute == null)
                    return (kubeResourceAttribute.Kind, kubeResourceAttribute.ApiVersion);

                return (null, null);
            });
        }

        /// <summary>
        ///     Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#types-kinds
        /// </summary>
        [JsonProperty("kind")]
        public string Kind { get; set; }

        /// <summary>
        ///     APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#resources
        /// </summary>
        [JsonProperty("apiVersion")]
        public string ApiVersion { get; set; }
    }
}

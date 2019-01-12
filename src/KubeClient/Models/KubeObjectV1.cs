using System;
using System.Collections.Concurrent;
using System.Reflection;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
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
            (Kind, ApiVersion) = GetKubeKind(GetType());
        }

        /// <summary>
        ///     Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#types-kinds
        /// </summary>
        [JsonProperty("kind")]
        [YamlMember(Alias = "kind")]
        public string Kind { get; set; }

        /// <summary>
        ///     APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#resources
        /// </summary>
        [JsonProperty("apiVersion")]
        [YamlMember(Alias = "apiVersion")]
        public string ApiVersion { get; set; }

        /// <summary>
        ///     Get Kubernetes Kind / ApiVersion metadata for the specified object type.
        /// </summary>
        /// <typeparam name="TObject">
        ///     The target object type.
        /// </typeparam>
        /// <returns>
        ///     A tuple containing the object's Kind and ApiVersion metadata (or <c>null</c> and <c>null</c>, if no metadata is available for <typeparamref name="TObject"/>).
        /// </returns>
        public static (string kind, string apiVersion) GetKubeKind<TObject>()
            where TObject : KubeObjectV1
        {
            return GetKubeKind(
                typeof(TObject)
            );
        }

        /// <summary>
        ///     Get Kubernetes Kind / ApiVersion metadata for the specified object type.
        /// </summary>
        /// <param name="kubeObjectType">
        ///     The Kubernetes object type.
        /// </param>
        /// <returns>
        ///     A tuple containing the object's Kind and ApiVersion metadata (or <c>null</c> and <c>null</c>, if no metadata is available for <paramref name="kubeObjectType"/>).
        /// </returns>
        public static (string kind, string apiVersion) GetKubeKind(Type kubeObjectType)
        {
            if (kubeObjectType == null)
                throw new ArgumentNullException(nameof(kubeObjectType));

            (string kind, string apiVersion) kubeKind = ModelMetadata.GetOrAdd(kubeObjectType, modelType =>
            {
                var kubeObjectAttribute = modelType.GetTypeInfo().GetCustomAttribute<KubeObjectAttribute>();
                if (kubeObjectAttribute != null)
                    return (kubeObjectAttribute.Kind, kubeObjectAttribute.ApiVersion);

                return (null, null);
            });

            return kubeKind;
        }
    }

    /// <summary>
    ///     Extension methods for <see cref="KubeObjectV1"/>.
    /// </summary>
    public static class KubeObjectExtensions
    {
        /// <summary>
        ///     Remove type metadata (Kind and ApiVersion) from the <see cref="KubeObjectV1"/>.
        /// </summary>
        /// <param name="kubeObject">
        ///     The <see cref="KubeObjectV1"/>.
        /// </param>
        /// <returns>
        ///     The <see cref="KubeObjectV1"/> (enables inline use).
        /// </returns>
        public static TObject NoTypeMeta<TObject>(this TObject kubeObject)
            where TObject : KubeObjectV1
        {
            if (kubeObject == null)
                return null;

            kubeObject.Kind = null;
            kubeObject.ApiVersion = null;

            return kubeObject;
        }
    }
}

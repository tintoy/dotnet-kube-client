using System;

namespace KubeClient.Models
{
    /// <summary>
    ///     Mark a model class as representing a Kubernetes object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class KubeObjectAttribute
        : Attribute
    {
        /// <summary>
        ///     Mark the model class as representing a Kubernetes object.
        /// </summary>
        /// <param name="kind">
        ///     The object kind.
        /// </param>
        /// <param name="apiVersion">
        ///     The object API version.
        /// </param>
        public KubeObjectAttribute(string kind, string apiVersion)
        {
            if (String.IsNullOrWhiteSpace(kind))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(kind));

            if (String.IsNullOrWhiteSpace(apiVersion))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'version'.", nameof(apiVersion));

            Kind = kind;
            ApiVersion = apiVersion;
        }

        /// <summary>
        ///     The resource kind.
        /// </summary>
        public string Kind { get; }

        /// <summary>
        ///     The resource API version.
        /// </summary>
        public string ApiVersion { get; }
    }
}

using System;

namespace KubeClient.Models
{
    /// <summary>
    ///     Mark a model class as representing a Kubernetes resource.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class KubeResourceAttribute
        : Attribute
    {
        /// <summary>
        ///     Mark the model class as representing a Kubernetes resource.
        /// </summary>
        /// <param name="kind">
        ///     The resource kind.
        /// </param>
        /// <param name="apiVersion">
        ///     The resource API version.
        /// </param>
        public KubeResourceAttribute(string kind, string apiVersion)
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

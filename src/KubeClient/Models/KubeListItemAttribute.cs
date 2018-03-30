using System;

namespace KubeClient.Models
{
    /// <summary>
    ///     Mark a model class as representing a list whose items are Kubernetes objects of a specific kind / API version.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class KubeListItemAttribute
        : Attribute
    {
        /// <summary>
        ///     Mark the model class as representing a list of Kubernetes objects of the specified kind / API version.
        /// </summary>
        /// <param name="kind">
        ///     The object kind.
        /// </param>
        /// <param name="apiVersion">
        ///     The object API version.
        /// </param>
        public KubeListItemAttribute(string kind, string apiVersion)
        {
            if (String.IsNullOrWhiteSpace(kind))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(kind));

            if (String.IsNullOrWhiteSpace(apiVersion))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'version'.", nameof(apiVersion));

            Kind = kind;
            ApiVersion = apiVersion;
        }

        /// <summary>
        ///     The item kind.
        /// </summary>
        public string Kind { get; }

        /// <summary>
        ///     The item API version.
        /// </summary>
        public string ApiVersion { get; }
    }
}

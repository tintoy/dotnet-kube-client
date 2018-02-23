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
        /// <param name="version">
        ///     The resource API version.
        /// </param>
        public KubeResourceAttribute(string kind, string version)
        {
            if (String.IsNullOrWhiteSpace(kind))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(kind));

            if (String.IsNullOrWhiteSpace(version))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'version'.", nameof(version));

            Kind = kind;
            Version = version;
        }

        /// <summary>
        ///     The resource kind.
        /// </summary>
        public string Kind { get; }

        /// <summary>
        ///     The resource API version.
        /// </summary>
        public string Version { get; }
    }
}

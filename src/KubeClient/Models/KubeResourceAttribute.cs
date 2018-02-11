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
        /// <param name="name">
        ///     The resource name.
        /// </param>
        /// <param name="version">
        ///     The resource API version.
        /// </param>
        public KubeResourceAttribute(string name, string version)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            if (String.IsNullOrWhiteSpace(version))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'version'.", nameof(version));

            Name = name;
            Version = version;
        }

        /// <summary>
        ///     The resource name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     The resource API version.
        /// </summary>
        public string Version { get; }
    }
}

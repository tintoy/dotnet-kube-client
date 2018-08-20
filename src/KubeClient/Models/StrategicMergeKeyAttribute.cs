using System;

namespace KubeClient.Models
{
    /// <summary>
    ///     Marks a property as being the key used to merge items when using strategic-merge-patch in the Kubernetes API.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class StrategicMergeKeyAttribute
        : Attribute
    {
        /// <summary>
        ///     Mark the property as being mergeable when using strategic-merge-patch in the Kubernetes API.
        /// </summary>
        /// <param name="name">
        ///     The name of the field to use as a key when merging items.
        /// </param>
        public StrategicMergeKeyAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        ///     The name of the field used as a key when merging items.
        /// </summary>
        public string Name { get; }
    }
}

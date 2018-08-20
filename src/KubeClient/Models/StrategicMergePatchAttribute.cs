using System;

namespace KubeClient.Models
{
    /// <summary>
    ///     Marks a property as being mergeable when using strategic-merge-patch in the Kubernetes API.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class StrategicMergePatchAttribute
        : Attribute
    {
        /// <summary>
        ///     Mark the property as being mergeable when using strategic-merge-patch in the Kubernetes API.
        /// </summary>
        /// <param name="mergeKey">
        ///     The name of the field to use as a key when merging items.
        /// </param>
        public StrategicMergePatchAttribute(string mergeKey)
        {
            MergeKey = mergeKey;
        }

        /// <summary>
        ///     The name of the field to use as a key when merging items.
        /// </summary>
        public string MergeKey { get; }
    }
}

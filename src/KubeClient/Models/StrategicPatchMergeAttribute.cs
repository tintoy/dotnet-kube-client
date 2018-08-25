using System;

namespace KubeClient.Models
{
    /// <summary>
    ///     Marks a property as supporting merge when using strategic patch in the Kubernetes API.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class StrategicPatchMergeAttribute
        : Attribute
    {
        /// <summary>
        ///     Mark the property as supporting merge when using strategic patch in the Kubernetes API.
        /// </summary>
        public StrategicPatchMergeAttribute()
        {
        }

        /// <summary>
        ///     The name of the field (if any) to use as a key when merging items.
        /// </summary>
        public string Key { get; set; }
    }
}

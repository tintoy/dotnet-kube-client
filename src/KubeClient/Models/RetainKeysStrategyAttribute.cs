using System;

namespace KubeClient.Models
{
    /// <summary>
    ///     Marks a property as supporting merge when using strategic patch in the Kubernetes API.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class RetainKeysStrategyAttribute
        : PatchStrategyAttribute
    {
        /// <summary>
        ///     Mark the property as supporting merge when using strategic patch in the Kubernetes API.
        /// </summary>
        public RetainKeysStrategyAttribute()
        {
        }
    }
}

using System;

namespace KubeClient.Models
{
    /// <summary>
    ///     Marks a property as using the specified strategy for strategic-merge-patch in the Kubernetes API.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class KubePatchStrategyAttribute
        : Attribute
    {
        /// <summary>
        ///     Mark the property as using the specified strategy for strategic-merge-patch in the Kubernetes API.
        /// </summary>
        /// <param name="strategy">
        ///     A <see cref="KubePatchStrategy"/> value representing the merge strategy to use.
        /// </param>
        public KubePatchStrategyAttribute(KubePatchStrategy strategy)
        {
            Strategy = strategy;
        }

        /// <summary>
        ///     A <see cref="KubePatchStrategy"/> value representing the merge strategy to use.
        /// </summary>
        public KubePatchStrategy Strategy { get; }

        /// <summary>
        ///     The name of the field (if any) to use as a key when merging items.
        /// </summary>
        public string MergeKey { get; set; }
    }
}

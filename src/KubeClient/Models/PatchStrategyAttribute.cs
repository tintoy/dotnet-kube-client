using System;

namespace KubeClient.Models
{
    /// <summary>
    ///     Base class for attributes representing property-level patch strategies in Kubernetes strategic patch.
    /// </summary>
    public abstract class PatchStrategyAttribute
        : Attribute
    {
        /// <summary>
        ///     Create a new <see cref="PatchStrategyAttribute"/>.
        /// </summary>
        protected PatchStrategyAttribute()
        {
        }
    }
}

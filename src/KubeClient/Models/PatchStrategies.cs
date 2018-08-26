using System;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents field-level strategies for Kubernetes strategic patch.
    /// </summary>
    [Flags]
    public enum PatchStrategies
    {
        /// <summary>
        ///     Field contents will be replaced.
        /// </summary>
        Replace = 0,

        /// <summary>
        ///     Field contents will be merged.
        /// </summary>
        Merge = 1,

        /// <summary>
        ///     Field contents will be replaced, except for contents whose keys match a value supplied in the $retainKeys directive.
        /// </summary>
        RetainKeys = 2
    }
}

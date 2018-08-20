namespace KubeClient.Models
{
    /// <summary>
    ///     Represents a strategy for strategic-merge-patch in the Kubernetes API.
    /// </summary>
    public enum KubePatchStrategy
    {
        /// <summary>
        ///     An unknown patch strategy.
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///     Field contents should be merged.
        /// </summary>
        Merge = 1,

        /// <summary>
        ///     Field contents should be replaced.
        /// </summary>
        Replace = 2
    }
}
namespace KubeClient.Models
{
    /// <summary>
    ///     Well-known policies for propagating deletion of a resource to its children.
    /// </summary>
    public enum DeletePropagationPolicy
    {
        /// <summary>
        ///     Do not delete child resources.
        /// </summary>
        Orphan = 0,

        /// <summary>
        ///     Delete child resources in the foreground; parent's deletion will not complete until child resources have been deleted.
        /// </summary>
        Foreground = 1,

        /// <summary>
        ///     Delete child resources in the foreground; child resources will be deleted asynchronously.
        /// </summary>
        Background = 2,
    }
}

using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Status is a return value for calls that don't return other objects.
    /// </summary>
    public partial class StatusV1
    {
        /// <summary>
        ///     Enumerate the list's items.
        /// </summary>
        /// <returns>
        ///     The list's items.
        /// </returns>
        public override IEnumerable<KubeResourceV1> EnumerateItems()
        {
            yield break; // StatusV1 is not really a Kubernetes resource list.
        }
    }
}

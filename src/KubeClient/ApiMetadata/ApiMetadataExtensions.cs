using KubeClient.Models;
using System;
using System.Linq;

namespace KubeClient.ApiMetadata
{
    /// <summary>
    ///     Extension methods for working with Kubernetes API metadata.
    /// </summary>
    public static class ApiMetadataExtensions
    {
        /// <summary>
        ///     Determine whether a Kubernetes API path supports the specified well-known action.
        /// </summary>
        /// <param name="apiPathMetadata">
        ///     The API path metadata.
        /// </param>
        /// <param name="action">
        ///     A <see cref="KubeAction"/> representing the target action.
        /// </param>
        /// <returns>
        ///     <c>true</c>, if the path supports the specified action; otherwise, <c>false</c>.
        /// </returns>
        public static bool SupportsAction(this KubeApiPathMetadata apiPathMetadata, KubeAction action)
        {
            if (apiPathMetadata == null)
                throw new ArgumentNullException(nameof(apiPathMetadata));

            string verb = action.ToString().ToLower();

            return apiPathMetadata.Verbs.Contains(verb);
        }
    }
}

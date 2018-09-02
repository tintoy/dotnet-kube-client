using System;

namespace KubeClient.Models
{
    /// <summary>
    ///     Well-known actions supported by Kubernetes resource APIs.
    /// </summary>
    public enum KubeAction
    {
        /// <summary>
        ///     An unknown action.
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///     Retrieve a single resource by name.
        /// </summary>
        Get = 1,

        /// <summary>
        ///     Create a new resource.
        /// </summary>
        Create = 2,

        /// <summary>
        ///     Delete a resource by name.
        /// </summary>
        Delete = 3,

        /// <summary>
        ///     Delete multiple resources.
        /// </summary>
        DeleteCollection = 4,

        /// <summary>
        ///     Retrieve a list of resources.
        /// </summary>
        List = 5,

        /// <summary>
        ///     Selectively update part(s) of a resource.
        /// </summary>
        Patch = 6,

        /// <summary>
        ///     Replace a resource.
        /// </summary>
        Update = 7,

        /// <summary>
        ///     Watch a resource for changes.
        /// </summary>
        Watch = 8,

        /// <summary>
        ///     Watch a list of resources for changes.
        /// </summary>
        WatchList = 9,

        /// <summary>
        ///     Connect to a resource.
        /// </summary>
        Connect = 10,

        /// <summary>
        ///     Create a proxy connection to a resource.
        /// </summary>
        Proxy = 11
    }
}

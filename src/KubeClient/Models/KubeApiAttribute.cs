using System;

namespace KubeClient.Models
{
    /// <summary>
    ///     Declares the path for one or more Kubernetes resource API actions.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class KubeApiAttribute
        : Attribute
    {
        /// <summary>
        ///     Declare a path and one or more actions for the Kubernetes resource API represented by the target model.
        /// </summary>
        /// <param name="action">
        ///     The API action.
        /// </param>
        /// <param name="paths">
        ///     The API path(s) supporting the action.
        /// </param>
        public KubeApiAttribute(KubeAction action, params string[] paths)
        {
            if (paths.Length == 0)
                throw new ArgumentException("Must specify at least one API path.", nameof(paths));

            Action = action;
            Paths = paths;
        }

        /// <summary>
        ///     The API action.
        /// </summary>
        public KubeAction Action { get; }

        /// <summary>
        ///     The API path(s) supporting the action.
        /// </summary>
        public string[] Paths { get; }
    }
}

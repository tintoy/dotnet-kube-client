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
        /// <param name="path">
        ///     The API path.
        /// </param>
        /// <param name="actions">
        ///     The API action(s) handled by the path.
        /// </param>
        public KubeApiAttribute(string path, params KubeAction[] actions)
        {
            if (String.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'path'.", nameof(path));

            if (actions.Length == 0)
                throw new ArgumentException("Must specify at least one API action.", nameof(actions));

            Path = path;
            Actions = actions;
        }

        /// <summary>
        ///     The API path.
        /// </summary>
        public string Path { get; }

        /// <summary>
        ///     The API action(s) handled by the path.
        /// </summary>
        public KubeAction[] Actions { get; }
    }
}

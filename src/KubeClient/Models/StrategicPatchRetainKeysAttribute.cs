using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Marks a model property as representing a field where the values of all fields not supplied in the patch request are discarded (except the ones specified in <see cref="RetainKeys"/>) when using strategic patch in the Kubernetes API.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class StrategicPatchRetainKeysAttribute
        : Attribute
    {
        /// <summary>
        ///     Mark the property as representing a field where the values of all fields not supplied in the patch request are discarded (except the ones specified in <see cref="RetainKeys"/>) when using strategic patch in the Kubernetes API.
        /// </summary>
        public StrategicPatchRetainKeysAttribute(params string[] retainKeys)
        {
            if (retainKeys == null)
                throw new ArgumentNullException(nameof(retainKeys));
            
            RetainKeys = retainKeys;
        }

        /// <summary>
        ///     The field name(s) that are retained even if they are not specified as part of the request.
        /// </summary>
        public IReadOnlyList<string> RetainKeys { get; set; }
    }
}

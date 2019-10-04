using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace KubeClient.Models.ContractResolvers
{
    /// <summary>
    ///     JSON contract resolver for K8s models.
    /// </summary>
    /// <remarks>
    ///     Preserves casing of dictionary keys, but all other keys are converted to camelCase.
    /// </remarks>
    public class KubeContractResolver
        : CamelCasePropertyNamesContractResolver
    {
        /// <summary>
        ///     Create a new <see cref="KubeContractResolver"/>.
        /// </summary>
        public KubeContractResolver()
        { 
        }

        /// <summary>
        /// Get the JSON property name used to represent a dictionary key.
        /// </summary>
        /// <param name="dictionaryKey">The dictionary key.</param>
        /// <returns>The JSON property name.</returns>
        protected override string ResolveDictionaryKey(string dictionaryKey) => dictionaryKey;
    }
}

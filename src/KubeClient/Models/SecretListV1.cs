using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     SecretList is a list of Secret.
    /// </summary>
    [KubeObject("SecretList", "v1")]
    public class SecretListV1 : KubeResourceListV1
    {
        /// <summary>
        ///     Items is a list of secret objects. More info: https://kubernetes.io/docs/concepts/configuration/secret
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<SecretV1> Items { get; set; } = new List<SecretV1>();
    }
}

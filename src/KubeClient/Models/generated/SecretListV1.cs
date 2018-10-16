using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SecretList is a list of Secret.
    /// </summary>
    [KubeListItem("Secret", "v1")]
    [KubeObject("SecretList", "v1")]
    public partial class SecretListV1 : KubeResourceListV1<SecretV1>
    {
        /// <summary>
        ///     Items is a list of secret objects. More info: https://kubernetes.io/docs/concepts/configuration/secret
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<SecretV1> Items { get; } = new List<SecretV1>();
    }
}

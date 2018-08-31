using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ServiceAccountList is a list of ServiceAccount objects
    /// </summary>
    [KubeListItem("ServiceAccount", "v1")]
    [KubeObject("ServiceAccountList", "v1")]
    public partial class ServiceAccountListV1 : KubeResourceListV1<ServiceAccountV1>
    {
        /// <summary>
        ///     List of ServiceAccounts. More info: https://kubernetes.io/docs/tasks/configure-pod-container/configure-service-account/
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ServiceAccountV1> Items { get; } = new List<ServiceAccountV1>();
    }
}

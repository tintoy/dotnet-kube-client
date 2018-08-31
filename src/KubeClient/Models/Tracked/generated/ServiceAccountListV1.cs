using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ServiceAccountList is a list of ServiceAccount objects
    /// </summary>
    [KubeListItem("ServiceAccount", "v1")]
    [KubeObject("ServiceAccountList", "v1")]
    public partial class ServiceAccountListV1 : Models.ServiceAccountListV1, ITracked
    {
        /// <summary>
        ///     List of ServiceAccounts. More info: https://kubernetes.io/docs/tasks/configure-pod-container/configure-service-account/
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.ServiceAccountV1> Items { get; } = new List<Models.ServiceAccountV1>();
    }
}

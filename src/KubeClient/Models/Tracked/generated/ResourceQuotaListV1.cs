using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ResourceQuotaList is a list of ResourceQuota items.
    /// </summary>
    [KubeListItem("ResourceQuota", "v1")]
    [KubeObject("ResourceQuotaList", "v1")]
    public partial class ResourceQuotaListV1 : Models.ResourceQuotaListV1, ITracked
    {
        /// <summary>
        ///     Items is a list of ResourceQuota objects. More info: https://git.k8s.io/community/contributors/design-proposals/admission_control_resource_quota.md
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.ResourceQuotaV1> Items { get; } = new List<Models.ResourceQuotaV1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}

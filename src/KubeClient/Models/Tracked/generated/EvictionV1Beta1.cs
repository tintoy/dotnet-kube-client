using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Eviction evicts a pod from its node subject to certain policies and safety constraints. This is a subresource of Pod.  A request to cause such an eviction is created by POSTing to .../pods/&lt;pod name&gt;/evictions.
    /// </summary>
    [KubeObject("Eviction", "policy/v1beta1")]
    public partial class EvictionV1Beta1 : Models.EvictionV1Beta1, ITracked
    {
        /// <summary>
        ///     DeleteOptions may be provided
        /// </summary>
        [JsonProperty("deleteOptions")]
        [YamlMember(Alias = "deleteOptions")]
        public override DeleteOptionsV1 DeleteOptions
        {
            get
            {
                return base.DeleteOptions;
            }
            set
            {
                base.DeleteOptions = value;

                __ModifiedProperties__.Add("DeleteOptions");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}

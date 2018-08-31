using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     NetworkPolicy describes what network traffic is allowed for a set of Pods
    /// </summary>
    [KubeObject("NetworkPolicy", "networking.k8s.io/v1")]
    public partial class NetworkPolicyV1 : Models.NetworkPolicyV1
    {
        /// <summary>
        ///     Specification of the desired behavior for this NetworkPolicy.
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public override Models.NetworkPolicySpecV1 Spec
        {
            get
            {
                return base.Spec;
            }
            set
            {
                base.Spec = value;

                __ModifiedProperties__.Add("Spec");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}

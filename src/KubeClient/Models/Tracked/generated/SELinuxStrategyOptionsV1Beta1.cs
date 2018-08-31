using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     SELinux  Strategy Options defines the strategy type and any options used to create the strategy.
    /// </summary>
    public partial class SELinuxStrategyOptionsV1Beta1 : Models.SELinuxStrategyOptionsV1Beta1, ITracked
    {
        /// <summary>
        ///     type is the strategy that will dictate the allowable labels that may be set.
        /// </summary>
        [JsonProperty("rule")]
        [YamlMember(Alias = "rule")]
        public override string Rule
        {
            get
            {
                return base.Rule;
            }
            set
            {
                base.Rule = value;

                __ModifiedProperties__.Add("Rule");
            }
        }


        /// <summary>
        ///     seLinuxOptions required to run as; required for MustRunAs More info: https://git.k8s.io/community/contributors/design-proposals/security_context.md
        /// </summary>
        [JsonProperty("seLinuxOptions")]
        [YamlMember(Alias = "seLinuxOptions")]
        public override Models.SELinuxOptionsV1 SeLinuxOptions
        {
            get
            {
                return base.SeLinuxOptions;
            }
            set
            {
                base.SeLinuxOptions = value;

                __ModifiedProperties__.Add("SeLinuxOptions");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}

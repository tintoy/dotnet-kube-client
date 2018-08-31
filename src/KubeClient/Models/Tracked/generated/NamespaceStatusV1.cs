using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     NamespaceStatus is information about the current status of a Namespace.
    /// </summary>
    public partial class NamespaceStatusV1 : Models.NamespaceStatusV1, ITracked
    {
        /// <summary>
        ///     Phase is the current lifecycle phase of the namespace. More info: https://git.k8s.io/community/contributors/design-proposals/namespaces.md#phases
        /// </summary>
        [JsonProperty("phase")]
        [YamlMember(Alias = "phase")]
        public override string Phase
        {
            get
            {
                return base.Phase;
            }
            set
            {
                base.Phase = value;

                __ModifiedProperties__.Add("Phase");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}

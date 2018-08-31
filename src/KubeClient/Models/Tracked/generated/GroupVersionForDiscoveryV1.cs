using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     GroupVersion contains the "group/version" and "version" string of a version. It is made a struct to keep extensibility.
    /// </summary>
    public partial class GroupVersionForDiscoveryV1 : Models.GroupVersionForDiscoveryV1, ITracked
    {
        /// <summary>
        ///     groupVersion specifies the API group and version in the form "group/version"
        /// </summary>
        [JsonProperty("groupVersion")]
        [YamlMember(Alias = "groupVersion")]
        public override string GroupVersion
        {
            get
            {
                return base.GroupVersion;
            }
            set
            {
                base.GroupVersion = value;

                __ModifiedProperties__.Add("GroupVersion");
            }
        }


        /// <summary>
        ///     version specifies the version in the form of "version". This is to save the clients the trouble of splitting the GroupVersion.
        /// </summary>
        [JsonProperty("version")]
        [YamlMember(Alias = "version")]
        public override string Version
        {
            get
            {
                return base.Version;
            }
            set
            {
                base.Version = value;

                __ModifiedProperties__.Add("Version");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}

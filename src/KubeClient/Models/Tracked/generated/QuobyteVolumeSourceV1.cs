using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Represents a Quobyte mount that lasts the lifetime of a pod. Quobyte volumes do not support ownership management or SELinux relabeling.
    /// </summary>
    public partial class QuobyteVolumeSourceV1 : Models.QuobyteVolumeSourceV1
    {
        /// <summary>
        ///     Volume is a string that references an already created Quobyte volume by name.
        /// </summary>
        [JsonProperty("volume")]
        [YamlMember(Alias = "volume")]
        public override string Volume
        {
            get
            {
                return base.Volume;
            }
            set
            {
                base.Volume = value;

                __ModifiedProperties__.Add("Volume");
            }
        }


        /// <summary>
        ///     Group to map volume access to Default is no group
        /// </summary>
        [JsonProperty("group")]
        [YamlMember(Alias = "group")]
        public override string Group
        {
            get
            {
                return base.Group;
            }
            set
            {
                base.Group = value;

                __ModifiedProperties__.Add("Group");
            }
        }


        /// <summary>
        ///     User to map volume access to Defaults to serivceaccount user
        /// </summary>
        [JsonProperty("user")]
        [YamlMember(Alias = "user")]
        public override string User
        {
            get
            {
                return base.User;
            }
            set
            {
                base.User = value;

                __ModifiedProperties__.Add("User");
            }
        }


        /// <summary>
        ///     ReadOnly here will force the Quobyte volume to be mounted with read-only permissions. Defaults to false.
        /// </summary>
        [JsonProperty("readOnly")]
        [YamlMember(Alias = "readOnly")]
        public override bool ReadOnly
        {
            get
            {
                return base.ReadOnly;
            }
            set
            {
                base.ReadOnly = value;

                __ModifiedProperties__.Add("ReadOnly");
            }
        }


        /// <summary>
        ///     Registry represents a single or multiple Quobyte Registry services specified as a string as host:port pair (multiple entries are separated with commas) which acts as the central registry for volumes
        /// </summary>
        [JsonProperty("registry")]
        [YamlMember(Alias = "registry")]
        public override string Registry
        {
            get
            {
                return base.Registry;
            }
            set
            {
                base.Registry = value;

                __ModifiedProperties__.Add("Registry");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}

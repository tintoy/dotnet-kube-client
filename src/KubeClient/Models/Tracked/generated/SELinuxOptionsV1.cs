using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     SELinuxOptions are the labels to be applied to the container
    /// </summary>
    public partial class SELinuxOptionsV1 : Models.SELinuxOptionsV1, ITracked
    {
        /// <summary>
        ///     Role is a SELinux role label that applies to the container.
        /// </summary>
        [JsonProperty("role")]
        [YamlMember(Alias = "role")]
        public override string Role
        {
            get
            {
                return base.Role;
            }
            set
            {
                base.Role = value;

                __ModifiedProperties__.Add("Role");
            }
        }


        /// <summary>
        ///     Type is a SELinux type label that applies to the container.
        /// </summary>
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public override string Type
        {
            get
            {
                return base.Type;
            }
            set
            {
                base.Type = value;

                __ModifiedProperties__.Add("Type");
            }
        }


        /// <summary>
        ///     Level is SELinux level label that applies to the container.
        /// </summary>
        [JsonProperty("level")]
        [YamlMember(Alias = "level")]
        public override string Level
        {
            get
            {
                return base.Level;
            }
            set
            {
                base.Level = value;

                __ModifiedProperties__.Add("Level");
            }
        }


        /// <summary>
        ///     User is a SELinux user label that applies to the container.
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
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}

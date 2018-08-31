using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     RoleRef contains information that points to the role being used
    /// </summary>
    public partial class RoleRefV1Beta1 : Models.RoleRefV1Beta1, ITracked
    {
        /// <summary>
        ///     Kind is the type of resource being referenced
        /// </summary>
        [JsonProperty("kind")]
        [YamlMember(Alias = "kind")]
        public override string Kind
        {
            get
            {
                return base.Kind;
            }
            set
            {
                base.Kind = value;

                __ModifiedProperties__.Add("Kind");
            }
        }


        /// <summary>
        ///     Name is the name of resource being referenced
        /// </summary>
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;

                __ModifiedProperties__.Add("Name");
            }
        }


        /// <summary>
        ///     APIGroup is the group for the resource being referenced
        /// </summary>
        [JsonProperty("apiGroup")]
        [YamlMember(Alias = "apiGroup")]
        public override string ApiGroup
        {
            get
            {
                return base.ApiGroup;
            }
            set
            {
                base.ApiGroup = value;

                __ModifiedProperties__.Add("ApiGroup");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}

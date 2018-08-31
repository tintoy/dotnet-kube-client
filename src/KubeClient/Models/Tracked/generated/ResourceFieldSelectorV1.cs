using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ResourceFieldSelector represents container resources (cpu, memory) and their output format
    /// </summary>
    public partial class ResourceFieldSelectorV1 : Models.ResourceFieldSelectorV1
    {
        /// <summary>
        ///     Container name: required for volumes, optional for env vars
        /// </summary>
        [JsonProperty("containerName")]
        [YamlMember(Alias = "containerName")]
        public override string ContainerName
        {
            get
            {
                return base.ContainerName;
            }
            set
            {
                base.ContainerName = value;

                __ModifiedProperties__.Add("ContainerName");
            }
        }


        /// <summary>
        ///     Required: resource to select
        /// </summary>
        [JsonProperty("resource")]
        [YamlMember(Alias = "resource")]
        public override string Resource
        {
            get
            {
                return base.Resource;
            }
            set
            {
                base.Resource = value;

                __ModifiedProperties__.Add("Resource");
            }
        }


        /// <summary>
        ///     Specifies the output format of the exposed resources, defaults to "1"
        /// </summary>
        [JsonProperty("divisor")]
        [YamlMember(Alias = "divisor")]
        public override string Divisor
        {
            get
            {
                return base.Divisor;
            }
            set
            {
                base.Divisor = value;

                __ModifiedProperties__.Add("Divisor");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Sysctl defines a kernel parameter to be set
    /// </summary>
    public partial class SysctlV1
    {
        /// <summary>
        ///     Value of a property to set
        /// </summary>
        [JsonProperty("value")]
        [YamlMember(Alias = "value")]
        public string Value { get; set; }

        /// <summary>
        ///     Name of a property to set
        /// </summary>
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public string Name { get; set; }
    }
}

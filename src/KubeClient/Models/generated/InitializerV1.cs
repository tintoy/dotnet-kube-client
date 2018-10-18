using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Initializer is information about an initializer that has not yet completed.
    /// </summary>
    public partial class InitializerV1
    {
        /// <summary>
        ///     name of the process that is responsible for initializing this object.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }
    }
}

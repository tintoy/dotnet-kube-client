using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Info contains versioning information. how we'll want to distribute that information.
    /// </summary>
    public partial class InfoVersion
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "buildDate")]
        [JsonProperty("buildDate", NullValueHandling = NullValueHandling.Include)]
        public string BuildDate { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "gitTreeState")]
        [JsonProperty("gitTreeState", NullValueHandling = NullValueHandling.Include)]
        public string GitTreeState { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "platform")]
        [JsonProperty("platform", NullValueHandling = NullValueHandling.Include)]
        public string Platform { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "gitVersion")]
        [JsonProperty("gitVersion", NullValueHandling = NullValueHandling.Include)]
        public string GitVersion { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "goVersion")]
        [JsonProperty("goVersion", NullValueHandling = NullValueHandling.Include)]
        public string GoVersion { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "compiler")]
        [JsonProperty("compiler", NullValueHandling = NullValueHandling.Include)]
        public string Compiler { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "major")]
        [JsonProperty("major", NullValueHandling = NullValueHandling.Include)]
        public string Major { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "minor")]
        [JsonProperty("minor", NullValueHandling = NullValueHandling.Include)]
        public string Minor { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "gitCommit")]
        [JsonProperty("gitCommit", NullValueHandling = NullValueHandling.Include)]
        public string GitCommit { get; set; }
    }
}

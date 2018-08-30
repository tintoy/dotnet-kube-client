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
        [JsonProperty("buildDate")]
        [YamlMember(Alias = "buildDate")]
        public virtual string BuildDate { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("gitTreeState")]
        [YamlMember(Alias = "gitTreeState")]
        public virtual string GitTreeState { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("platform")]
        [YamlMember(Alias = "platform")]
        public virtual string Platform { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("gitVersion")]
        [YamlMember(Alias = "gitVersion")]
        public virtual string GitVersion { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("goVersion")]
        [YamlMember(Alias = "goVersion")]
        public virtual string GoVersion { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("compiler")]
        [YamlMember(Alias = "compiler")]
        public virtual string Compiler { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("major")]
        [YamlMember(Alias = "major")]
        public virtual string Major { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("minor")]
        [YamlMember(Alias = "minor")]
        public virtual string Minor { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("gitCommit")]
        [YamlMember(Alias = "gitCommit")]
        public virtual string GitCommit { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Info contains versioning information. how we'll want to distribute that information.
    /// </summary>
    [KubeObject("Info", "version")]
    public class InfoVersion
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("buildDate")]
        public string BuildDate { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("gitTreeState")]
        public string GitTreeState { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("platform")]
        public string Platform { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("gitVersion")]
        public string GitVersion { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("goVersion")]
        public string GoVersion { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("compiler")]
        public string Compiler { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("major")]
        public string Major { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("minor")]
        public string Minor { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("gitCommit")]
        public string GitCommit { get; set; }
    }
}

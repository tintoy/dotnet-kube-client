using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    public partial class CustomResourceDefinitionVersionV1Beta1
    {
        /// <summary>
        ///     Served is a flag enabling/disabling this version from being served via REST APIs
        /// </summary>
        [JsonProperty("served")]
        [YamlMember(Alias = "served")]
        public bool Served { get; set; }

        /// <summary>
        ///     Name is the version name, e.g. “v1”, “v2beta1”, etc.
        /// </summary>
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        /// <summary>
        ///     Storage flags the version as storage version. There must be exactly one flagged as storage version.
        /// </summary>
        [JsonProperty("storage")]
        [YamlMember(Alias = "storage")]
        public bool Storage { get; set; }
    }
}

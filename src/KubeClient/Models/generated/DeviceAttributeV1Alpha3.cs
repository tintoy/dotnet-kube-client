using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DeviceAttribute must have exactly one field set.
    /// </summary>
    public partial class DeviceAttributeV1Alpha3
    {
        /// <summary>
        ///     StringValue is a string. Must not be longer than 64 characters.
        /// </summary>
        [YamlMember(Alias = "string")]
        [JsonProperty("string", NullValueHandling = NullValueHandling.Ignore)]
        public string String { get; set; }

        /// <summary>
        ///     BoolValue is a true/false value.
        /// </summary>
        [YamlMember(Alias = "bool")]
        [JsonProperty("bool", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Bool { get; set; }

        /// <summary>
        ///     VersionValue is a semantic version according to semver.org spec 2.0.0. Must not be longer than 64 characters.
        /// </summary>
        [YamlMember(Alias = "version")]
        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set; }

        /// <summary>
        ///     IntValue is a number.
        /// </summary>
        [YamlMember(Alias = "int")]
        [JsonProperty("int", NullValueHandling = NullValueHandling.Ignore)]
        public long? Int { get; set; }
    }
}

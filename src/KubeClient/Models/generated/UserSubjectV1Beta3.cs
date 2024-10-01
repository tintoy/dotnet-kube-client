using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     UserSubject holds detailed information for user-kind subject.
    /// </summary>
    public partial class UserSubjectV1Beta3
    {
        /// <summary>
        ///     `name` is the username that matches, or "*" to match all usernames. Required.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }
    }
}

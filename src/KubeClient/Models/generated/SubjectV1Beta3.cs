using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Subject matches the originator of a request, as identified by the request authentication system. There are three ways of matching an originator; by user, group, or service account.
    /// </summary>
    public partial class SubjectV1Beta3
    {
        /// <summary>
        ///     `kind` indicates which one of the other fields is non-empty. Required
        /// </summary>
        [YamlMember(Alias = "kind")]
        [JsonProperty("kind", NullValueHandling = NullValueHandling.Include)]
        public string Kind { get; set; }

        /// <summary>
        ///     `group` matches based on user group name.
        /// </summary>
        [YamlMember(Alias = "group")]
        [JsonProperty("group", NullValueHandling = NullValueHandling.Ignore)]
        public GroupSubjectV1Beta3 Group { get; set; }

        /// <summary>
        ///     `user` matches based on username.
        /// </summary>
        [YamlMember(Alias = "user")]
        [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
        public UserSubjectV1Beta3 User { get; set; }

        /// <summary>
        ///     `serviceAccount` matches ServiceAccounts.
        /// </summary>
        [YamlMember(Alias = "serviceAccount")]
        [JsonProperty("serviceAccount", NullValueHandling = NullValueHandling.Ignore)]
        public ServiceAccountSubjectV1Beta3 ServiceAccount { get; set; }
    }
}

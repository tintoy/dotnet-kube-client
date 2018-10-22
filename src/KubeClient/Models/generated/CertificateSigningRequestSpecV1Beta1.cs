using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     This information is immutable after the request is created. Only the Request and Usages fields can be set on creation, other fields are derived by Kubernetes and cannot be modified by users.
    /// </summary>
    public partial class CertificateSigningRequestSpecV1Beta1
    {
        /// <summary>
        ///     Extra information about the requesting user. See user.Info interface for details.
        /// </summary>
        [YamlMember(Alias = "extra")]
        [JsonProperty("extra", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, List<string>> Extra { get; } = new Dictionary<string, List<string>>();

        /// <summary>
        ///     Determine whether the <see cref="Extra"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeExtra() => Extra.Count > 0;

        /// <summary>
        ///     UID information about the requesting user. See user.Info interface for details.
        /// </summary>
        [YamlMember(Alias = "uid")]
        [JsonProperty("uid", NullValueHandling = NullValueHandling.Ignore)]
        public string Uid { get; set; }

        /// <summary>
        ///     Information about the requesting user. See user.Info interface for details.
        /// </summary>
        [YamlMember(Alias = "username")]
        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }

        /// <summary>
        ///     Group information about the requesting user. See user.Info interface for details.
        /// </summary>
        [YamlMember(Alias = "groups")]
        [JsonProperty("groups", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Groups { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Groups"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeGroups() => Groups.Count > 0;

        /// <summary>
        ///     allowedUsages specifies a set of usage contexts the key will be valid for. See: https://tools.ietf.org/html/rfc5280#section-4.2.1.3
        ///          https://tools.ietf.org/html/rfc5280#section-4.2.1.12
        /// </summary>
        [YamlMember(Alias = "usages")]
        [JsonProperty("usages", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Usages { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Usages"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeUsages() => Usages.Count > 0;

        /// <summary>
        ///     Base64-encoded PKCS#10 CSR data
        /// </summary>
        [YamlMember(Alias = "request")]
        [JsonProperty("request", NullValueHandling = NullValueHandling.Include)]
        public string Request { get; set; }
    }
}

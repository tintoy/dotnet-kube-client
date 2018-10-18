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
        [JsonProperty("extra", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, List<string>> Extra { get; set; } = new Dictionary<string, List<string>>();

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
        [JsonProperty("groups", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Groups { get; set; } = new List<string>();

        /// <summary>
        ///     allowedUsages specifies a set of usage contexts the key will be valid for. See: https://tools.ietf.org/html/rfc5280#section-4.2.1.3
        ///          https://tools.ietf.org/html/rfc5280#section-4.2.1.12
        /// </summary>
        [YamlMember(Alias = "usages")]
        [JsonProperty("usages", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Usages { get; set; } = new List<string>();

        /// <summary>
        ///     Base64-encoded PKCS#10 CSR data
        /// </summary>
        [YamlMember(Alias = "request")]
        [JsonProperty("request", NullValueHandling = NullValueHandling.Include)]
        public string Request { get; set; }
    }
}

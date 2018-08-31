using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     This information is immutable after the request is created. Only the Request and Usages fields can be set on creation, other fields are derived by Kubernetes and cannot be modified by users.
    /// </summary>
    public partial class CertificateSigningRequestSpecV1Beta1 : Models.CertificateSigningRequestSpecV1Beta1
    {
        /// <summary>
        ///     Extra information about the requesting user. See user.Info interface for details.
        /// </summary>
        [YamlMember(Alias = "extra")]
        [JsonProperty("extra", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, List<string>> Extra { get; set; } = new Dictionary<string, List<string>>();

        /// <summary>
        ///     UID information about the requesting user. See user.Info interface for details.
        /// </summary>
        [JsonProperty("uid")]
        [YamlMember(Alias = "uid")]
        public override string Uid
        {
            get
            {
                return base.Uid;
            }
            set
            {
                base.Uid = value;

                __ModifiedProperties__.Add("Uid");
            }
        }


        /// <summary>
        ///     Information about the requesting user. See user.Info interface for details.
        /// </summary>
        [JsonProperty("username")]
        [YamlMember(Alias = "username")]
        public override string Username
        {
            get
            {
                return base.Username;
            }
            set
            {
                base.Username = value;

                __ModifiedProperties__.Add("Username");
            }
        }


        /// <summary>
        ///     Group information about the requesting user. See user.Info interface for details.
        /// </summary>
        [YamlMember(Alias = "groups")]
        [JsonProperty("groups", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> Groups { get; set; } = new List<string>();

        /// <summary>
        ///     allowedUsages specifies a set of usage contexts the key will be valid for. See: https://tools.ietf.org/html/rfc5280#section-4.2.1.3
        ///          https://tools.ietf.org/html/rfc5280#section-4.2.1.12
        /// </summary>
        [YamlMember(Alias = "usages")]
        [JsonProperty("usages", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> Usages { get; set; } = new List<string>();

        /// <summary>
        ///     Base64-encoded PKCS#10 CSR data
        /// </summary>
        [JsonProperty("request")]
        [YamlMember(Alias = "request")]
        public override string Request
        {
            get
            {
                return base.Request;
            }
            set
            {
                base.Request = value;

                __ModifiedProperties__.Add("Request");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}

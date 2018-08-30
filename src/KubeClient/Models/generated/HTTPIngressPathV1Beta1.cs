using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     HTTPIngressPath associates a path regex with a backend. Incoming urls matching the path are forwarded to the backend.
    /// </summary>
    public partial class HTTPIngressPathV1Beta1
    {
        /// <summary>
        ///     Backend defines the referenced service endpoint to which the traffic will be forwarded to.
        /// </summary>
        [JsonProperty("backend")]
        [YamlMember(Alias = "backend")]
        public virtual IngressBackendV1Beta1 Backend { get; set; }

        /// <summary>
        ///     Path is an extended POSIX regex as defined by IEEE Std 1003.1, (i.e this follows the egrep/unix syntax, not the perl syntax) matched against the path of an incoming request. Currently it can contain characters disallowed from the conventional "path" part of a URL as defined by RFC 3986. Paths must begin with a '/'. If unspecified, the path defaults to a catch all sending traffic to the backend.
        /// </summary>
        [JsonProperty("path")]
        [YamlMember(Alias = "path")]
        public virtual string Path { get; set; }
    }
}

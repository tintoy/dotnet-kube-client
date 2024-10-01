using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     HTTPIngressPath associates a path with a backend. Incoming urls matching the path are forwarded to the backend.
    /// </summary>
    public partial class HTTPIngressPathV1
    {
        /// <summary>
        ///     backend defines the referenced service endpoint to which the traffic will be forwarded to.
        /// </summary>
        [YamlMember(Alias = "backend")]
        [JsonProperty("backend", NullValueHandling = NullValueHandling.Include)]
        public IngressBackendV1 Backend { get; set; }

        /// <summary>
        ///     pathType determines the interpretation of the path matching. PathType can be one of the following values: * Exact: Matches the URL path exactly. * Prefix: Matches based on a URL path prefix split by '/'. Matching is
        ///       done on a path element by element basis. A path element refers is the
        ///       list of labels in the path split by the '/' separator. A request is a
        ///       match for path p if every p is an element-wise prefix of p of the
        ///       request path. Note that if the last element of the path is a substring
        ///       of the last element in request path, it is not a match (e.g. /foo/bar
        ///       matches /foo/bar/baz, but does not match /foo/barbaz).
        ///     * ImplementationSpecific: Interpretation of the Path matching is up to
        ///       the IngressClass. Implementations can treat this as a separate PathType
        ///       or treat it identically to Prefix or Exact path types.
        ///     Implementations are required to support all path types.
        /// </summary>
        [YamlMember(Alias = "pathType")]
        [JsonProperty("pathType", NullValueHandling = NullValueHandling.Include)]
        public string PathType { get; set; }

        /// <summary>
        ///     path is matched against the path of an incoming request. Currently it can contain characters disallowed from the conventional "path" part of a URL as defined by RFC 3986. Paths must begin with a '/' and must be present when using PathType with value "Exact" or "Prefix".
        /// </summary>
        [YamlMember(Alias = "path")]
        [JsonProperty("path", NullValueHandling = NullValueHandling.Ignore)]
        public string Path { get; set; }
    }
}

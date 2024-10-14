using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ClusterTrustBundleProjection describes how to select a set of ClusterTrustBundle objects and project their contents into the pod filesystem.
    /// </summary>
    public partial class ClusterTrustBundleProjectionV1
    {
        /// <summary>
        ///     Select a single ClusterTrustBundle by object name.  Mutually-exclusive with signerName and labelSelector.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        ///     Select all ClusterTrustBundles that match this signer name. Mutually-exclusive with name.  The contents of all selected ClusterTrustBundles will be unified and deduplicated.
        /// </summary>
        [YamlMember(Alias = "signerName")]
        [JsonProperty("signerName", NullValueHandling = NullValueHandling.Ignore)]
        public string SignerName { get; set; }

        /// <summary>
        ///     Relative path from the volume root to write the bundle.
        /// </summary>
        [YamlMember(Alias = "path")]
        [JsonProperty("path", NullValueHandling = NullValueHandling.Include)]
        public string Path { get; set; }

        /// <summary>
        ///     If true, don't block pod startup if the referenced ClusterTrustBundle(s) aren't available.  If using name, then the named ClusterTrustBundle is allowed not to exist.  If using signerName, then the combination of signerName and labelSelector is allowed to match zero ClusterTrustBundles.
        /// </summary>
        [YamlMember(Alias = "optional")]
        [JsonProperty("optional", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Optional { get; set; }

        /// <summary>
        ///     Select all ClusterTrustBundles that match this label selector.  Only has effect if signerName is set.  Mutually-exclusive with name.  If unset, interpreted as "match nothing".  If set but empty, interpreted as "match everything".
        /// </summary>
        [YamlMember(Alias = "labelSelector")]
        [JsonProperty("labelSelector", NullValueHandling = NullValueHandling.Ignore)]
        public LabelSelectorV1 LabelSelector { get; set; }
    }
}

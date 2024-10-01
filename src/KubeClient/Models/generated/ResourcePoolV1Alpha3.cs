using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourcePool describes the pool that ResourceSlices belong to.
    /// </summary>
    public partial class ResourcePoolV1Alpha3
    {
        /// <summary>
        ///     Name is used to identify the pool. For node-local devices, this is often the node name, but this is not required.
        ///     
        ///     It must not be longer than 253 characters and must consist of one or more DNS sub-domains separated by slashes. This field is immutable.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     Generation tracks the change in a pool over time. Whenever a driver changes something about one or more of the resources in a pool, it must change the generation in all ResourceSlices which are part of that pool. Consumers of ResourceSlices should only consider resources from the pool with the highest generation number. The generation may be reset by drivers, which should be fine for consumers, assuming that all ResourceSlices in a pool are updated to match or deleted.
        ///     
        ///     Combined with ResourceSliceCount, this mechanism enables consumers to detect pools which are comprised of multiple ResourceSlices and are in an incomplete state.
        /// </summary>
        [YamlMember(Alias = "generation")]
        [JsonProperty("generation", NullValueHandling = NullValueHandling.Include)]
        public long Generation { get; set; }

        /// <summary>
        ///     ResourceSliceCount is the total number of ResourceSlices in the pool at this generation number. Must be greater than zero.
        ///     
        ///     Consumers can use this to check whether they have seen all ResourceSlices belonging to the same pool.
        /// </summary>
        [YamlMember(Alias = "resourceSliceCount")]
        [JsonProperty("resourceSliceCount", NullValueHandling = NullValueHandling.Include)]
        public long ResourceSliceCount { get; set; }
    }
}

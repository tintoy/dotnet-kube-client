using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DEPRECATED - This group version of ControllerRevision is deprecated by apps/v1beta2/ControllerRevision. See the release notes for more information. ControllerRevision implements an immutable snapshot of state data. Clients are responsible for serializing and deserializing the objects that contain their internal state. Once a ControllerRevision has been successfully created, it can not be updated. The API Server will fail validation of all requests that attempt to mutate the Data field. ControllerRevisions may, however, be deleted. Note that, due to its use by both the DaemonSet and StatefulSet controllers for update and rollback, this object is beta. However, it may be subject to name and representation changes in future releases, and clients should not depend on its stability. It is primarily for internal use by controllers.
    /// </summary>
    [KubeObject("ControllerRevision", "apps/v1beta1")]
    [KubeApi(KubeAction.List, "apis/apps/v1beta1/controllerrevisions")]
    [KubeApi(KubeAction.WatchList, "apis/apps/v1beta1/watch/controllerrevisions")]
    [KubeApi(KubeAction.List, "apis/apps/v1beta1/namespaces/{namespace}/controllerrevisions")]
    [KubeApi(KubeAction.Create, "apis/apps/v1beta1/namespaces/{namespace}/controllerrevisions")]
    [KubeApi(KubeAction.Get, "apis/apps/v1beta1/namespaces/{namespace}/controllerrevisions/{name}")]
    [KubeApi(KubeAction.Patch, "apis/apps/v1beta1/namespaces/{namespace}/controllerrevisions/{name}")]
    [KubeApi(KubeAction.Delete, "apis/apps/v1beta1/namespaces/{namespace}/controllerrevisions/{name}")]
    [KubeApi(KubeAction.Update, "apis/apps/v1beta1/namespaces/{namespace}/controllerrevisions/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/apps/v1beta1/watch/namespaces/{namespace}/controllerrevisions")]
    [KubeApi(KubeAction.DeleteCollection, "apis/apps/v1beta1/namespaces/{namespace}/controllerrevisions")]
    [KubeApi(KubeAction.Watch, "apis/apps/v1beta1/watch/namespaces/{namespace}/controllerrevisions/{name}")]
    public partial class ControllerRevisionV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Data is the serialized representation of the state.
        /// </summary>
        [YamlMember(Alias = "data")]
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public RawExtensionRuntime Data { get; set; }

        /// <summary>
        ///     Revision indicates the revision of the state represented by Data.
        /// </summary>
        [YamlMember(Alias = "revision")]
        [JsonProperty("revision", NullValueHandling = NullValueHandling.Include)]
        public long Revision { get; set; }
    }
}

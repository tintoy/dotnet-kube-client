using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ControllerRevision implements an immutable snapshot of state data. Clients are responsible for serializing and deserializing the objects that contain their internal state. Once a ControllerRevision has been successfully created, it can not be updated. The API Server will fail validation of all requests that attempt to mutate the Data field. ControllerRevisions may, however, be deleted. Note that, due to its use by both the DaemonSet and StatefulSet controllers for update and rollback, this object is beta. However, it may be subject to name and representation changes in future releases, and clients should not depend on its stability. It is primarily for internal use by controllers.
    /// </summary>
    [KubeObject("ControllerRevision", "apps/v1beta1")]
    public partial class ControllerRevisionV1Beta1 : Models.ControllerRevisionV1Beta1, ITracked
    {
        /// <summary>
        ///     Data is the serialized representation of the state.
        /// </summary>
        [JsonProperty("data")]
        [YamlMember(Alias = "data")]
        public override Models.RawExtensionRuntime Data
        {
            get
            {
                return base.Data;
            }
            set
            {
                base.Data = value;

                __ModifiedProperties__.Add("Data");
            }
        }


        /// <summary>
        ///     Revision indicates the revision of the state represented by Data.
        /// </summary>
        [JsonProperty("revision")]
        [YamlMember(Alias = "revision")]
        public override int Revision
        {
            get
            {
                return base.Revision;
            }
            set
            {
                base.Revision = value;

                __ModifiedProperties__.Add("Revision");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}

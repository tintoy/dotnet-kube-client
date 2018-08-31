using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ContainerStatus contains details for the current status of this container.
    /// </summary>
    public partial class ContainerStatusV1 : Models.ContainerStatusV1, ITracked
    {
        /// <summary>
        ///     Container's ID in the format 'docker://&lt;container_id&gt;'.
        /// </summary>
        [JsonProperty("containerID")]
        [YamlMember(Alias = "containerID")]
        public override string ContainerID
        {
            get
            {
                return base.ContainerID;
            }
            set
            {
                base.ContainerID = value;

                __ModifiedProperties__.Add("ContainerID");
            }
        }


        /// <summary>
        ///     ImageID of the container's image.
        /// </summary>
        [JsonProperty("imageID")]
        [YamlMember(Alias = "imageID")]
        public override string ImageID
        {
            get
            {
                return base.ImageID;
            }
            set
            {
                base.ImageID = value;

                __ModifiedProperties__.Add("ImageID");
            }
        }


        /// <summary>
        ///     The image the container is running. More info: https://kubernetes.io/docs/concepts/containers/images
        /// </summary>
        [JsonProperty("image")]
        [YamlMember(Alias = "image")]
        public override string Image
        {
            get
            {
                return base.Image;
            }
            set
            {
                base.Image = value;

                __ModifiedProperties__.Add("Image");
            }
        }


        /// <summary>
        ///     Details about the container's last termination condition.
        /// </summary>
        [JsonProperty("lastState")]
        [YamlMember(Alias = "lastState")]
        public override Models.ContainerStateV1 LastState
        {
            get
            {
                return base.LastState;
            }
            set
            {
                base.LastState = value;

                __ModifiedProperties__.Add("LastState");
            }
        }


        /// <summary>
        ///     This must be a DNS_LABEL. Each container in a pod must have a unique name. Cannot be updated.
        /// </summary>
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;

                __ModifiedProperties__.Add("Name");
            }
        }


        /// <summary>
        ///     Details about the container's current condition.
        /// </summary>
        [JsonProperty("state")]
        [YamlMember(Alias = "state")]
        public override Models.ContainerStateV1 State
        {
            get
            {
                return base.State;
            }
            set
            {
                base.State = value;

                __ModifiedProperties__.Add("State");
            }
        }


        /// <summary>
        ///     The number of times the container has been restarted, currently based on the number of dead containers that have not yet been removed. Note that this is calculated from dead containers. But those containers are subject to garbage collection. This value will get capped at 5 by GC.
        /// </summary>
        [JsonProperty("restartCount")]
        [YamlMember(Alias = "restartCount")]
        public override int RestartCount
        {
            get
            {
                return base.RestartCount;
            }
            set
            {
                base.RestartCount = value;

                __ModifiedProperties__.Add("RestartCount");
            }
        }


        /// <summary>
        ///     Specifies whether the container has passed its readiness probe.
        /// </summary>
        [JsonProperty("ready")]
        [YamlMember(Alias = "ready")]
        public override bool Ready
        {
            get
            {
                return base.Ready;
            }
            set
            {
                base.Ready = value;

                __ModifiedProperties__.Add("Ready");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}

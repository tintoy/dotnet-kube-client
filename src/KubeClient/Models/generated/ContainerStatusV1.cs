using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ContainerStatus contains details for the current status of this container.
    /// </summary>
    public partial class ContainerStatusV1
    {
        /// <summary>
        ///     ContainerID is the ID of the container in the format '&lt;type&gt;://&lt;container_id&gt;'. Where type is a container runtime identifier, returned from Version call of CRI API (for example "containerd").
        /// </summary>
        [YamlMember(Alias = "containerID")]
        [JsonProperty("containerID", NullValueHandling = NullValueHandling.Ignore)]
        public string ContainerID { get; set; }

        /// <summary>
        ///     ImageID is the image ID of the container's image. The image ID may not match the image ID of the image used in the PodSpec, as it may have been resolved by the runtime.
        /// </summary>
        [YamlMember(Alias = "imageID")]
        [JsonProperty("imageID", NullValueHandling = NullValueHandling.Include)]
        public string ImageID { get; set; }

        /// <summary>
        ///     Started indicates whether the container has finished its postStart lifecycle hook and passed its startup probe. Initialized as false, becomes true after startupProbe is considered successful. Resets to false when the container is restarted, or if kubelet loses state temporarily. In both cases, startup probes will run again. Is always true when no startupProbe is defined and container is running and has passed the postStart lifecycle hook. The null value must be treated the same as false.
        /// </summary>
        [YamlMember(Alias = "started")]
        [JsonProperty("started", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Started { get; set; }

        /// <summary>
        ///     Image is the name of container image that the container is running. The container image may not match the image used in the PodSpec, as it may have been resolved by the runtime. More info: https://kubernetes.io/docs/concepts/containers/images.
        /// </summary>
        [YamlMember(Alias = "image")]
        [JsonProperty("image", NullValueHandling = NullValueHandling.Include)]
        public string Image { get; set; }

        /// <summary>
        ///     LastTerminationState holds the last termination state of the container to help debug container crashes and restarts. This field is not populated if the container is still running and RestartCount is 0.
        /// </summary>
        [YamlMember(Alias = "lastState")]
        [JsonProperty("lastState", NullValueHandling = NullValueHandling.Ignore)]
        public ContainerStateV1 LastState { get; set; }

        /// <summary>
        ///     Name is a DNS_LABEL representing the unique name of the container. Each container in a pod must have a unique name across all container types. Cannot be updated.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     State holds details about the container's current condition.
        /// </summary>
        [YamlMember(Alias = "state")]
        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public ContainerStateV1 State { get; set; }

        /// <summary>
        ///     User represents user identity information initially attached to the first process of the container
        /// </summary>
        [YamlMember(Alias = "user")]
        [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
        public ContainerUserV1 User { get; set; }

        /// <summary>
        ///     AllocatedResources represents the compute resources allocated for this container by the node. Kubelet sets this value to Container.Resources.Requests upon successful pod admission and after successfully admitting desired pod resize.
        /// </summary>
        [YamlMember(Alias = "allocatedResources")]
        [JsonProperty("allocatedResources", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> AllocatedResources { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="AllocatedResources"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeAllocatedResources() => AllocatedResources.Count > 0;

        /// <summary>
        ///     AllocatedResourcesStatus represents the status of various resources allocated for this Pod.
        /// </summary>
        [MergeStrategy(Key = "name")]
        [YamlMember(Alias = "allocatedResourcesStatus")]
        [JsonProperty("allocatedResourcesStatus", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<ResourceStatusV1> AllocatedResourcesStatus { get; } = new List<ResourceStatusV1>();

        /// <summary>
        ///     Determine whether the <see cref="AllocatedResourcesStatus"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeAllocatedResourcesStatus() => AllocatedResourcesStatus.Count > 0;

        /// <summary>
        ///     Resources represents the compute resource requests and limits that have been successfully enacted on the running container after it has been started or has been successfully resized.
        /// </summary>
        [YamlMember(Alias = "resources")]
        [JsonProperty("resources", NullValueHandling = NullValueHandling.Ignore)]
        public ResourceRequirementsV1 Resources { get; set; }

        /// <summary>
        ///     Status of volume mounts.
        /// </summary>
        [MergeStrategy(Key = "mountPath")]
        [YamlMember(Alias = "volumeMounts")]
        [JsonProperty("volumeMounts", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<VolumeMountStatusV1> VolumeMounts { get; } = new List<VolumeMountStatusV1>();

        /// <summary>
        ///     Determine whether the <see cref="VolumeMounts"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeVolumeMounts() => VolumeMounts.Count > 0;

        /// <summary>
        ///     RestartCount holds the number of times the container has been restarted. Kubelet makes an effort to always increment the value, but there are cases when the state may be lost due to node restarts and then the value may be reset to 0. The value is never negative.
        /// </summary>
        [YamlMember(Alias = "restartCount")]
        [JsonProperty("restartCount", NullValueHandling = NullValueHandling.Include)]
        public int RestartCount { get; set; }

        /// <summary>
        ///     Ready specifies whether the container is currently passing its readiness check. The value will change as readiness probes keep executing. If no readiness probes are specified, this field defaults to true once the container is fully started (see Started field).
        ///     
        ///     The value is typically used to determine whether a container is ready to accept traffic.
        /// </summary>
        [YamlMember(Alias = "ready")]
        [JsonProperty("ready", NullValueHandling = NullValueHandling.Include)]
        public bool Ready { get; set; }
    }
}

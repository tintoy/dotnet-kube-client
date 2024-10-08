using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     A single application container that you want to run within a pod.
    /// </summary>
    public partial class ContainerV1
    {
        /// <summary>
        ///     Entrypoint array. Not executed within a shell. The container image's ENTRYPOINT is used if this is not provided. Variable references $(VAR_NAME) are expanded using the container's environment. If a variable cannot be resolved, the reference in the input string will be unchanged. Double $$ are reduced to a single $, which allows for escaping the $(VAR_NAME) syntax: i.e. "$$(VAR_NAME)" will produce the string literal "$(VAR_NAME)". Escaped references will never be expanded, regardless of whether the variable exists or not. Cannot be updated. More info: https://kubernetes.io/docs/tasks/inject-data-application/define-command-argument-container/#running-a-command-in-a-shell
        /// </summary>
        [YamlMember(Alias = "command")]
        [JsonProperty("command", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Command { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Command"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeCommand() => Command.Count > 0;

        /// <summary>
        ///     Container image name. More info: https://kubernetes.io/docs/concepts/containers/images This field is optional to allow higher level config management to default or override container images in workload controllers like Deployments and StatefulSets.
        /// </summary>
        [YamlMember(Alias = "image")]
        [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
        public string Image { get; set; }

        /// <summary>
        ///     Actions that the management system should take in response to container lifecycle events. Cannot be updated.
        /// </summary>
        [YamlMember(Alias = "lifecycle")]
        [JsonProperty("lifecycle", NullValueHandling = NullValueHandling.Ignore)]
        public LifecycleV1 Lifecycle { get; set; }

        /// <summary>
        ///     Periodic probe of container liveness. Container will be restarted if the probe fails. Cannot be updated. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#container-probes
        /// </summary>
        [YamlMember(Alias = "livenessProbe")]
        [JsonProperty("livenessProbe", NullValueHandling = NullValueHandling.Ignore)]
        public ProbeV1 LivenessProbe { get; set; }

        /// <summary>
        ///     Name of the container specified as a DNS_LABEL. Each container in a pod must have a unique name (DNS_LABEL). Cannot be updated.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     Periodic probe of container service readiness. Container will be removed from service endpoints if the probe fails. Cannot be updated. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#container-probes
        /// </summary>
        [YamlMember(Alias = "readinessProbe")]
        [JsonProperty("readinessProbe", NullValueHandling = NullValueHandling.Ignore)]
        public ProbeV1 ReadinessProbe { get; set; }

        /// <summary>
        ///     StartupProbe indicates that the Pod has successfully initialized. If specified, no other probes are executed until this completes successfully. If this probe fails, the Pod will be restarted, just as if the livenessProbe failed. This can be used to provide different probe parameters at the beginning of a Pod's lifecycle, when it might take a long time to load data or warm a cache, than during steady-state operation. This cannot be updated. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#container-probes
        /// </summary>
        [YamlMember(Alias = "startupProbe")]
        [JsonProperty("startupProbe", NullValueHandling = NullValueHandling.Ignore)]
        public ProbeV1 StartupProbe { get; set; }

        /// <summary>
        ///     Whether the container runtime should close the stdin channel after it has been opened by a single attach. When stdin is true the stdin stream will remain open across multiple attach sessions. If stdinOnce is set to true, stdin is opened on container start, is empty until the first client attaches to stdin, and then remains open and accepts data until the client disconnects, at which time stdin is closed and remains closed until the container is restarted. If this flag is false, a container processes that reads from stdin will never receive an EOF. Default is false
        /// </summary>
        [YamlMember(Alias = "stdinOnce")]
        [JsonProperty("stdinOnce", NullValueHandling = NullValueHandling.Ignore)]
        public bool? StdinOnce { get; set; }

        /// <summary>
        ///     Optional: Path at which the file to which the container's termination message will be written is mounted into the container's filesystem. Message written is intended to be brief final status, such as an assertion failure message. Will be truncated by the node if greater than 4096 bytes. The total message length across all containers will be limited to 12kb. Defaults to /dev/termination-log. Cannot be updated.
        /// </summary>
        [YamlMember(Alias = "terminationMessagePath")]
        [JsonProperty("terminationMessagePath", NullValueHandling = NullValueHandling.Ignore)]
        public string TerminationMessagePath { get; set; }

        /// <summary>
        ///     List of sources to populate environment variables in the container. The keys defined within a source must be a C_IDENTIFIER. All invalid keys will be reported as an event when the container is starting. When a key exists in multiple sources, the value associated with the last source will take precedence. Values defined by an Env with a duplicate key will take precedence. Cannot be updated.
        /// </summary>
        [YamlMember(Alias = "envFrom")]
        [JsonProperty("envFrom", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<EnvFromSourceV1> EnvFrom { get; } = new List<EnvFromSourceV1>();

        /// <summary>
        ///     Determine whether the <see cref="EnvFrom"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeEnvFrom() => EnvFrom.Count > 0;

        /// <summary>
        ///     Whether this container should allocate a buffer for stdin in the container runtime. If this is not set, reads from stdin in the container will always result in EOF. Default is false.
        /// </summary>
        [YamlMember(Alias = "stdin")]
        [JsonProperty("stdin", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Stdin { get; set; }

        /// <summary>
        ///     Container's working directory. If not specified, the container runtime's default will be used, which might be configured in the container image. Cannot be updated.
        /// </summary>
        [YamlMember(Alias = "workingDir")]
        [JsonProperty("workingDir", NullValueHandling = NullValueHandling.Ignore)]
        public string WorkingDir { get; set; }

        /// <summary>
        ///     Arguments to the entrypoint. The container image's CMD is used if this is not provided. Variable references $(VAR_NAME) are expanded using the container's environment. If a variable cannot be resolved, the reference in the input string will be unchanged. Double $$ are reduced to a single $, which allows for escaping the $(VAR_NAME) syntax: i.e. "$$(VAR_NAME)" will produce the string literal "$(VAR_NAME)". Escaped references will never be expanded, regardless of whether the variable exists or not. Cannot be updated. More info: https://kubernetes.io/docs/tasks/inject-data-application/define-command-argument-container/#running-a-command-in-a-shell
        /// </summary>
        [YamlMember(Alias = "args")]
        [JsonProperty("args", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Args { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Args"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeArgs() => Args.Count > 0;

        /// <summary>
        ///     List of ports to expose from the container. Not specifying a port here DOES NOT prevent that port from being exposed. Any port which is listening on the default "0.0.0.0" address inside a container will be accessible from the network. Modifying this array with strategic merge patch may corrupt the data. For more information See https://github.com/kubernetes/kubernetes/issues/108255. Cannot be updated.
        /// </summary>
        [YamlMember(Alias = "ports")]
        [MergeStrategy(Key = "containerPort")]
        [JsonProperty("ports", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<ContainerPortV1> Ports { get; } = new List<ContainerPortV1>();

        /// <summary>
        ///     Determine whether the <see cref="Ports"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializePorts() => Ports.Count > 0;

        /// <summary>
        ///     Compute Resources required by this container. Cannot be updated. More info: https://kubernetes.io/docs/concepts/configuration/manage-resources-containers/
        /// </summary>
        [YamlMember(Alias = "resources")]
        [JsonProperty("resources", NullValueHandling = NullValueHandling.Ignore)]
        public ResourceRequirementsV1 Resources { get; set; }

        /// <summary>
        ///     volumeDevices is the list of block devices to be used by the container.
        /// </summary>
        [MergeStrategy(Key = "devicePath")]
        [YamlMember(Alias = "volumeDevices")]
        [JsonProperty("volumeDevices", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<VolumeDeviceV1> VolumeDevices { get; } = new List<VolumeDeviceV1>();

        /// <summary>
        ///     Determine whether the <see cref="VolumeDevices"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeVolumeDevices() => VolumeDevices.Count > 0;

        /// <summary>
        ///     Pod volumes to mount into the container's filesystem. Cannot be updated.
        /// </summary>
        [MergeStrategy(Key = "mountPath")]
        [YamlMember(Alias = "volumeMounts")]
        [JsonProperty("volumeMounts", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<VolumeMountV1> VolumeMounts { get; } = new List<VolumeMountV1>();

        /// <summary>
        ///     Determine whether the <see cref="VolumeMounts"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeVolumeMounts() => VolumeMounts.Count > 0;

        /// <summary>
        ///     SecurityContext defines the security options the container should be run with. If set, the fields of SecurityContext override the equivalent fields of PodSecurityContext. More info: https://kubernetes.io/docs/tasks/configure-pod-container/security-context/
        /// </summary>
        [YamlMember(Alias = "securityContext")]
        [JsonProperty("securityContext", NullValueHandling = NullValueHandling.Ignore)]
        public SecurityContextV1 SecurityContext { get; set; }

        /// <summary>
        ///     List of environment variables to set in the container. Cannot be updated.
        /// </summary>
        [YamlMember(Alias = "env")]
        [MergeStrategy(Key = "name")]
        [JsonProperty("env", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<EnvVarV1> Env { get; } = new List<EnvVarV1>();

        /// <summary>
        ///     Determine whether the <see cref="Env"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeEnv() => Env.Count > 0;

        /// <summary>
        ///     Image pull policy. One of Always, Never, IfNotPresent. Defaults to Always if :latest tag is specified, or IfNotPresent otherwise. Cannot be updated. More info: https://kubernetes.io/docs/concepts/containers/images#updating-images
        /// </summary>
        [YamlMember(Alias = "imagePullPolicy")]
        [JsonProperty("imagePullPolicy", NullValueHandling = NullValueHandling.Ignore)]
        public string ImagePullPolicy { get; set; }

        /// <summary>
        ///     Resources resize policy for the container.
        /// </summary>
        [YamlMember(Alias = "resizePolicy")]
        [JsonProperty("resizePolicy", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<ContainerResizePolicyV1> ResizePolicy { get; } = new List<ContainerResizePolicyV1>();

        /// <summary>
        ///     Determine whether the <see cref="ResizePolicy"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeResizePolicy() => ResizePolicy.Count > 0;

        /// <summary>
        ///     RestartPolicy defines the restart behavior of individual containers in a pod. This field may only be set for init containers, and the only allowed value is "Always". For non-init containers or when this field is not specified, the restart behavior is defined by the Pod's restart policy and the container type. Setting the RestartPolicy as "Always" for the init container will have the following effect: this init container will be continually restarted on exit until all regular containers have terminated. Once all regular containers have completed, all init containers with restartPolicy "Always" will be shut down. This lifecycle differs from normal init containers and is often referred to as a "sidecar" container. Although this init container still starts in the init container sequence, it does not wait for the container to complete before proceeding to the next init container. Instead, the next init container starts immediately after this init container is started, or after any startupProbe has successfully completed.
        /// </summary>
        [YamlMember(Alias = "restartPolicy")]
        [JsonProperty("restartPolicy", NullValueHandling = NullValueHandling.Ignore)]
        public string RestartPolicy { get; set; }

        /// <summary>
        ///     Indicate how the termination message should be populated. File will use the contents of terminationMessagePath to populate the container status message on both success and failure. FallbackToLogsOnError will use the last chunk of container log output if the termination message file is empty and the container exited with an error. The log output is limited to 2048 bytes or 80 lines, whichever is smaller. Defaults to File. Cannot be updated.
        /// </summary>
        [YamlMember(Alias = "terminationMessagePolicy")]
        [JsonProperty("terminationMessagePolicy", NullValueHandling = NullValueHandling.Ignore)]
        public string TerminationMessagePolicy { get; set; }

        /// <summary>
        ///     Whether this container should allocate a TTY for itself, also requires 'stdin' to be true. Default is false.
        /// </summary>
        [YamlMember(Alias = "tty")]
        [JsonProperty("tty", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Tty { get; set; }
    }
}

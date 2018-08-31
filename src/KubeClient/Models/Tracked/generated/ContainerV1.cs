using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     A single application container that you want to run within a pod.
    /// </summary>
    public partial class ContainerV1 : Models.ContainerV1, ITracked
    {
        /// <summary>
        ///     Entrypoint array. Not executed within a shell. The docker image's ENTRYPOINT is used if this is not provided. Variable references $(VAR_NAME) are expanded using the container's environment. If a variable cannot be resolved, the reference in the input string will be unchanged. The $(VAR_NAME) syntax can be escaped with a double $$, ie: $$(VAR_NAME). Escaped references will never be expanded, regardless of whether the variable exists or not. Cannot be updated. More info: https://kubernetes.io/docs/tasks/inject-data-application/define-command-argument-container/#running-a-command-in-a-shell
        /// </summary>
        [YamlMember(Alias = "command")]
        [JsonProperty("command", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> Command { get; set; } = new List<string>();

        /// <summary>
        ///     Docker image name. More info: https://kubernetes.io/docs/concepts/containers/images
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
        ///     Actions that the management system should take in response to container lifecycle events. Cannot be updated.
        /// </summary>
        [JsonProperty("lifecycle")]
        [YamlMember(Alias = "lifecycle")]
        public override Models.LifecycleV1 Lifecycle
        {
            get
            {
                return base.Lifecycle;
            }
            set
            {
                base.Lifecycle = value;

                __ModifiedProperties__.Add("Lifecycle");
            }
        }


        /// <summary>
        ///     Periodic probe of container liveness. Container will be restarted if the probe fails. Cannot be updated. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#container-probes
        /// </summary>
        [JsonProperty("livenessProbe")]
        [YamlMember(Alias = "livenessProbe")]
        public override Models.ProbeV1 LivenessProbe
        {
            get
            {
                return base.LivenessProbe;
            }
            set
            {
                base.LivenessProbe = value;

                __ModifiedProperties__.Add("LivenessProbe");
            }
        }


        /// <summary>
        ///     Name of the container specified as a DNS_LABEL. Each container in a pod must have a unique name (DNS_LABEL). Cannot be updated.
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
        ///     Periodic probe of container service readiness. Container will be removed from service endpoints if the probe fails. Cannot be updated. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#container-probes
        /// </summary>
        [JsonProperty("readinessProbe")]
        [YamlMember(Alias = "readinessProbe")]
        public override Models.ProbeV1 ReadinessProbe
        {
            get
            {
                return base.ReadinessProbe;
            }
            set
            {
                base.ReadinessProbe = value;

                __ModifiedProperties__.Add("ReadinessProbe");
            }
        }


        /// <summary>
        ///     Whether the container runtime should close the stdin channel after it has been opened by a single attach. When stdin is true the stdin stream will remain open across multiple attach sessions. If stdinOnce is set to true, stdin is opened on container start, is empty until the first client attaches to stdin, and then remains open and accepts data until the client disconnects, at which time stdin is closed and remains closed until the container is restarted. If this flag is false, a container processes that reads from stdin will never receive an EOF. Default is false
        /// </summary>
        [JsonProperty("stdinOnce")]
        [YamlMember(Alias = "stdinOnce")]
        public override bool StdinOnce
        {
            get
            {
                return base.StdinOnce;
            }
            set
            {
                base.StdinOnce = value;

                __ModifiedProperties__.Add("StdinOnce");
            }
        }


        /// <summary>
        ///     Optional: Path at which the file to which the container's termination message will be written is mounted into the container's filesystem. Message written is intended to be brief final status, such as an assertion failure message. Will be truncated by the node if greater than 4096 bytes. The total message length across all containers will be limited to 12kb. Defaults to /dev/termination-log. Cannot be updated.
        /// </summary>
        [JsonProperty("terminationMessagePath")]
        [YamlMember(Alias = "terminationMessagePath")]
        public override string TerminationMessagePath
        {
            get
            {
                return base.TerminationMessagePath;
            }
            set
            {
                base.TerminationMessagePath = value;

                __ModifiedProperties__.Add("TerminationMessagePath");
            }
        }


        /// <summary>
        ///     List of sources to populate environment variables in the container. The keys defined within a source must be a C_IDENTIFIER. All invalid keys will be reported as an event when the container is starting. When a key exists in multiple sources, the value associated with the last source will take precedence. Values defined by an Env with a duplicate key will take precedence. Cannot be updated.
        /// </summary>
        [YamlMember(Alias = "envFrom")]
        [JsonProperty("envFrom", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.EnvFromSourceV1> EnvFrom { get; set; } = new List<Models.EnvFromSourceV1>();

        /// <summary>
        ///     Whether this container should allocate a buffer for stdin in the container runtime. If this is not set, reads from stdin in the container will always result in EOF. Default is false.
        /// </summary>
        [JsonProperty("stdin")]
        [YamlMember(Alias = "stdin")]
        public override bool Stdin
        {
            get
            {
                return base.Stdin;
            }
            set
            {
                base.Stdin = value;

                __ModifiedProperties__.Add("Stdin");
            }
        }


        /// <summary>
        ///     Container's working directory. If not specified, the container runtime's default will be used, which might be configured in the container image. Cannot be updated.
        /// </summary>
        [JsonProperty("workingDir")]
        [YamlMember(Alias = "workingDir")]
        public override string WorkingDir
        {
            get
            {
                return base.WorkingDir;
            }
            set
            {
                base.WorkingDir = value;

                __ModifiedProperties__.Add("WorkingDir");
            }
        }


        /// <summary>
        ///     Arguments to the entrypoint. The docker image's CMD is used if this is not provided. Variable references $(VAR_NAME) are expanded using the container's environment. If a variable cannot be resolved, the reference in the input string will be unchanged. The $(VAR_NAME) syntax can be escaped with a double $$, ie: $$(VAR_NAME). Escaped references will never be expanded, regardless of whether the variable exists or not. Cannot be updated. More info: https://kubernetes.io/docs/tasks/inject-data-application/define-command-argument-container/#running-a-command-in-a-shell
        /// </summary>
        [YamlMember(Alias = "args")]
        [JsonProperty("args", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> Args { get; set; } = new List<string>();

        /// <summary>
        ///     List of ports to expose from the container. Exposing a port here gives the system additional information about the network connections a container uses, but is primarily informational. Not specifying a port here DOES NOT prevent that port from being exposed. Any port which is listening on the default "0.0.0.0" address inside a container will be accessible from the network. Cannot be updated.
        /// </summary>
        [YamlMember(Alias = "ports")]
        [MergeStrategy(Key = "containerPort")]
        [JsonProperty("ports", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.ContainerPortV1> Ports { get; set; } = new List<Models.ContainerPortV1>();

        /// <summary>
        ///     Compute Resources required by this container. Cannot be updated. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#resources
        /// </summary>
        [JsonProperty("resources")]
        [YamlMember(Alias = "resources")]
        public override Models.ResourceRequirementsV1 Resources
        {
            get
            {
                return base.Resources;
            }
            set
            {
                base.Resources = value;

                __ModifiedProperties__.Add("Resources");
            }
        }


        /// <summary>
        ///     Pod volumes to mount into the container's filesystem. Cannot be updated.
        /// </summary>
        [MergeStrategy(Key = "mountPath")]
        [YamlMember(Alias = "volumeMounts")]
        [JsonProperty("volumeMounts", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.VolumeMountV1> VolumeMounts { get; set; } = new List<Models.VolumeMountV1>();

        /// <summary>
        ///     Security options the pod should run with. More info: https://kubernetes.io/docs/concepts/policy/security-context/ More info: https://git.k8s.io/community/contributors/design-proposals/security_context.md
        /// </summary>
        [JsonProperty("securityContext")]
        [YamlMember(Alias = "securityContext")]
        public override Models.SecurityContextV1 SecurityContext
        {
            get
            {
                return base.SecurityContext;
            }
            set
            {
                base.SecurityContext = value;

                __ModifiedProperties__.Add("SecurityContext");
            }
        }


        /// <summary>
        ///     List of environment variables to set in the container. Cannot be updated.
        /// </summary>
        [YamlMember(Alias = "env")]
        [MergeStrategy(Key = "name")]
        [JsonProperty("env", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.EnvVarV1> Env { get; set; } = new List<Models.EnvVarV1>();

        /// <summary>
        ///     Image pull policy. One of Always, Never, IfNotPresent. Defaults to Always if :latest tag is specified, or IfNotPresent otherwise. Cannot be updated. More info: https://kubernetes.io/docs/concepts/containers/images#updating-images
        /// </summary>
        [JsonProperty("imagePullPolicy")]
        [YamlMember(Alias = "imagePullPolicy")]
        public override string ImagePullPolicy
        {
            get
            {
                return base.ImagePullPolicy;
            }
            set
            {
                base.ImagePullPolicy = value;

                __ModifiedProperties__.Add("ImagePullPolicy");
            }
        }


        /// <summary>
        ///     Indicate how the termination message should be populated. File will use the contents of terminationMessagePath to populate the container status message on both success and failure. FallbackToLogsOnError will use the last chunk of container log output if the termination message file is empty and the container exited with an error. The log output is limited to 2048 bytes or 80 lines, whichever is smaller. Defaults to File. Cannot be updated.
        /// </summary>
        [JsonProperty("terminationMessagePolicy")]
        [YamlMember(Alias = "terminationMessagePolicy")]
        public override string TerminationMessagePolicy
        {
            get
            {
                return base.TerminationMessagePolicy;
            }
            set
            {
                base.TerminationMessagePolicy = value;

                __ModifiedProperties__.Add("TerminationMessagePolicy");
            }
        }


        /// <summary>
        ///     Whether this container should allocate a TTY for itself, also requires 'stdin' to be true. Default is false.
        /// </summary>
        [JsonProperty("tty")]
        [YamlMember(Alias = "tty")]
        public override bool Tty
        {
            get
            {
                return base.Tty;
            }
            set
            {
                base.Tty = value;

                __ModifiedProperties__.Add("Tty");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     APIServiceSpec contains information for locating and communicating with a server. Only https is supported, though you are able to disable certificate verification.
    /// </summary>
    public partial class APIServiceSpecV1Beta1 : Models.APIServiceSpecV1Beta1, ITracked
    {
        /// <summary>
        ///     CABundle is a PEM encoded CA bundle which will be used to validate an API server's serving certificate.
        /// </summary>
        [JsonProperty("caBundle")]
        [YamlMember(Alias = "caBundle")]
        public override string CaBundle
        {
            get
            {
                return base.CaBundle;
            }
            set
            {
                base.CaBundle = value;

                __ModifiedProperties__.Add("CaBundle");
            }
        }


        /// <summary>
        ///     Service is a reference to the service for this API server.  It must communicate on port 443 If the Service is nil, that means the handling for the API groupversion is handled locally on this server. The call will simply delegate to the normal handler chain to be fulfilled.
        /// </summary>
        [JsonProperty("service")]
        [YamlMember(Alias = "service")]
        public override Models.ServiceReferenceV1Beta1 Service
        {
            get
            {
                return base.Service;
            }
            set
            {
                base.Service = value;

                __ModifiedProperties__.Add("Service");
            }
        }


        /// <summary>
        ///     GroupPriorityMininum is the priority this group should have at least. Higher priority means that the group is prefered by clients over lower priority ones. Note that other versions of this group might specify even higher GroupPriorityMininum values such that the whole group gets a higher priority. The primary sort is based on GroupPriorityMinimum, ordered highest number to lowest (20 before 10). The secondary sort is based on the alphabetical comparison of the name of the object.  (v1.bar before v1.foo) We'd recommend something like: *.k8s.io (except extensions) at 18000 and PaaSes (OpenShift, Deis) are recommended to be in the 2000s
        /// </summary>
        [JsonProperty("groupPriorityMinimum")]
        [YamlMember(Alias = "groupPriorityMinimum")]
        public override int GroupPriorityMinimum
        {
            get
            {
                return base.GroupPriorityMinimum;
            }
            set
            {
                base.GroupPriorityMinimum = value;

                __ModifiedProperties__.Add("GroupPriorityMinimum");
            }
        }


        /// <summary>
        ///     Version is the API version this server hosts.  For example, "v1"
        /// </summary>
        [JsonProperty("version")]
        [YamlMember(Alias = "version")]
        public override string Version
        {
            get
            {
                return base.Version;
            }
            set
            {
                base.Version = value;

                __ModifiedProperties__.Add("Version");
            }
        }


        /// <summary>
        ///     Group is the API group name this server hosts
        /// </summary>
        [JsonProperty("group")]
        [YamlMember(Alias = "group")]
        public override string Group
        {
            get
            {
                return base.Group;
            }
            set
            {
                base.Group = value;

                __ModifiedProperties__.Add("Group");
            }
        }


        /// <summary>
        ///     InsecureSkipTLSVerify disables TLS certificate verification when communicating with this server. This is strongly discouraged.  You should use the CABundle instead.
        /// </summary>
        [JsonProperty("insecureSkipTLSVerify")]
        [YamlMember(Alias = "insecureSkipTLSVerify")]
        public override bool InsecureSkipTLSVerify
        {
            get
            {
                return base.InsecureSkipTLSVerify;
            }
            set
            {
                base.InsecureSkipTLSVerify = value;

                __ModifiedProperties__.Add("InsecureSkipTLSVerify");
            }
        }


        /// <summary>
        ///     VersionPriority controls the ordering of this API version inside of its group.  Must be greater than zero. The primary sort is based on VersionPriority, ordered highest to lowest (20 before 10). The secondary sort is based on the alphabetical comparison of the name of the object.  (v1.bar before v1.foo) Since it's inside of a group, the number can be small, probably in the 10s.
        /// </summary>
        [JsonProperty("versionPriority")]
        [YamlMember(Alias = "versionPriority")]
        public override int VersionPriority
        {
            get
            {
                return base.VersionPriority;
            }
            set
            {
                base.VersionPriority = value;

                __ModifiedProperties__.Add("VersionPriority");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}

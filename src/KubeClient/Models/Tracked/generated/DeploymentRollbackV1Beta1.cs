using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     DeploymentRollback stores the information required to rollback a deployment.
    /// </summary>
    public partial class DeploymentRollbackV1Beta1 : Models.DeploymentRollbackV1Beta1, ITracked
    {
        /// <summary>
        ///     Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#types-kinds
        /// </summary>
        [JsonProperty("kind")]
        [YamlMember(Alias = "kind")]
        public override string Kind
        {
            get
            {
                return base.Kind;
            }
            set
            {
                base.Kind = value;

                __ModifiedProperties__.Add("Kind");
            }
        }


        /// <summary>
        ///     Required: This must match the Name of a deployment.
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
        ///     APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#resources
        /// </summary>
        [JsonProperty("apiVersion")]
        [YamlMember(Alias = "apiVersion")]
        public override string ApiVersion
        {
            get
            {
                return base.ApiVersion;
            }
            set
            {
                base.ApiVersion = value;

                __ModifiedProperties__.Add("ApiVersion");
            }
        }


        /// <summary>
        ///     The config of this deployment rollback.
        /// </summary>
        [JsonProperty("rollbackTo")]
        [YamlMember(Alias = "rollbackTo")]
        public override Models.RollbackConfigV1Beta1 RollbackTo
        {
            get
            {
                return base.RollbackTo;
            }
            set
            {
                base.RollbackTo = value;

                __ModifiedProperties__.Add("RollbackTo");
            }
        }


        /// <summary>
        ///     The annotations to be updated to a deployment
        /// </summary>
        [YamlMember(Alias = "updatedAnnotations")]
        [JsonProperty("updatedAnnotations", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, string> UpdatedAnnotations { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}

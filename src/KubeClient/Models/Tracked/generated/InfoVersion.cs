using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Info contains versioning information. how we'll want to distribute that information.
    /// </summary>
    public partial class InfoVersion : Models.InfoVersion, ITracked
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("buildDate")]
        [YamlMember(Alias = "buildDate")]
        public override string BuildDate
        {
            get
            {
                return base.BuildDate;
            }
            set
            {
                base.BuildDate = value;

                __ModifiedProperties__.Add("BuildDate");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("gitTreeState")]
        [YamlMember(Alias = "gitTreeState")]
        public override string GitTreeState
        {
            get
            {
                return base.GitTreeState;
            }
            set
            {
                base.GitTreeState = value;

                __ModifiedProperties__.Add("GitTreeState");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("platform")]
        [YamlMember(Alias = "platform")]
        public override string Platform
        {
            get
            {
                return base.Platform;
            }
            set
            {
                base.Platform = value;

                __ModifiedProperties__.Add("Platform");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("gitVersion")]
        [YamlMember(Alias = "gitVersion")]
        public override string GitVersion
        {
            get
            {
                return base.GitVersion;
            }
            set
            {
                base.GitVersion = value;

                __ModifiedProperties__.Add("GitVersion");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("goVersion")]
        [YamlMember(Alias = "goVersion")]
        public override string GoVersion
        {
            get
            {
                return base.GoVersion;
            }
            set
            {
                base.GoVersion = value;

                __ModifiedProperties__.Add("GoVersion");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("compiler")]
        [YamlMember(Alias = "compiler")]
        public override string Compiler
        {
            get
            {
                return base.Compiler;
            }
            set
            {
                base.Compiler = value;

                __ModifiedProperties__.Add("Compiler");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("major")]
        [YamlMember(Alias = "major")]
        public override string Major
        {
            get
            {
                return base.Major;
            }
            set
            {
                base.Major = value;

                __ModifiedProperties__.Add("Major");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("minor")]
        [YamlMember(Alias = "minor")]
        public override string Minor
        {
            get
            {
                return base.Minor;
            }
            set
            {
                base.Minor = value;

                __ModifiedProperties__.Add("Minor");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("gitCommit")]
        [YamlMember(Alias = "gitCommit")]
        public override string GitCommit
        {
            get
            {
                return base.GitCommit;
            }
            set
            {
                base.GitCommit = value;

                __ModifiedProperties__.Add("GitCommit");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ExpressionWarning is a warning information that targets a specific expression.
    /// </summary>
    public partial class ExpressionWarningV1
    {
        /// <summary>
        ///     The path to the field that refers the expression. For example, the reference to the expression of the first item of validations is "spec.validations[0].expression"
        /// </summary>
        [YamlMember(Alias = "fieldRef")]
        [JsonProperty("fieldRef", NullValueHandling = NullValueHandling.Include)]
        public string FieldRef { get; set; }

        /// <summary>
        ///     The content of type checking information in a human-readable form. Each line of the warning contains the type that the expression is checked against, followed by the type check error from the compiler.
        /// </summary>
        [YamlMember(Alias = "warning")]
        [JsonProperty("warning", NullValueHandling = NullValueHandling.Include)]
        public string Warning { get; set; }
    }
}

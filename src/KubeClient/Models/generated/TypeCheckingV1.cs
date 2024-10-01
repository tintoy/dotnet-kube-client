using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     TypeChecking contains results of type checking the expressions in the ValidatingAdmissionPolicy
    /// </summary>
    public partial class TypeCheckingV1
    {
        /// <summary>
        ///     The type checking warnings for each expression.
        /// </summary>
        [YamlMember(Alias = "expressionWarnings")]
        [JsonProperty("expressionWarnings", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<ExpressionWarningV1> ExpressionWarnings { get; } = new List<ExpressionWarningV1>();

        /// <summary>
        ///     Determine whether the <see cref="ExpressionWarnings"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeExpressionWarnings() => ExpressionWarnings.Count > 0;
    }
}

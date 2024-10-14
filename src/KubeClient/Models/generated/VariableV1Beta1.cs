using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Variable is the definition of a variable that is used for composition. A variable is defined as a named expression.
    /// </summary>
    public partial class VariableV1Beta1
    {
        /// <summary>
        ///     Name is the name of the variable. The name must be a valid CEL identifier and unique among all variables. The variable can be accessed in other expressions through `variables` For example, if name is "foo", the variable will be available as `variables.foo`
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     Expression is the expression that will be evaluated as the value of the variable. The CEL expression has access to the same identifiers as the CEL expressions in Validation.
        /// </summary>
        [YamlMember(Alias = "expression")]
        [JsonProperty("expression", NullValueHandling = NullValueHandling.Include)]
        public string Expression { get; set; }
    }
}

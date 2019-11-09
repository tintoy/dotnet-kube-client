using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     The model for the response returned by client-go credential plugins.
    /// </summary>
    [KubeObject("ExecCredential", "client.authentication.k8s.io/v1beta1")]
    public class ExecCredentialV1Beta1
        : KubeObjectV1
    {
        /// <summary>
        ///     Create a new <see cref="ExecCredentialV1Beta1"/>.
        /// </summary>
        public ExecCredentialV1Beta1()
        {
        }

        /// <summary>
        ///     The credential plugin's status.
        /// </summary>
        public Dictionary<string, object> Status { get; } = new Dictionary<string, object>();

        /// <summary>
        ///     Determine whether the credential status should be serialised.
        /// </summary>
        /// <returns>
        ///     <c>true</c>, if the credential status should be serialised; otherwise, <c>false</c>.
        /// </returns>
        public bool ShouldSerializeStatus() => Status.Count > 0;

        /// <summary>
        ///     Get the value of the "status/token" field (if present).
        /// </summary>
        /// <returns>
        ///     The token, or <c>null</c> if the field is not present.
        /// </returns>
        public string GetToken()
        {
            if (Status.TryGetValue("token", out object token))
                return token as string;

            return null;
        }
    }
}

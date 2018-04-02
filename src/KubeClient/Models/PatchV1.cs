using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Patch is provided to give a concrete name and type to the Kubernetes PATCH request body.
    /// </summary>
    [KubeObject("Patch", "v1")]
    public partial class PatchV1
    {
    }
}

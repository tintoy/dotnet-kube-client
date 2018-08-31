using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Patch is provided to give a concrete name and type to the Kubernetes PATCH request body.
    /// </summary>
    public partial class PatchV1 : Models.PatchV1
    {
    }
}

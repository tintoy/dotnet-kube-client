using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     InitializerConfiguration describes the configuration of initializers.
    /// </summary>
    [KubeObject("InitializerConfiguration", "admissionregistration.k8s.io/v1alpha1")]
    [KubeApi(KubeAction.List, "apis/admissionregistration.k8s.io/v1alpha1/initializerconfigurations")]
    [KubeApi(KubeAction.Create, "apis/admissionregistration.k8s.io/v1alpha1/initializerconfigurations")]
    [KubeApi(KubeAction.Get, "apis/admissionregistration.k8s.io/v1alpha1/initializerconfigurations/{name}")]
    [KubeApi(KubeAction.Patch, "apis/admissionregistration.k8s.io/v1alpha1/initializerconfigurations/{name}")]
    [KubeApi(KubeAction.Delete, "apis/admissionregistration.k8s.io/v1alpha1/initializerconfigurations/{name}")]
    [KubeApi(KubeAction.Update, "apis/admissionregistration.k8s.io/v1alpha1/initializerconfigurations/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/admissionregistration.k8s.io/v1alpha1/watch/initializerconfigurations")]
    [KubeApi(KubeAction.DeleteCollection, "apis/admissionregistration.k8s.io/v1alpha1/initializerconfigurations")]
    [KubeApi(KubeAction.Watch, "apis/admissionregistration.k8s.io/v1alpha1/watch/initializerconfigurations/{name}")]
    public partial class InitializerConfigurationV1Alpha1 : KubeResourceV1
    {
        /// <summary>
        ///     Initializers is a list of resources and their default initializers Order-sensitive. When merging multiple InitializerConfigurations, we sort the initializers from different InitializerConfigurations by the name of the InitializerConfigurations; the order of the initializers from the same InitializerConfiguration is preserved.
        /// </summary>
        [MergeStrategy(Key = "name")]
        [YamlMember(Alias = "initializers")]
        [JsonProperty("initializers", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<InitializerV1Alpha1> Initializers { get; } = new List<InitializerV1Alpha1>();

        /// <summary>
        ///     Determine whether the <see cref="Initializers"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeInitializers() => Initializers.Count > 0;
    }
}

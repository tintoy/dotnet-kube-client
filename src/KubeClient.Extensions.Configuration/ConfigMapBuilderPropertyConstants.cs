namespace KubeClient.Extensions.Configuration
{
    /// <summary>
    /// Constants for ConfigMap builder properties.
    /// </summary>
    static class ConfigMapBuilderPropertyConstants
    {
        private const string Prefix = "KubeClient_ConfigMap_";
            
        public const string Client = Prefix + "Client";
        public const string Name = Prefix + "Name";
        public const string Namespace = Prefix + "Namespace";
        public const string SectionName = Prefix + "SectionName";
        public const string Watch = Prefix + "Watch";
        public const string ThrowOnNotFound = Prefix + "ThrowOnNotFound";
    }
}
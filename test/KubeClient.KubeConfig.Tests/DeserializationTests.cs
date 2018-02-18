using System.IO;
using Xunit;

namespace KubeClient.KubeConfig.Tests
{
    using Models;

    /// <summary>
    ///     Tests for deserialisation of Kubernetes client configuration.
    /// </summary>
    public class DeserializationTests
    {
        /// <summary>
        ///     Verify that the configuration loader can parse valid configuration from YAML. 
        /// </summary>
        [InlineData("valid1")]
        [Theory(DisplayName = "Can load valid configuration ")]
        public void CanLoadValidConfig(string configName)
        {
            FileInfo configFile = new FileInfo(
                Path.Combine("Configurations", $"{configName}.yml")
            );

            Config kubeConfig = Config.Load(configFile);
            Assert.Equal("v1", kubeConfig.ApiVersion);
            Assert.Equal("Config", kubeConfig.Kind);
            Assert.Equal("docker-for-desktop", kubeConfig.CurrentContext);

            Assert.Equal(5, kubeConfig.Contexts.Count);
            Assert.Equal(5, kubeConfig.Clusters.Count);
            Assert.Equal(5, kubeConfig.Users.Count);
        }
    }
}

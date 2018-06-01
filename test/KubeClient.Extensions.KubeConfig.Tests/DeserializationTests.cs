using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace KubeClient.Extensions.KubeConfig.Tests
{
    using Models;
    using TestCommon;

    /// <summary>
    ///     Tests for deserialisation of Kubernetes client configuration.
    /// </summary>
    public class DeserializationTests
        : TestBase
    {
        /// <summary>
        ///     Create a new config-deserialisation test suite.
        /// </summary>
        /// <param name="testOutput">
        ///     Output for the current test.
        /// </param>
        public DeserializationTests(ITestOutputHelper testOutput)
            : base(testOutput)
        {
        }

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

            K8sConfig kubeConfig = K8sConfig.Load(configFile);
            Assert.Equal("v1", kubeConfig.ApiVersion);
            Assert.Equal("Config", kubeConfig.Kind);
            Assert.Equal("docker-for-desktop", kubeConfig.CurrentContextName);

            Assert.Equal(5, kubeConfig.Contexts.Count);
            Assert.Equal(5, kubeConfig.Clusters.Count);
            Assert.Equal(5, kubeConfig.UserIdentities.Count);
        }
    }
}

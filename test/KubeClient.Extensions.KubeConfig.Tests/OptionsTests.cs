using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace KubeClient.Extensions.KubeConfig.Tests
{
    using Models;
    using TestCommon;

    /// <summary>
    ///     Tests for population of KubeClientOptions from Kubernetes client configuration.
    /// </summary>
    public class OptionsTests
        : TestBase
    {
        /// <summary>
        ///     Create a new options-population test suite.
        /// </summary>
        /// <param name="testOutput">
        ///     Output for the current test.
        /// </param>
        public OptionsTests(ITestOutputHelper testOutput)
            : base(testOutput)
        {
        }

        /// <summary>
        ///     Verify that the configuration loader can parse valid configuration from YAML. 
        /// </summary>
        [InlineData("valid1", "rke")]
        [Theory(DisplayName = "Can resolve valid options from client configuration ")]
        public void CanLoadValidConfig(string configName, string kubeContextName)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddOptions();
            services.AddKubeClientOptionsFromKubeConfig(
                Path.Combine("Configurations", $"{configName}.yml"),
                kubeContextName
            );

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                IOptions<KubeClientOptions> options = serviceProvider.GetRequiredService<IOptions<KubeClientOptions>>();
                Assert.NotNull(options);
            }
        }
    }
}

using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Xunit.Abstractions;

using MSLoggerFactory = Microsoft.Extensions.Logging.LoggerFactory;

namespace KubeClient.Extensions.DataProtection.Tests
{
    using Models;
    using TestCommon;
    using TestCommon.Logging;
    using TestCommon.Mocks;

    /// <summary>
    ///     Tests for DataProtection key persistence using K8s secrets.
    /// </summary>
    public class KeyPersistenceTests
        : TestBase
    {
        /// <summary>
        ///     The name of the secret used to sure DataProtection keys.
        /// </summary>
        static readonly string TestSecretName = "test-secret";

        /// <summary>
        ///     The namespace of the secret used to sure DataProtection keys.
        /// </summary>
        static readonly string TestSecretNamespace = "test-namespace";

        /// <summary>
        ///     Create a new DataProtection key persistence test-suite.
        /// </summary>
        /// <param name="testOutput">
        ///     Output for the current test.
        /// </param>
        public KeyPersistenceTests(ITestOutputHelper testOutput)
            : base(testOutput)
        {
        }

        /// <summary>
        ///     Verify that the data protector (i.e. <see cref="IDataProtectionProvider"/>) can be resolved from the DI container.
        /// </summary>
        /// <remarks>
        ///     This results in the data-protection system loading and initialising our provider.
        /// </remarks>
        [Fact]
        public async Task Can_Create_Data_Protector()
        {
            using Subject<SecretV1> secretWatchSubject = new Subject<SecretV1>(); // Needed because the key store always watches for changes.

            ConcurrentDictionary<string, SecretV1> secrets = new ConcurrentDictionary<string, SecretV1>();

            await using MockKubeApi mockApi = MockKubeApi.Create(TestOutput, api =>
            {
                api.HandleResources<SecretV1, SecretListV1>(secrets, secretWatchSubject);
            });

            using ServiceProvider serviceProvider = BuildServiceProvider(mockApi);

            IDataProtectionProvider dataProtection = serviceProvider.GetRequiredService<IDataProtectionProvider>();
            IDataProtector dataProtector = dataProtection.CreateProtector(purpose: "Test");

            SecretV1 secretResource;
            string secretResourceKey = MockKubeApi.GetResourceKey(TestSecretName, TestSecretNamespace);

            Assert.True(
                secrets.TryGetValue(secretResourceKey, out secretResource)
            );
            Assert.NotNull(secretResource);
            Assert.Empty(secretResource.Data);
        }

        /// <summary>
        ///     Verify that the data protector can encrypt (and then decrypt) some data.
        /// </summary>
        [Fact]
        public async Task Can_RoundTrip_Data()
        {
            using Subject<SecretV1> secretWatchSubject = new Subject<SecretV1>(); // Needed because the key store always watches for changes.

            ConcurrentDictionary<string, SecretV1> secrets = new ConcurrentDictionary<string, SecretV1>();

            await using MockKubeApi mockApi = MockKubeApi.Create(TestOutput, api =>
            {
                api.HandleResources<SecretV1, SecretListV1>(secrets, secretWatchSubject);
            });

            using ServiceProvider serviceProvider = BuildServiceProvider(mockApi);

            IDataProtectionProvider dataProtection = serviceProvider.GetRequiredService<IDataProtectionProvider>();
            IDataProtector dataProtector = dataProtection.CreateProtector(purpose: "Test");

            SecretV1 secretResource;
            string secretResourceKey = MockKubeApi.GetResourceKey(TestSecretName, TestSecretNamespace);

            Assert.True(
                secrets.TryGetValue(secretResourceKey, out secretResource)
            );
            Assert.NotNull(secretResource);
            Assert.Empty(secretResource.Data);

            const string expectedPlainText = "PlainText";
            string protectedData = dataProtector.Protect(expectedPlainText);

            Assert.True(
                secrets.TryGetValue(secretResourceKey, out secretResource)
            );
            Assert.NotNull(secretResource);
            Assert.NotEmpty(secretResource.Data);

            string actualPlainText = dataProtector.Unprotect(protectedData);
            Assert.Equal(expectedPlainText, actualPlainText);
        }

        /// <summary>
        ///     Build a (client-side) service provider for use in tests.
        /// </summary>
        /// <param name="mockApi">
        ///     The mock Kubernetes API that the client will communicate.
        /// </param>
        /// <param name="configureDataProtection">
        ///     An optional delegate that can be used to customise the data-protection system.
        /// </param>
        /// <param name="configureServices">
        ///     An optional delegate that can be used to configure additional services for dependency-injection.
        /// </param>
        /// <returns>
        ///     The configured service provider.
        /// </returns>
        ServiceProvider BuildServiceProvider(MockKubeApi mockApi, Action<IDataProtectionBuilder> configureDataProtection = null, Action<IServiceCollection> configureServices = null)
        {
            if (mockApi == null)
                throw new ArgumentNullException(nameof(mockApi));

            IKubeApiClient testApiClient = KubeApiClient.CreateTestClient(
                mockApi.CreateClient(),
                new KubeClientOptions
                {
                    ApiEndPoint = mockApi.BaseAddress,
                    AuthStrategy = KubeAuthStrategy.None,
                    KubeNamespace = "default",
                    LoggerFactory = MSLoggerFactory.Create(logging => logging.AddTestOutput(TestOutput)),
                    LogPayloads = true,
                }
            );

            var services = new ServiceCollection();

            services.AddSingleton(testApiClient);

            IDataProtectionBuilder dataProtection = services.AddDataProtection()
                .AddKeyManagementOptions(keyManagement =>
                {
                    keyManagement.AutoGenerateKeys = true;
                })
                .PersistKeysToKubeSecret(testApiClient, TestSecretName, TestSecretNamespace);

            if (configureDataProtection != null)
                configureDataProtection(dataProtection);

            if (configureServices != null)
                configureServices(services);

            return services.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateOnBuild = true,
                ValidateScopes = true,
            });
        }
    }
}

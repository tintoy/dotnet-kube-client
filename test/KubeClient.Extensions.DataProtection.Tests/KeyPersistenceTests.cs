using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace KubeClient.Extensions.DataProtection.Tests
{
    using Mocks;
    using Models;
    using TestCommon;
    using TestCommon.Logging;

    public class KeyPersistenceTests
        : TestBase
    {
        static readonly string TestSecretName = "test-secret";

        public KeyPersistenceTests(ITestOutputHelper testOutput)
            : base(testOutput)
        {
        }

        [Fact]
        public async Task Can_Create_Data_Protector()
        {
            using Subject<SecretV1> secretWatchSource = new Subject<SecretV1>(); // Needed because the key store always watches for changes.

            SecretV1 secretResource = null;

            await using MockKubeApi mockApi = MockKubeApi.Create(TestOutput, api =>
            {
                const string secretUri = "api/v1/namespaces/default/secrets/test-secret";
                const string secretsUri = "api/v1/namespaces/default/secrets";
                const string secretWatchUri = "api/v1/watch/namespaces/default/secrets/test-secret";

                api.HandleResourceGet(secretUri, () => secretResource);
                api.HandleResourceCreate(secretsUri, (SecretV1 initialSecretData) =>
                {
                    secretResource = initialSecretData;

                    return secretResource;
                });
                api.HandleResourceWatch(secretWatchUri, secretWatchSource);
            });
            
            using ServiceProvider serviceProvider = BuildServiceProvider(mockApi);

            IDataProtectionProvider dataProtection = serviceProvider.GetRequiredService<IDataProtectionProvider>();
            IDataProtector dataProtector = dataProtection.CreateProtector(purpose: "Test");

            Assert.NotNull(secretResource);
            Assert.Empty(secretResource.Data);
        }

        [Fact]
        public async Task Can_Protect_Data()
        {
            using Subject<SecretV1> secretWatchSource = new Subject<SecretV1>(); // Needed because the key store always watches for changes.

            SecretV1 secretResource = null;

            await using MockKubeApi mockApi = MockKubeApi.Create(TestOutput, api =>
            {
                const string secretUri = "api/v1/namespaces/default/secrets/test-secret";
                const string secretsUri = "api/v1/namespaces/default/secrets";
                const string secretWatchUri = "api/v1/watch/namespaces/default/secrets/test-secret";

                api.HandleResourceGet(secretUri, () => secretResource);
                api.HandleResourceCreate(secretsUri, (SecretV1 initialSecretData) =>
                {
                    secretResource = initialSecretData;

                    return secretResource;
                });
                api.HandleResourcePatchAsync(secretUri, (JArray patchRequest) =>
                {
                    Log.LogInformation("Patch request: {PatchRequest}", patchRequest.ToString(Newtonsoft.Json.Formatting.None));

                    secretResource = secretResource.ApplyJsonPatch(patchRequest);

                    return Task.FromResult(secretResource);
                });
                api.HandleResourceWatch(secretWatchUri, secretWatchSource);
            });

            using ServiceProvider serviceProvider = BuildServiceProvider(mockApi);

            IDataProtectionProvider dataProtection = serviceProvider.GetRequiredService<IDataProtectionProvider>();
            IDataProtector dataProtector = dataProtection.CreateProtector(purpose: "Test");

            Assert.NotNull(secretResource);
            Assert.Empty(secretResource.Data);

            string protectedData = dataProtector.Protect("Plain Text");
            Assert.NotNull(protectedData);

            Assert.NotEmpty(secretResource.Data);
        }

        [Fact]
        public async Task Can_RoundTrip_Data()
        {
            using Subject<SecretV1> secretWatchSource = new Subject<SecretV1>(); // Needed because the key store always watches for changes.

            SecretV1 secretResource = null;

            await using MockKubeApi mockApi = MockKubeApi.Create(TestOutput, api =>
            {
                const string secretUri = "api/v1/namespaces/default/secrets/test-secret";
                const string secretsUri = "api/v1/namespaces/default/secrets";
                const string secretWatchUri = "api/v1/watch/namespaces/default/secrets/test-secret";

                api.HandleResourceGet(secretUri, () => secretResource);
                api.HandleResourceCreate(secretsUri, (SecretV1 initialSecretData) =>
                {
                    secretResource = initialSecretData;

                    return secretResource;
                });
                api.HandleResourcePatchAsync(secretUri, (JArray patchRequest) =>
                {
                    Log.LogInformation("Patch request: {PatchRequest}", patchRequest.ToString(Newtonsoft.Json.Formatting.None));

                    secretResource = secretResource.ApplyJsonPatch(patchRequest);

                    return Task.FromResult(secretResource);
                });
                api.HandleResourceWatch(secretWatchUri, secretWatchSource);
            });

            using ServiceProvider serviceProvider = BuildServiceProvider(mockApi);

            IDataProtectionProvider dataProtection = serviceProvider.GetRequiredService<IDataProtectionProvider>();
            IDataProtector dataProtector = dataProtection.CreateProtector(purpose: "Test");

            Assert.NotNull(secretResource);
            Assert.Empty(secretResource.Data);

            const string expectedPlainText = "PlainText";
            string protectedData = dataProtector.Protect(expectedPlainText);

            Assert.NotEmpty(secretResource.Data);

            string actualPlainText = dataProtector.Unprotect(protectedData);
            Assert.Equal(expectedPlainText, actualPlainText);
        }

        ServiceProvider BuildServiceProvider(MockKubeApi mockApi, Action<IDataProtectionBuilder> configureDataProtection = null, Action<IServiceCollection> configureServices = null)
        {
            if (mockApi == null)
                throw new ArgumentNullException(nameof(mockApi));

            IKubeApiClient testApiClient = KubeApiClient.Create(
                mockApi.CreateClient(),
                new KubeClientOptions
                {
                    ApiEndPoint = mockApi.BaseAddress,
                    AuthStrategy = KubeAuthStrategy.None,
                    KubeNamespace = "default",
                    LoggerFactory = new LoggerFactory().AddTestOutput(TestOutput),
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
                .PersistKeysToKubeSecret(testApiClient, TestSecretName);

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

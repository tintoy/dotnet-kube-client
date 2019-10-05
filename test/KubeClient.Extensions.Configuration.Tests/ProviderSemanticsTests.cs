using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Xunit;

namespace KubeClient.Extensions.Configuration.Tests
{
    /// <summary>
    ///     Tests to validate the semantics of <see cref="ConfigurationBuilder"/>, <see cref="IConfigurationSource"/>, and <see cref="IConfigurationProvider"/>.
    /// </summary>
    public class ProviderSemanticsTests
    {
        /// <summary>
        /// An <see cref="Action"/> delegate used to trigger reload of configuration.
        /// </summary>
        static Action TriggerReload;

        /// <summary>
        ///     A second <see cref="IConfigurationSource"/> will override values provided by the first source.
        /// </summary>
        [Fact]
        public void Can_Override_Configuration()
        {
            var source1 = new DummyConfigSource
            {
                ProviderData =
                    {
                        ["Key1"] = "Value1",
                        ["Key2"] = "Value2",
                    }
            };

            var source2 = new DummyConfigSource
            {
                ProviderData =
                    {
                        ["Key1"] = "Value1a",
                        ["Key3"] = "Value3",
                    }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .Add(source1)
                .Add(source2)
                .Build();

            Assert.Equal("Value2", configuration["Key2"]);
            Assert.Equal("Value3", configuration["Key3"]);

            Assert.Equal("Value1a", configuration["Key1"]);

            source2.ProviderData["Key1"] = "Value1b";
            TriggerReload();

            Assert.Equal("Value2", configuration["Key2"]);
            Assert.Equal("Value3", configuration["Key3"]);

            Assert.Equal("Value1a", configuration["Key1"]);
        }

        /// <summary>
        ///     A dummy configuration source that gets its data from a dictionary.
        /// </summary>
        class DummyConfigSource
            : IConfigurationSource
        {
            /// <summary>
            ///     Create a new <see cref="DummyConfigSource"/>.
            /// </summary>
            public DummyConfigSource()
            {
            }

            /// <summary>
            ///     The provider data.
            /// </summary>
            public Dictionary<string, string> ProviderData { get; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            /// <summary>
            ///     Build a configuration provider for the source's configured values.
            /// </summary>
            /// <param name="builder">
            ///     The <see cref="IConfigurationBuilder"/> for which configuration is being built.
            /// </param>
            /// <returns>
            ///     The configuration provider.
            /// </returns>
            public IConfigurationProvider Build(IConfigurationBuilder builder) => new DummyConfigProvider(ProviderData);
        }

        /// <summary>
        ///     A dummy configuration provider that gets its data from a dictionary.
        /// </summary>
        class DummyConfigProvider
            : ConfigurationProvider
        {
            /// <summary>
            ///     Create a new <see cref="DummyConfigProvider"/>.
            /// </summary>
            /// <param name="providerData">
            ///     A <see cref="Dictionary{TKey, TValue}"/> containing the provider data.
            /// </param>
            public DummyConfigProvider(Dictionary<string, string> providerData)
            {
                if ( providerData == null )
                    throw new ArgumentNullException(nameof(providerData));

                ProviderData = providerData;

                // Hacky mechanism to register for reload.
                Action oldReload = TriggerReload;
                TriggerReload = () =>
                {
                    if (oldReload != null)
                        oldReload();

                    OnReload();
                };
            }

            /// <summary>
            ///     A <see cref="Dictionary{TKey, TValue}"/> containing the provider data.
            /// </summary>
            public Dictionary<string, string> ProviderData { get; }

            /// <summary>
            ///     Load the provider data's.
            /// </summary>
            public override void Load()
            {
                base.Load();

                Data = new Dictionary<string, string>(ProviderData, StringComparer.OrdinalIgnoreCase);
            }
        }
    }
}

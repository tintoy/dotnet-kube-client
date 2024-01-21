using Microsoft.Extensions.Configuration;
using System.Collections.Immutable;
using Xunit;

namespace KubeClient.Extensions.Configuration.Tests
{
    using Utilities;

    /// <summary>
    ///     Tests for <see cref="KeyPathHelper"/>.
    /// </summary>
    public class KeyPathHelperTests
    {
        /// <summary>
        ///     Verify that <see cref="KeyPathHelper.ToConfigurationPath"/> can convert a K8s ConfigMap/Secret key path to an <see cref="IConfiguration"/> key path.
        /// </summary>
        /// <param name="keyPath">
        ///     The K8s ConfigMap/Secret key path.
        /// </param>
        /// <param name="keyPathDelimiters">
        ///     A string representing the delimiters used in the K8s ConfigMap/Secret key path.
        /// </param>
        /// <param name="expectedConfigPath">
        ///     The expected <see cref="IConfiguration"/> key path.
        /// </param>
        [Theory]
        [InlineData("foo.bar.baz", "", "foo.bar.baz")]
        [InlineData("foo.bar.baz", ".", "foo:bar:baz")]
        [InlineData("foo.bar_baz", ".", "foo:bar_baz")]
        [InlineData("foo.bar_baz", "._", "foo:bar:baz")]
        public void Can_Convert_KeyPath_To_ConfigPath(string keyPath, string keyPathDelimiters, string expectedConfigPath)
        {
            ImmutableHashSet<char> delimiters = ImmutableHashSet.CreateRange(keyPathDelimiters);

            string actualConfigPath = KeyPathHelper.ToConfigurationPath(keyPath, delimiters);
            Assert.Equal(expectedConfigPath, actualConfigPath);
        }
    }
}

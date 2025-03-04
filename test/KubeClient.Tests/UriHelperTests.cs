using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KubeClient.Tests
{
    using Utilities;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="UriHelper"/>.
    /// </summary>
    public class UriHelperTests
    {
        static readonly Uri BaseUri = new Uri("https://localhost");

        [Theory]
        [InlineData("/")]
        [InlineData("/?param1=value1&param2=value2")]
        [InlineData("path1")]
        [InlineData("path1?param1=value1&param2=value2")]
        [InlineData("path1/path2")]
        [InlineData("path1/path2?param1=value1&param2=value2")]
        [InlineData("path1/path2/")]
        [InlineData("path1/path2/?param1=value1&param2=value2")]
        [InlineData("/path1")]
        [InlineData("/path1?param1=value1&param2=value2")]
        [InlineData("/path1/path2")]
        [InlineData("/path1/path2?param1=value1&param2=value2")]
        [InlineData("/path1/path2/")]
        [InlineData("/path1/path2/?param1=value1&param2=value2")]
        public void Can_SafeGetPathAndQuery_RelativeUri(string input)
        {
            Uri uri = new Uri(input, UriKind.RelativeOrAbsolute);
            Assert.False(uri.IsAbsoluteUri);

            string pathAndQuery = uri.SafeGetPathAndQuery();
            Assert.Equal(pathAndQuery,
                NormalizePath(input)
            );
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/?param1=value1&param2=value2")]
        [InlineData("path1")]
        [InlineData("path1?param1=value1&param2=value2")]
        [InlineData("path1/path2")]
        [InlineData("path1/path2?param1=value1&param2=value2")]
        [InlineData("path1/path2/")]
        [InlineData("path1/path2/?param1=value1&param2=value2")]
        [InlineData("/path1")]
        [InlineData("/path1?param1=value1&param2=value2")]
        [InlineData("/path1/path2")]
        [InlineData("/path1/path2?param1=value1&param2=value2")]
        [InlineData("/path1/path2/")]
        [InlineData("/path1/path2/?param1=value1&param2=value2")]
        public void Can_SafeGetPathAndQuery_AbsoluteUri(string input)
        {
            Uri relativeUri = new Uri(input, UriKind.RelativeOrAbsolute);
            Assert.False(relativeUri.IsAbsoluteUri);

            Uri uri = new Uri(BaseUri, relativeUri);

            string pathAndQuery = uri.SafeGetPathAndQuery();
            Assert.Equal(pathAndQuery,
                NormalizePath(input)
            );
        }

        /// <summary>
        ///     Normalise the specified path and query for comparisons in tests.
        /// </summary>
        /// <param name="pathAndQuery">
        ///     The URI path and query components.
        /// </param>
        /// <returns>
        ///     The normalised path and query.
        /// </returns>
        /// <remarks>
        ///     System.Uri treats the path component of an absolute URI as an absolute path.
        /// </remarks>
        static string NormalizePath(string pathAndQuery)
        {
            if (pathAndQuery == null)
                throw new ArgumentNullException(nameof(pathAndQuery));

            if (pathAndQuery.StartsWith("/"))
                return pathAndQuery;

            return $"/{pathAndQuery}";
        }
    }
}

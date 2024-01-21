using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace KubeClient.Extensions.Configuration.Utilities
{
    /// <summary>
    ///     Helper methods for working with key paths in ConfigMaps and Secrets.
    /// </summary>
    public static class KeyPathHelper
    {
        /// <summary>
        ///     The delimiter for path segments in <see cref="IConfiguration"/>
        /// </summary>
        public static readonly char ConfigurationPathDelimiter = ':';

        /// <summary>
        ///     Convert a Kubernetes ConfigMap/Secret key path to an <see cref="IConfiguration"/> path (e.g. "foo.bar.baz" -> "foo:bar:baz").
        /// </summary>
        /// <param name="keyPath">
        ///     The key path (e.g. "foo.bar.baz").
        /// </param>
        /// <param name="delimiters">
        ///     The characters (if any) that represent key-path delimiters.
        /// </param>
        /// <returns>
        ///     The <see cref="IConfiguration"/> (e.g. "foo:bar:baz").
        /// </returns>
        public static string ToConfigurationPath(string keyPath, IImmutableSet<char> delimiters)
        {
            if (String.IsNullOrEmpty(keyPath))
                throw new ArgumentException($"Argument cannot be null or empty: {nameof(keyPath)}.", nameof(keyPath));

            if (delimiters == null)
                throw new ArgumentNullException(nameof(delimiters));

            if (delimiters.Count == 0)
                return keyPath;

            bool modified = false;

            char[] buffer = keyPath.ToCharArray();
            for (int bufferIndex = 0; bufferIndex < buffer.Length; bufferIndex++)
            { 
                char current = buffer[bufferIndex];
                if (delimiters.Contains(current))
                {
                    buffer[bufferIndex] = ConfigurationPathDelimiter;
                    
                    modified = true;
                }
            }

            if (modified)
                return new string(buffer);

            return keyPath;
        }
    }
}

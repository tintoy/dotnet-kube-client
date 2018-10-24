using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using KubeClient.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KubeClient.MessageHandlers
{
    /// <summary>
    ///     HTTP message handler that runs a command to obtain a bearer token and adds it to outgoing requests.
    /// </summary>
    public class CommandBearerTokenHandler
        : BearerTokenHandler
    {
        /// <summary>
        ///     An object used to synchronise access to handler state.
        /// </summary>
        readonly object _stateLock = new object();

        /// <summary>
        ///     The command to execute in order to obtain the access token for outgoing requests.
        /// </summary>
        readonly string _accessTokenCommand;

        /// <summary>
        ///     The arguments (if any) for the access-token command.
        /// </summary>
        readonly string _accessTokenCommandArguments;

        /// <summary>
        ///     The JPath-style selector used to retrieve the access token from the command output.
        /// </summary>
        readonly string _accessTokenSelector;

        /// <summary>
        ///     The JPath-style selector used to retrieve the access token's expiry date/time from the command output.
        /// </summary>
        readonly string _accessTokenExpirySelector;

        /// <summary>
        ///     The current access token (if any).
        /// </summary>
        string _accessToken;

        /// <summary>
        ///     The UTC date/time that the access token expires.
        /// </summary>
        DateTime? _accessTokenExpiresUtc;

        /// <summary>
        ///     Create a new <see cref="CommandBearerTokenHandler"/>.
        /// </summary>
        /// <param name="accessTokenCommand">
        ///     The command to execute in order to obtain the access token for outgoing requests.
        /// </param>
        /// <param name="accessTokenCommandArguments">
        ///     The arguments (if any) for the access-token command.
        /// </param>
        /// <param name="accessTokenSelector">
        ///     The Go-style selector used to retrieve the access token from the command output.
        /// </param>
        /// <param name="accessTokenExpirySelector">
        ///     The Go-style selector used to retrieve the access token's expiry date/time from the command output.
        /// </param>
        /// <param name="initialAccessToken">
        ///     The initial access token (if any) to use for authentication.
        /// </param>
        /// <param name="initialTokenExpiryUtc">
        ///     The UTC date / time the the initial access token (if any) expires.
        /// </param>
        public CommandBearerTokenHandler(string accessTokenCommand, string accessTokenCommandArguments, string accessTokenSelector, string accessTokenExpirySelector, string initialAccessToken = null, DateTime? initialTokenExpiryUtc = null)
        {
            if (String.IsNullOrWhiteSpace(accessTokenCommand))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'accessTokenCommand'.", nameof(accessTokenCommand));

            if (String.IsNullOrWhiteSpace(accessTokenSelector))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'accessTokenSelector'.", nameof(accessTokenSelector));
            
            if (String.IsNullOrWhiteSpace(accessTokenExpirySelector))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'accessTokenExpirySelector'.", nameof(accessTokenExpirySelector));
            
            _accessTokenCommand = accessTokenCommand;
            _accessTokenCommandArguments = accessTokenCommandArguments ?? String.Empty;
            _accessTokenSelector = JPathFromGoSelector(accessTokenSelector);
            _accessTokenExpirySelector = JPathFromGoSelector(accessTokenExpirySelector);

            if (!String.IsNullOrWhiteSpace(initialAccessToken))
                _accessToken = initialAccessToken;

            _accessTokenExpiresUtc = initialTokenExpiryUtc;
        }

        /// <summary>
        ///     Obtain a bearer token to use for authentication.
        /// </summary>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     The access token.
        /// </returns>
        protected override async Task<string> GetTokenAsync(CancellationToken cancellationToken)
        {
            string accessToken;
            DateTime? accessTokenExpiresUtc;

            // Capture snapshot of access token / expiry.
            lock (_stateLock)
            {
                accessToken = _accessToken;
                accessTokenExpiresUtc = _accessTokenExpiresUtc;
            }

            if (!String.IsNullOrWhiteSpace(accessToken) && accessTokenExpiresUtc > DateTime.UtcNow)
                return accessToken;

            cancellationToken.ThrowIfCancellationRequested();

            ProcessStartInfo accessTokenCommandInfo = new ProcessStartInfo(_accessTokenCommand, _accessTokenCommandArguments)
            {
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
            };

            using (Process accessTokenCommand = Process.Start(accessTokenCommandInfo))
            {
                int exitCode = await accessTokenCommand.WaitForExitAsync(cancellationToken, killIfCancelled: true);
                if (exitCode != 0)
                {
                    // We omit the command's STDOUT / STDERR from this exception message because they may contain sensitive information!

                    throw new KubeClientException(
                        $"Failed to execute access-token command '{_accessTokenCommand} {_accessTokenCommandArguments}' (process exited with code {exitCode})."
                    );
                }

                string standardOutput = await accessTokenCommand.StandardOutput.ReadToEndAsync().ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();

                // Ensure command output is JSON
                JObject outputJson;
                try
                {
                    outputJson = JObject.Parse(standardOutput);
                }
                catch (JsonReaderException invalidJson)
                {
                    throw new KubeClientException($"Failed to parse output of access-token command '{_accessTokenCommand} {_accessTokenCommandArguments}' (not valid JSON).",
                        innerException: invalidJson
                    );
                }

                accessToken = outputJson.SelectToken(_accessTokenSelector)?.Value<string>();
                if (accessToken == null)
                {
                    throw new KubeClientException(
                        $"Failed to find access-token in output of command '{_accessTokenCommand} {_accessTokenCommandArguments}' using JPath selector '{_accessTokenSelector}'."
                            + Environment.NewLine
                            + standardOutput
                    );
                }

                string accessTokenExpiresUtcValue = outputJson.SelectToken(_accessTokenExpirySelector)?.Value<string>();
                if (accessTokenExpiresUtcValue == null)
                {
                    throw new KubeClientException(
                        $"Failed to find access-token lifetime in output of command '{_accessTokenCommand} {_accessTokenCommandArguments}' using JPath selector '{_accessTokenExpirySelector}'."
                            + Environment.NewLine
                            + standardOutput
                    );
                }

                accessTokenExpiresUtc = ConvertAccessTokenExpiresUtc(accessTokenExpiresUtcValue);

                // OK, both access token and expiry are good; update atomically.
                lock (_stateLock)
                {
                    _accessToken = accessToken;
                    _accessTokenExpiresUtc = accessTokenExpiresUtc;
                }

                return accessToken;
            }
        }

        private static DateTime ConvertAccessTokenExpiresUtc(string accessTokenExpiresUtcValue)
        {
            return DateTime.Parse(accessTokenExpiresUtcValue,
                provider: CultureInfo.InvariantCulture,
                styles: DateTimeStyles.AssumeUniversal
            );
        }

        /// <summary>
        ///     Convert a Go-style selector to a JPath-style selector.
        /// </summary>
        /// <param name="goSelector">
        ///     The Go-style selector (e.g. "{.foo.bar}").
        /// </param>
        /// <returns>
        ///     The JPath-style selector (e.g. "$.foo.bar").
        /// </returns>
        static string JPathFromGoSelector(string goSelector)
        {
            if (String.IsNullOrWhiteSpace(goSelector))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'goSelector'.", nameof(goSelector));

            string jpathSelector = goSelector;
            
            if (jpathSelector[0] == '{' && jpathSelector[jpathSelector.Length - 1] == '}')
                jpathSelector = jpathSelector.Substring(1, jpathSelector.Length - 2);
            
            if (jpathSelector[0] == '.')
                jpathSelector = '$' + jpathSelector;

            return jpathSelector;
        }
    }
}
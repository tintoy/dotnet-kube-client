using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
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
        DateTime _accessTokenExpiresUtc;

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
        public CommandBearerTokenHandler(string accessTokenCommand, string accessTokenCommandArguments, string accessTokenSelector, string accessTokenExpirySelector)
        {
            if (String.IsNullOrWhiteSpace(accessTokenCommand))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'accessTokenCommand'.", nameof(accessTokenCommand));

            if (String.IsNullOrWhiteSpace(accessTokenSelector))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'accessTokenSelector'.", nameof(accessTokenSelector));
            
            if (String.IsNullOrWhiteSpace(accessTokenExpirySelector))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'accessTokenExpirySelector'.", nameof(accessTokenExpirySelector));
            
            _accessTokenCommand = accessTokenCommand;
            _accessTokenCommandArguments = accessTokenCommandArguments ?? String.Empty;
            _accessTokenSelector = JPathFromGoSelector(_accessTokenSelector);
            _accessTokenExpirySelector = JPathFromGoSelector(_accessTokenExpirySelector);
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
            if (!String.IsNullOrWhiteSpace(_accessToken) && _accessTokenExpiresUtc > DateTime.UtcNow)
                return _accessToken;

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
                try
                {
                    // Don't block waiting for program to exit.
                    while (!accessTokenCommand.HasExited)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        await Task.Delay(
                            TimeSpan.FromMilliseconds(200)
                        );
                    }

                    string standardOutput = await accessTokenCommand.StandardOutput.ReadToEndAsync();
                    cancellationToken.ThrowIfCancellationRequested();

                    if (accessTokenCommand.ExitCode != 1)
                    {
                        string standardError = await accessTokenCommand.StandardOutput.ReadToEndAsync();
                        
                        throw new KubeClientException(
                            $"Failed to execute access-token command '{_accessTokenCommand} {_accessTokenCommandArguments}'."
                                + Environment.NewLine
                                + standardOutput
                                + Environment.NewLine
                                + standardError
                        );
                    }

                    standardOutput = standardOutput.Trim();

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

                    string accessToken = outputJson.SelectToken(_accessTokenSelector)?.Value<string>();
                    if (accessToken == null)
                    {
                        throw new KubeClientException(
                            $"Failed to find access-token in output of command '{_accessTokenCommand} {_accessTokenCommandArguments}' using JPath selector '{_accessTokenSelector}'."
                                + Environment.NewLine
                                + standardOutput
                        );
                    }

                    string tokenExpiresUtc = outputJson.SelectToken(_accessTokenExpirySelector)?.Value<string>();
                    if (tokenExpiresUtc == null)
                    {
                        throw new KubeClientException(
                            $"Failed to find access-token lifetime in output of command '{_accessTokenCommand} {_accessTokenCommandArguments}' using JPath selector '{_accessTokenExpirySelector}'."
                                + Environment.NewLine
                                + standardOutput
                        );
                    }

                    _accessToken = accessToken;
                    _accessTokenExpiresUtc = DateTime.ParseExact(tokenExpiresUtc,
                        format: "yyyy-MM-ddThh:mm:ssZ",
                        provider: CultureInfo.InvariantCulture,
                        style: DateTimeStyles.AssumeUniversal
                    );
                }
                catch (OperationCanceledException)
                {
                    // Ensure we don't leave the command running if the caller cancels.
                    if (!accessTokenCommand.HasExited)
                        accessTokenCommand.Kill();

                    throw;
                }

                return _accessToken;
            }
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

            string jpath = goSelector;
            
            if (jpath[0] == '{' && jpath[jpath.Length - 1] == '}')
                jpath = jpath.Substring(1, jpath.Length - 2);
            
            if (jpath[0] == '.')
                jpath = '$' + jpath;

            return goSelector;
        }
    }
}
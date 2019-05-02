    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.IO;
    using System.Net.WebSockets;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit.Abstractions;
    using Xunit;

    namespace KubeClient.Extensions.WebSockets.Tests
    {
        using Logging;
        using Server;

        /// <summary>
        ///     The base class for Kubernetes WebSocket test suites.
        /// </summary>
        public abstract class WebSocketTestBase
            : TestBase
        {
            /// <summary>
            ///     The next server port to use.
            /// </summary>
            static int NextPort = 13255;

            /// <summary>
            ///     Create a new <see cref="WebSocketTestBase"/>.
            /// </summary>
            /// <param name="testOutput">
            ///     Output for the current test.
            /// </param>
            protected WebSocketTestBase(ITestOutputHelper testOutput)
                : base(testOutput)
            {
                int port = Interlocked.Increment(ref NextPort);

                BaseAddress = new Uri($"http://localhost:{port}");
                WebSocketBaseAddress = new Uri($"ws://localhost:{port}");

                Host = WebHost.CreateDefaultBuilder()
                    .UseStartup<Startup>()
                    .ConfigureServices(ConfigureTestServerServices)
                    .ConfigureLogging(ConfigureTestServerLogging)
                    .UseUrls(BaseAddress.AbsoluteUri)
                    .Build();

                Disposal.Add(Host);
            }

            /// <summary>
            ///     The test server's base address (http://).
            /// </summary>
            protected Uri BaseAddress { get; }

            /// <summary>
            ///     The test server's base WebSockets address (ws://).
            /// </summary>
            protected Uri WebSocketBaseAddress { get; }

            /// <summary>
            ///     The test server's web host.
            /// </summary>
            protected IWebHost Host { get; }

            /// <summary>
            ///     Test adapter for accepting web sockets.
            /// </summary>
            protected WebSocketTestAdapter WebSocketTestAdapter { get; } = new WebSocketTestAdapter();

            /// <summary>
            ///     Create a <see cref="KubeApiClient"/> that issues requests against the test server.
            /// </summary>
            /// <returns>
            ///     The configured <see cref="KubeApiClient"/>.
            /// </returns>
            protected KubeApiClient CreateTestClient()
            {
                return KubeApiClient.Create(new KubeClientOptions
                {
                    ApiEndPoint = BaseAddress,
                    KubeNamespace = "default",
                    LoggerFactory = LoggerFactory

                    // No authentication.
                });
            }

            /// <summary>
            ///     Configure services for the test server.
            /// </summary>
            /// <param name="services">
            ///     The service collection to configure.
            /// </param>
            protected virtual void ConfigureTestServerServices(IServiceCollection services)
            {
                if (services == null)
                    throw new ArgumentNullException(nameof(services));
                
                // Inject WebSocketTestData.
                services.AddSingleton(WebSocketTestAdapter);
            }

            /// <summary>
            ///     Configure logging for the test server.
            /// </summary>
            /// <param name="services">
            ///     The logger factory to configure.
            /// </param>
            protected virtual void ConfigureTestServerLogging(ILoggingBuilder logging)
            {
                if (logging == null)
                    throw new ArgumentNullException(nameof(logging));
                
                logging.AddTestOutput(TestOutput, MinLogLevel);
            }

            /// <summary>
            ///     Asynchronously disconnect client and server WebSockets using the standard handshake.
            /// </summary>
            /// <param name="clientSocket">
            ///     The client-side <see cref="WebSocket"/>.
            /// </param>
            /// <param name="serverSocket">
            ///     The server-side <see cref="WebSocket"/>.
            /// </param>
            /// <param name="closeStatus">
            ///     An optional <see cref="WebSocketCloseStatus"/> value indicating the reason for disconnection.
            ///
            ///     Defaults to <see cref="WebSocketCloseStatus.NormalClosure"/>.
            /// </param>
            /// <param name="closeStatusDescription">
            ///     An optional textual description of the reason for disconnection.
            ///
            ///     Defaults to "Normal Closure".
            /// </param>
            /// <returns>
            ///     A <see cref="Task"/> representing the asynchronous operation.
            /// </returns>
            protected async Task Disconnect(WebSocket clientSocket, WebSocket serverSocket, WebSocketCloseStatus closeStatus = WebSocketCloseStatus.NormalClosure, string closeStatusDescription = "Normal Closure")
            {
                if (clientSocket == null)
                    throw new ArgumentNullException(nameof(clientSocket));

                if (serverSocket == null)
                    throw new ArgumentNullException(nameof(serverSocket));

                Log.LogInformation("Disconnecting...");

                // Asynchronously perform the server's half of the handshake (the call to clientSocket.CloseAsync will block until it receives the server's response).
                Task closeServerSocket = WaitForClose(serverSocket, "server", closeStatus, closeStatusDescription);

                Log.LogInformation("Closing client socket...");

                await clientSocket.CloseAsync(closeStatus, closeStatusDescription, TestCancellation).ConfigureAwait(false);
                Assert.Equal(WebSocketState.Aborted, clientSocket.State); // AF: Not sure whether or not this is a bug in CoreFX. See if it occurs when we directly use ClientWebSocket.
                
                Log.LogInformation("Client socket closed (CloseStatus = '{CloseStatus}', CloseStatusDescription = '{CloseStatusDescription}').", clientSocket.CloseStatus, clientSocket.CloseStatusDescription);
                
                await closeServerSocket.ConfigureAwait(false);
                Assert.Equal(WebSocketState.Closed, serverSocket.State);

                Log.LogInformation("Disconnected.");

                Assert.Equal(closeStatus, clientSocket.CloseStatus);
                Assert.Equal(clientSocket.CloseStatus, serverSocket.CloseStatus);

                Assert.Equal(closeStatusDescription, clientSocket.CloseStatusDescription);
                Assert.Equal(clientSocket.CloseStatusDescription, serverSocket.CloseStatusDescription);
            }

            /// <summary>
            ///     Asynchronously wait for and respond to the first half the standard WebSocket-close handshake.
            /// </summary>
            /// <param name="socket">
            ///     The target <see cref="WebSocket"/>.
            /// </param>
            /// <param name="socketType">
            ///     A short textual description of the socket type (e.g. "server" or "client").
            /// <param>
            /// <param name="closeStatus">
            ///     An optional <see cref="WebSocketCloseStatus"/> value indicating the reason for disconnection.
            ///
            ///     Defaults to <see cref="WebSocketCloseStatus.NormalClosure"/>.
            /// </param>
            /// <param name="closeStatusDescription">
            ///     An optional textual description of the reason for disconnection.
            ///
            ///     Defaults to "Normal Closure".
            /// </param>
            /// <returns>
            ///     A <see cref="Task"/> representing the asynchronous operation.
            /// </returns>
            protected Task WaitForClose(WebSocket socket, string socketType, WebSocketCloseStatus closeStatus = WebSocketCloseStatus.NormalClosure, string closeStatusDescription = "Normal Closure")
            {
                if (socket == null)
                    throw new ArgumentNullException(nameof(socket));

                if (String.IsNullOrWhiteSpace(socketType))
                    throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'socketType'.", nameof(socketType));

                Log.LogInformation("{SocketType} socket waiting for Close message...", socketType);

                ArraySegment<byte> receiveBuffer = new byte[1024];
                Task<WebSocketReceiveResult> closeSocketTask = socket.ReceiveAsync(receiveBuffer, TestCancellation);

                return closeSocketTask.ContinueWith(async received =>
                {
                    if (received.IsFaulted)
                        Log.LogError(EventIds.ErrorClosingServerSocket, received.Exception.Flatten().InnerExceptions[0], "Server socket operation to receive Close message failed.");
                    else if (received.IsCanceled)
                        Log.LogWarning("{SocketType} socket operation to receive Close message was canceled.", socketType);
                    else
                    {
                        Log.LogInformation("Received {MessageType} message from {SocketType} socket (expecting {ExpectedMessageType}).",
                            received.Result.MessageType,
                            socketType,
                            WebSocketMessageType.Close
                        );

                        if (received.Result.MessageType == WebSocketMessageType.Close)
                        {
                            Log.LogInformation("Closing {SocketType} socket (with status {CloseStatus})...", socketType, received.Result.CloseStatus);

                            await socket.CloseAsync(closeStatus, closeStatusDescription, TestCancellation);

                            Log.LogInformation("{SocketType} socket closed.", socketType);
                        }

                        Assert.Equal(WebSocketMessageType.Close, received.Result.MessageType);
                    }
                });
            }

            /// <summary>
            ///     Send text to a multiplexed substream over the specified WebSocket.
            /// </summary>
            /// <param name="webSocket">
            ///     The target <see cref="WebSocket"/>.
            /// </param>
            /// <param name="streamIndex">
            ///     The 0-based index of the target substream.
            /// </param>
            /// <param name="text">
            ///     The text to send.
            /// </param>
            /// <param name="encoding">
            ///     The text encoding to use (defaults to UTF8).
            /// </param>
            /// <returns>
            ///     The number of bytes sent to the WebSocket.
            /// </returns>
            protected async Task<int> SendMultiplexed(WebSocket webSocket, byte streamIndex, string text, Encoding encoding = null)
            {
                if (webSocket == null)
                    throw new ArgumentNullException(nameof(webSocket));

                if (text == null)
                    throw new ArgumentNullException(nameof(text));

                if (encoding == null)
                    encoding = Encoding.UTF8;

                byte[] payload = encoding.GetBytes(text);
                byte[] sendBuffer = new byte[payload.Length + 1];

                sendBuffer[0] = streamIndex;
                Array.Copy(payload, 0, sendBuffer, 1, payload.Length);

                await webSocket.SendAsync(sendBuffer, WebSocketMessageType.Binary,
                    endOfMessage: true,
                    cancellationToken: TestCancellation
                );

                return sendBuffer.Length;
            }

            /// <summary>
            ///     Receive text from a multiplexed substream over the specified WebSocket.
            /// </summary>
            /// <param name="webSocket">
            ///     The target <see cref="WebSocket"/>.
            /// </param>
            /// <param name="text">
            ///     The text to send.
            /// </param>
            /// <param name="encoding">
            ///     The text encoding to use (defaults to UTF8).
            /// </param>
            /// <returns>
            ///     A tuple containing the received text, 0-based substream index, and total bytes received.
            /// </returns>
            protected async Task<(string text, byte streamIndex, int totalBytes)> ReceiveTextMultiplexed(WebSocket webSocket, Encoding encoding = null)
            {
                if (webSocket == null)
                    throw new ArgumentNullException(nameof(webSocket));

                if (encoding == null)
                    encoding = Encoding.UTF8;

                byte[] receivedData;
                using (MemoryStream buffer = new MemoryStream())
                {
                    byte[] receiveBuffer = new byte[1024];
                    WebSocketReceiveResult receiveResult = await webSocket.ReceiveAsync(receiveBuffer, TestCancellation);
                    if (receiveResult.MessageType != WebSocketMessageType.Binary)
                        throw new IOException($"Received unexpected WebSocket message of type '{receiveResult.MessageType}'.");

                    buffer.Write(receiveBuffer, 0, receiveResult.Count);

                    while (!receiveResult.EndOfMessage)
                    {
                        receiveResult = await webSocket.ReceiveAsync(receiveBuffer, TestCancellation);
                        buffer.Write(receiveBuffer, 0, receiveResult.Count);
                    }

                    buffer.Flush();

                    receivedData = buffer.ToArray();
                }

                return (
                    text: encoding.GetString(receivedData, 1, receivedData.Length - 1),
                    streamIndex: receivedData[0],
                    totalBytes: receivedData.Length
                );
            }

            /// <summary>
            ///     Event Id constants used in WebSocket tests.
            /// </summary>
            protected static class EventIds
            {
                /// <summary>
                ///     An error occurred while closing the server-side socket.
                /// </summary>
                public static readonly EventId ErrorClosingServerSocket = new EventId(1000, nameof(ErrorClosingServerSocket));
            }
        }
    }

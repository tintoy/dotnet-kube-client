using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace KubeClient.Extensions.WebSockets.Tests
{
    /// <summary>
    ///     Tests for <see cref="KubeApiClient"/>'s exec-in-pod functionality.
    /// </summary>
    public class PodExecTests
        : WebSocketTestBase
    {
        /// <summary>
        ///     Create a new <see cref="KubeApiClient"/> exec-in-pod test suite.
        /// </summary>
        /// <param name="testOutput">
        ///     Output for the current test.
        /// </param>
        public PodExecTests(ITestOutputHelper testOutput)
            : base(testOutput)
        {
        }

        /// <summary>
        ///     Verify that the client can request execution of a command in a pod's default container, over a raw WebSocket connection, with only the STDOUT stream enabled.
        /// </summary>
        [Fact(DisplayName = "Can exec in pod's default container, raw WebSocket, STDOUT only")]
        public async Task Exec_DefaultContainer_Raw_StdOut()
        {
            const string expectedOutput = "This is text sent to STDOUT.";

            TestTimeout(
                TimeSpan.FromSeconds(5)
            );
            await Host.StartAsync(TestCancellation);

            using (KubeApiClient client = CreateTestClient())
            {
                WebSocket clientSocket = await client.PodsV1().ExecAndConnectRaw(
                    podName: "pod1",
                    command: "/bin/bash",
                    stdin: true,
                    stdout: true,
                    stderr: true
                );
                Assert.Equal(K8sChannelProtocol.V1, clientSocket.SubProtocol); // For WebSockets, the Kubernetes API defaults to the binary channel (v1) protocol.

                using (clientSocket)
                {
                    Log.LogInformation("Waiting for server-side WebSocket.");

                    WebSocket serverSocket = await WebSocketTestAdapter.AcceptedPodExecV1Connection;

                    int bytesSent = await SendMultiplexed(serverSocket, K8sChannel.StdOut, expectedOutput);
                    Log.LogInformation("Sent {ByteCount} bytes to server socket; receiving from client socket...", bytesSent);

                    (string receivedText, byte streamIndex, int bytesReceived) = await ReceiveTextMultiplexed(clientSocket);
                    Log.LogInformation("Received {ByteCount} bytes from client socket ('{ReceivedText}', stream {StreamIndex}).", bytesReceived, receivedText, streamIndex);

                    Assert.Equal(K8sChannel.StdOut, streamIndex);
                    Assert.Equal(expectedOutput, receivedText);

                    await Disconnect(clientSocket, serverSocket);
                    
                    WebSocketTestAdapter.Done();
                }
            }
        }

        /// <summary>
        ///     Verify that the client can request execution of a command in a pod's default container, multiplexed, with all streams enabled.
        /// </summary>
        [Fact(DisplayName = "Can exec in pod's default container, multiplexed, all streams")]
        public async Task Exec_DefaultContainer_Multiplexed_AllStreams()
        {
            const string expectedPrompt = "/root # ";
            const string expectedCommand = "ls -l /root";

            TestTimeout(
                TimeSpan.FromSeconds(5)
            );
            await Host.StartAsync(TestCancellation);

            using (KubeApiClient client = CreateTestClient())
            {
                K8sMultiplexer multiplexer = await client.PodsV1().ExecAndConnect(
                    podName: "pod1",
                    command: "/bin/bash",
                    stdin: true,
                    stdout: true,
                    stderr: true
                );

                using (multiplexer)
                {
                    Stream stdin = multiplexer.GetStdIn();
                    Stream stdout = multiplexer.GetStdOut();

                    Log.LogInformation("Waiting for server-side WebSocket.");

                    WebSocket serverSocket = await WebSocketTestAdapter.AcceptedPodExecV1Connection;

                    Log.LogInformation("Server sends prompt.");
                    await SendMultiplexed(serverSocket, K8sChannel.StdOut, expectedPrompt);
                    Log.LogInformation("Server sent prompt.");

                    Log.LogInformation("Client expects prompt.");
                    byte[] receiveBuffer = new byte[2048];
                    int bytesReceived = await stdout.ReadAsync(receiveBuffer, 0, receiveBuffer.Length, TestCancellation);

                    string prompt = Encoding.ASCII.GetString(receiveBuffer, 0, bytesReceived);
                    Assert.Equal(expectedPrompt, prompt);
                    Log.LogInformation("Client got expected prompt.");
                    
                    Log.LogInformation("Client sends command.");
                    byte[] sendBuffer = Encoding.ASCII.GetBytes(expectedCommand);
                    await stdin.WriteAsync(sendBuffer, 0, sendBuffer.Length, TestCancellation);
                    Log.LogInformation("Client sent command.");

                    Log.LogInformation("Server expects command.");
                    (string command, byte streamIndex, int totalBytes) = await ReceiveTextMultiplexed(serverSocket);
                    Assert.Equal(K8sChannel.StdIn, streamIndex);

                    Assert.Equal(expectedCommand, command);
                    Log.LogInformation("Server got expected command.");

                    Task closeServerSocket = WaitForClose(serverSocket, socketType: "server");

                    Log.LogInformation("Close enough; we're done.");
                    await multiplexer.Shutdown(TestCancellation);

                    await closeServerSocket;
                    
                    WebSocketTestAdapter.Done();
                }
            }
        }
    }
}


using System;
using System.Diagnostics;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Xunit;

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
        public PodExecTests()
        {
        }

        /// <summary>
        ///     Verify that the client can request execution of a command in a pod's default container, with all streams enabled.
        /// </summary>
        [Fact(DisplayName = "Can exec in pod's default container, all streams")]
        public async Task Exec_DefaultContainer_AllStreams()
        {
            const string expectedPrompt = "/root # ";
            const string expectedCommand = "ls -l /root";

            if (!Debugger.IsAttached)
            {
                CancellationSource.CancelAfter(
                    TimeSpan.FromSeconds(5)
                );
            }
            await Host.StartAsync(Cancellation);

            using (KubeApiClient client = CreateTestClient())
            {
                // A small amount of jiggery-pokery to get our test server to keep the connection open for the lifetime of the test.
                var serverSocketAccepted = new TaskCompletionSource<WebSocket>();
                var testComplete = new TaskCompletionSource<object>();
                WebSocketTestAdapter.OnPodExec = webSocket =>
                {
                    serverSocketAccepted.SetResult(webSocket);

                    return testComplete.Task;
                };

                K8sMultiplexer multiplexer = await client.PodsV1().ExecAndConnect(
                    podName: "pod1",
                    command: "/bin/bash",
                    stdin: true,
                    stdout: true,
                    stderr: true
                );

                using (multiplexer)
                {
                    Stream stdin = multiplexer.GetOutputStream(0);
                    Stream stdout = multiplexer.GetInputStream(1);

                    WebSocket serverSocket = await serverSocketAccepted.Task;

                    // Server sends prompt.
                    byte[] payload = Encoding.ASCII.GetBytes(expectedPrompt);
                    byte[] sendBuffer = new byte[payload.Length + 1];
                    sendBuffer[0] = 1; // STDOUT
                    Array.Copy(payload, 0, sendBuffer, 1, payload.Length);
                    await serverSocket.SendAsync(sendBuffer, WebSocketMessageType.Binary, true, Cancellation);

                    // Client expects prompt.
                    byte[] receiveBuffer = new byte[2048];
                    int bytesReceived = await stdout.ReadAsync(receiveBuffer, 0, receiveBuffer.Length, Cancellation);

                    string prompt = Encoding.ASCII.GetString(receiveBuffer, 0, bytesReceived);
                    Assert.Equal(expectedPrompt, prompt);
                    
                    // Client sends command.
                    sendBuffer = Encoding.ASCII.GetBytes(expectedCommand); // No prefix needed, since this is a multiplexer stream.
                    await stdin.WriteAsync(sendBuffer, 0, sendBuffer.Length, Cancellation);

                    // Server expects command.
                    receiveBuffer = new byte[2048];
                    WebSocketReceiveResult receiveResult = await serverSocket.ReceiveAsync(receiveBuffer, Cancellation);
                    Assert.Equal(0 /* STDIN */, receiveBuffer[0]);

                    string command = Encoding.ASCII.GetString(receiveBuffer, 1, receiveResult.Count - 1);
                    Assert.Equal(expectedCommand, command);

                    // Close enough; we're done.
                    await multiplexer.Shutdown(Cancellation);
                    testComplete.SetResult(null);
                }
            }
        }
    }
}


using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.Extensions.WebSockets
{
    /// <summary>
    ///     A client for Kubernetes text channels over WebSockets.
    /// </summary>
    public sealed class K8sTextChannelClient
    {
        /// <summary>
        ///     Asynchronously send text to the specified (output) channel.
        /// </summary>
        /// <param name="channelIndex">
        ///     The (0-based) index of the target channel.
        /// </param>
        /// <param name="text">
        ///     The text to send.
        /// </param>
        /// <returns>
        ///     A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public Task Send(byte channelIndex, string text) => throw new NotImplementedException();

        /// <summary>
        ///     Get all lines received so far on the specified channel.
        /// </summary>
        /// <param name="channelIndex">
        ///     The (0-based) index of the target channel.
        /// </param>
        /// <returns>
        ///     An array of lines.
        /// </returns>
        /// <remarks>
        ///     Removes the returned lines from the receive buffer.
        /// </remarks>
        public Task<string[]> GetBufferedLines(byte channelIndex) => throw new NotImplementedException();

        /// <summary>
        ///     Discard all lines of text received so far on the specified channel.
        /// </summary>
        /// <param name="channelIndex">
        ///     The (0-based) index of the target channel.
        /// </param>
        /// <returns>
        ///     A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public Task DiscardBufferedLines(byte channelIndex) => throw new NotImplementedException();

        /// <summary>
        ///     Wait for specific text on the specified (input) channel.
        /// </summary>
        /// <param name="channelIndex">
        ///     The (0-based) index of the target channel.
        /// </param>
        /// <param name="text">
        ///     The text to wait for.
        /// </param>
        /// <returns>
        ///     A <see cref="K8sTextChannelReceiveResult"/> representing the received test.
        /// </returns>
        /// <remarks>
        ///     Blocks until the text is received.
        /// </remarks>
        public Task<K8sTextChannelReceiveResult> WaitForText(byte channelIndex, string text) => WaitForText(channelIndex, text, Timeout.InfiniteTimeSpan);

        /// <summary>
        ///    Wait for specific text on the specified (input) channel.
        /// </summary>
        /// <param name="channelIndex">
        ///     The (0-based) index of the target channel.
        /// </param>
        /// <param name="timeout">
        ///     The span of time to wait for the text.
        /// </param>
        /// <param name="text">
        ///     The text to wait for.
        /// </param>
        /// <returns>
        ///     A <see cref="K8sTextChannelReceiveResult"/> representing the received test.
        /// </returns>
        /// <remarks>
        ///     Blocks until the text is received or the timeout period elapses.
        /// </remarks>
        public Task<K8sTextChannelReceiveResult> WaitForText(byte channelIndex, string text, TimeSpan timeout) => throw new NotImplementedException();

        /// <summary>
        ///     Wait for specific text on the specified (input) channel.
        /// </summary>
        /// <param name="channelIndex">
        ///     The (0-based) index of the target channel.
        /// </param>
        /// <param name="cancellation">
        ///     A <see cref="CancellationToken"/> that can be used to cancel the wait operation.
        /// </param>
        /// <param name="text">
        ///     The text to wait for.
        /// </param>
        /// <returns>
        ///     A <see cref="K8sTextChannelReceiveResult"/> representing the received test.
        /// </returns>
        /// <remarks>
        ///     Blocks until the text is received or the cancellation token is canceled.
        /// </remarks>
        public Task<K8sTextChannelReceiveResult> WaitForText(byte channelIndex, string text, CancellationToken cancellation) => throw new NotImplementedException();

        /// <summary>
        ///     Wait for text matching a regular expression on the specified (input) channel.
        /// </summary>
        /// <param name="channelIndex">
        ///     The (0-based) index of the target channel.
        /// </param>
        /// <param name="regex">
        ///     A regular expression representing the text to wait for.
        /// </param>
        /// <returns>
        ///     A <see cref="K8sTextChannelReceiveResult"/> representing the received test.
        /// </returns>
        /// <remarks>
        ///     Blocks until the text is received.
        /// </remarks>
        public Task<K8sTextChannelReceiveResult> WaitForText(byte channelIndex, Regex regex) => WaitForText(channelIndex, regex, Timeout.InfiniteTimeSpan);

        /// <summary>
        ///     Wait for text matching a regular expression on the specified (input) channel.
        /// </summary>
        /// <param name="channelIndex">
        ///     The (0-based) index of the target channel.
        /// </param>
        /// <param name="timeout">
        ///     The span of time to wait for the text.
        /// </param>
        /// <param name="regex">
        ///     A regular expression representing the text to wait for.
        /// </param>
        /// <returns>
        ///     A <see cref="K8sTextChannelReceiveResult"/> representing the received test.
        /// </returns>
        /// <remarks>
        ///     Blocks until the text is received or the timeout period elapses.
        /// </remarks>
        public Task<K8sTextChannelReceiveResult> WaitForText(byte channelIndex, Regex regex, TimeSpan timeout) => throw new NotImplementedException();

        /// <summary>
        ///     Wait for text matching a regular expression on the specified (input) channel.
        /// </summary>
        /// <param name="channelIndex">
        ///     The (0-based) index of the target channel.
        /// </param>
        /// <param name="cancellation">
        ///     A <see cref="CancellationToken"/> that can be used to cancel the wait operation.
        /// </param>
        /// <param name="regex">
        ///     A regular expression representing the text to wait for.
        /// </param>
        /// <returns>
        ///     A <see cref="K8sTextChannelReceiveResult"/> representing the received test.
        /// </returns>
        /// <remarks>
        ///     Blocks until the text is received or the cancellation token is canceled.
        /// </remarks>
        public Task<K8sTextChannelReceiveResult> WaitForText(byte channelIndex, Regex regex, CancellationToken cancellation) => throw new NotImplementedException();

        /// <summary>
        ///     Wait for a complete line of text on the specified channel.
        /// </summary>
        /// <param name="channelIndex">
        ///     The (0-based) index of the target channel.
        /// </param>
        public Task<K8sTextChannelReceiveResult> WaitForLineReceived(byte channelIndex) => throw new NotImplementedException();

        /// <summary>
        ///     Invoke a callback each time specific text is received on the specified (input) channel.
        /// </summary>
        /// <param name="channelIndex">
        ///     The (0-based) index of the target channel.
        /// </param>
        /// <param name="text">
        ///     The text to wait for.
        /// </param>
        /// <param name="onTextReceived">
        ///     An asynchronous callback invoked when the text is received.
        /// </param>
        /// <returns>
        ///     An <see cref="IDisposable"/> representing the subscription; when disposed, the callback will no longer be invoked.
        /// </returns>
        public IDisposable WhenTextReceived(byte channelIndex, string text, Func<K8sTextChannelReceiveResult, Task> onTextReceived) => WhenTextReceived(channelIndex, text, Timeout.InfiniteTimeSpan, onTextReceived);

        /// <summary>
        ///     Invoke a callback each time specific text is received on the specified (input) channel.
        /// </summary>
        /// <param name="channelIndex">
        ///     The (0-based) index of the target channel.
        /// </param>
        /// <param name="timeout">
        ///     The span of time to wait for the text.
        /// </param>
        /// <param name="text">
        ///     The text to wait for.
        /// </param>
        /// <param name="onTextReceived">
        ///     An asynchronous callback invoked when the text is received.
        /// </param>
        /// <returns>
        ///     An <see cref="IDisposable"/> representing the subscription; when disposed, the callback will no longer be invoked.
        /// </returns>
        public IDisposable WhenTextReceived(byte channelIndex, string text, TimeSpan timeout, Func<K8sTextChannelReceiveResult, Task> onTextReceived) => throw new NotImplementedException();

        /// <summary>
        ///     Invoke a callback each time specific text is received on the specified (input) channel.
        /// </summary>
        /// <param name="channelIndex">
        ///     The (0-based) index of the target channel.
        /// </param>
        /// <param name="cancellation">
        ///     A <see cref="CancellationToken"/> that can be used to cancel the wait operation.
        /// </param>
        /// <param name="text">
        ///     The text to wait for.
        /// </param>
        /// <param name="onTextReceived">
        ///     An asynchronous callback invoked when the text is received.
        /// </param>
        /// <returns>
        ///     An <see cref="IDisposable"/> representing the subscription; when disposed, the callback will no longer be invoked.
        /// </returns>
        public IDisposable WhenTextReceived(byte channelIndex, string text, CancellationToken cancellation, Action<K8sTextChannelReceiveResult, string> onTextReceived) => throw new NotImplementedException();

        /// <summary>
        ///     Invoke a callback each time a complete line of text on the specified channel.
        /// </summary>
        /// <param name="channelIndex">
        ///     The (0-based) index of the target channel.
        /// </param>
        /// <param name="onLineReceived">
        ///     An asynchronous callback invoked when the line of text is received.
        /// </param>
        /// <returns>
        ///     An <see cref="IDisposable"/> representing the subscription; when disposed, the callback will no longer be invoked.
        /// </returns>
        public IDisposable WhenLineReceived(byte channelIndex, Func<K8sTextChannelReceiveResult, Task> onLineReceived) => throw new NotImplementedException();
    }

    /// <summary>
    ///     Information about a message received from a Kubernetes text channel.
    /// </summary>
    public struct K8sTextChannelReceiveResult
    {
        /// <summary>
        ///     The client that received the text.
        /// </summary>
        public K8sTextChannelClient Client { get; }

        /// <summary>
        ///     The (0-based) index of the channel on which the text was received.
        /// </summary>
        public byte ChannelIndex { get; }

        /// <summary>
        ///     The (0-based) index of the channel (if any) on which replies can be sent.
        /// </summary>
        public byte? ReplyToChannelIndex { get; }

        /// <summary>
        ///     The text that was received.
        /// </summary>
        public string Text { get; }

        /// <summary>
        ///     The current line (so far) that the text was matched in.
        /// </summary>
        public string CurrentLine { get; }

        /// <summary>
        ///     Reply to the received text.
        /// </summary>
        /// <param name="text">
        ///     The text to reply with.
        /// </param>
        public void ReplyWith(string text)
        {
            if (!ReplyToChannelIndex.HasValue)
                throw new NotSupportedException($"No channel is available to send replies for channel {ChannelIndex}.");

            Send(ReplyToChannelIndex.Value, text);
        }

        /// <summary>
        ///     Send text to the specified channel.
        /// </summary>
        /// <param name="channelIndex">
        ///     The (0-based) index of the target channel.
        /// </param>
        /// <param name="text">
        ///     The text to send.
        /// </param>
        public void Send(byte channelIndex, string text) => Client.Send(channelIndex, text);
    }
}
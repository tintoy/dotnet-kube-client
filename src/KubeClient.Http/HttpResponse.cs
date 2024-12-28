using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace KubeClient.Http
{
	/// <summary>
	///		The response from an asynchronous invocation of an <see cref="HttpRequest"/>.
	/// </summary>
	public struct HttpResponse
	{
		/// <summary>
		///		Create a new <see cref="HttpResponse"/>.
		/// </summary>
		/// <param name="request">
		///		The request whose response is represented by the <see cref="HttpResponse"/>.
		/// </param>
		/// <param name="task">
		///		The underlying <see cref="Task{HttpResponse}"/> represented by the <see cref="HttpResponse"/>.
		/// </param>
		public HttpResponse(HttpRequest request, Task<HttpResponseMessage> task)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (task == null)
				throw new ArgumentNullException(nameof(task));

			Request = request;
			Task = task;
		}

		/// <summary>
		///		Create a new <see cref="HttpResponse"/> for the specified asynchronous action.
		/// </summary>
		/// <param name="request">
		///		The request whose response is represented by the <see cref="HttpResponse"/>.
		/// </param>
		/// <param name="asyncAction">
		///		An asynchronous delegate that produces the action's resulting <see cref="HttpResponseMessage"/>.
		/// </param>
		public HttpResponse(HttpRequest request, Func<Task<HttpResponseMessage>> asyncAction)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (asyncAction == null)
				throw new ArgumentNullException(nameof(asyncAction));

			Request = request;
			Task = asyncAction();
			if (Task == null)
				throw new InvalidOperationException("The asynchronous action delegate returned null.");
		}

		/// <summary>
		///		The request whose response is represented by the <see cref="HttpResponse"/>.
		/// </summary>
		public HttpRequest Request { get; set; }

		/// <summary>
		///		The underlying <see cref="Task{T}"/> represented by the <see cref="HttpResponse"/>.
		/// </summary>
		public Task<HttpResponseMessage> Task { get; }

		// TODO: Considering something like Promise's "Then" method to encapsulate the construction of a new HttpResponse using the same HttpRequest but a new async action.

		/// <summary>
		///		Get an awaiter for the underlying <see cref="Task{HttpResponse}"/> represented by the <see cref="HttpResponse"/>.
		/// </summary>
		/// <returns>
		///		The task awaiter.
		/// </returns>
		/// <remarks>
		///		Enables directly awaiting the <see cref="HttpResponse"/>.
		/// </remarks>
		public TaskAwaiter<HttpResponseMessage> GetAwaiter() => Task.GetAwaiter();

		/// <summary>
		///		Configure the way that the response's task is awaited.
		/// </summary>
		/// <param name="continueOnCapturedContext">
		///		Should the awaited task return to the ambient synchronisation context?
		/// </param>
		/// <returns>
		///		A <see cref="ConfiguredTaskAwaitable{HttpResponseMessage}"/> that can be awaited.
		/// </returns>
		public ConfiguredTaskAwaitable<HttpResponseMessage> ConfigureAwait(bool continueOnCapturedContext) => Task.ConfigureAwait(continueOnCapturedContext);

		/// <summary>
		///		Implicit conversion from <see cref="HttpResponse"/> to <see cref="Task{HttpResponse}"/>
		/// </summary>
		/// <param name="httpResponse">
		///		The <see cref="HttpResponse"/> to convert.
		/// </param>
		/// <returns>
		///		The <see cref="HttpResponse"/>'s <see cref="Task"/>.
		/// </returns>
		public static implicit operator Task<HttpResponseMessage>(HttpResponse httpResponse)
		{
			return httpResponse.Task;
		}
	}
}
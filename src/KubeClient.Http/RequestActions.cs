using System.Net.Http;

namespace KubeClient.Http
{
	/// <summary>
	///		Delegate that performs configuration of an outgoing HTTP request message.
	/// </summary>
	/// <param name="requestMessage">
	///		The outgoing request message.
	/// </param>
	public delegate void RequestAction(HttpRequestMessage requestMessage);
	
	/// <summary>
	///		Delegate that performs configuration of an outgoing HTTP request message.
	/// </summary>
	/// <typeparam name="TContext">
	///		The type of object used by the request when resolving deferred parameters.
	/// </typeparam>
	/// <param name="requestMessage">
	///		The outgoing request message.
	/// </param>
	/// <param name="context">
	///		The object used by the request when resolving deferred parameters.
	/// </param>
	public delegate void RequestAction<in TContext>(HttpRequestMessage requestMessage, TContext context);
}

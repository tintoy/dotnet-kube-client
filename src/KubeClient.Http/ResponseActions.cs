using System.Net.Http;

namespace KubeClient.Http
{
	/// <summary>
	///		Delegate that performs processing of an incoming HTTP response message.
	/// </summary>
	/// <param name="responseMessage">
	///		The incoming response message.
	/// </param>
	public delegate void ResponseAction(HttpResponseMessage responseMessage);
	
	/// <summary>
	///		Delegate that performs processing of an incoming HTTP response message.
	/// </summary>
	/// <typeparam name="TContext">
	///		The type of object used by the response when resolving deferred parameters.
	/// </typeparam>
	/// <param name="responseMessage">
	///		The incoming response message.
	/// </param>
	/// <param name="context">
	///		The object used by the response when resolving deferred parameters.
	/// </param>
	public delegate void ResponseAction<in TContext>(HttpResponseMessage responseMessage, TContext context);
}

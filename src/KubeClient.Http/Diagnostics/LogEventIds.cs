using Microsoft.Extensions.Logging;

namespace KubeClient.Http.Diagnostics
{
    /// <summary>
    ///		The Ids of well-known log events raised by KubeClient.Http diagnostics.
    /// </summary>
    public static class LogEventIds
	{
		/// <summary>
		/// 	An outgoing HTTP request is being performed. 
		/// </summary>
		public static readonly EventId BeginRequest	= new EventId(100, nameof(BeginRequest));

		/// <summary>
		/// 	The body of an outgoing HTTP request.
		/// </summary>
		public static readonly EventId RequestBody	= new EventId(101, nameof(RequestBody));

		/// <summary>
		/// 	The body of an incoming HTTP response.
		/// </summary>
		public static readonly EventId ResponseBody	= new EventId(102, nameof(ResponseBody));

		/// <summary>
		/// 	The body of an incoming HTTP response is streamed.
		/// </summary>
		public static readonly EventId StreamedResponse	= new EventId(103, nameof(StreamedResponse));
		
		/// <summary>
		/// 	An incoming HTTP response has been received.
		/// </summary>
		public static readonly EventId EndRequest	= new EventId(110, nameof(EndRequest));
		
		/// <summary>
		/// 	An exception occurred while performing an HTTP request.
		/// </summary>
		public static readonly EventId RequestError	= new EventId(115, nameof(RequestError));
	}
}

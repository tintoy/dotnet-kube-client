namespace KubeClient.Http.Templates
{
	/// <summary>
	///		The base class for URI template segments that represent segments of the URI.
	/// </summary>
	abstract class UriSegment
		: TemplateSegment
	{
		/// <summary>
		///		Does the segment represent a directory (i.e. have a trailing slash?).
		/// </summary>
		readonly bool _isDirectory;

		/// <summary>
		///		Create a new URI segment.
		/// </summary>
		/// <param name="isDirectory">
		///		Does the segment represent a directory (i.e. have a trailing slash?).
		/// </param>
		protected UriSegment(bool isDirectory)
		{
			_isDirectory = isDirectory;
		}

		/// <summary>
		///		Does the segment represent a directory (i.e. have a trailing slash?).
		/// </summary>
		public bool IsDirectory
		{
			get
			{
				return _isDirectory;
			}
		}
	}
}

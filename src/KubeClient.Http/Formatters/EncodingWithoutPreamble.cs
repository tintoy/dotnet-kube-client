using System.Text;

namespace KubeClient.Http
{
	/// <summary>
	/// 	Well-known text encodings for output (i.see. no preambles).
	/// </summary>
	/// <remarks>
	/// 	Some web APIs don't like being sent preambles to indicate text encoding (usually indicated by HTTP headers instead).
	/// </remarks>
	public static class OutputEncoding
	{
		/// <summary>
		/// 	UTF-8 encoding (no preamble).
		/// </summary>
		public static readonly Encoding UTF8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

		/// <summary>
		/// 	Unicode / UTF-16 encoding (no preamble).
		/// </summary>
		public static readonly Encoding Unicode = new UnicodeEncoding(bigEndian: false, byteOrderMark: false);
	}
}
using System;
using System.Collections.Generic;

namespace KubeClient.Http.Formatters
{
	/// <summary>
	///		Represents a collection of <see cref="IFormatter">content formatter</see>s.
	/// </summary>
    public interface IFormatterCollection
		: ICollection<IFormatter>
	{
		/// <summary>
		///		Get the most appropriate <see cref="IInputFormatter">formatter</see> to read the specified data.
		/// </summary>
		/// <param name="context">
		///		Contextual information about the data to deserialise.
		/// </param>
		/// <returns>
		///		The formatter, or <c>null</c> if none of the formatters in the collection can handle the specified content type.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		///		<paramref name="context"/> is <c>null</c>.
		/// </exception>
		IInputFormatter FindInputFormatter(InputFormatterContext context);

		/// <summary>
		///		Find the most appropriate <see cref="IOutputFormatter">formatter</see> to write the specified data.
		/// </summary>
		/// <param name="context">
		///		Contextual information about the data to deserialise.
		/// </param>
		/// <returns>
		///		The formatter, or <c>null</c> if none of the formatters in the collection can handle the specified content type.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		///		<paramref name="context"/> is <c>null</c>.
		/// </exception>
		IOutputFormatter FindOutputFormatter(OutputFormatterContext context);

		/// <summary>
		///		Determine whether the collection contains a formatter of the specified type.
		/// </summary>
		/// <param name="formatterType">
		///		The formatter type.
		/// </param>
		/// <returns>
		///		<c>true</c>, if the collection contains a formatter of the specified type; otherwise, <c>false</c>.
		/// </returns>
		bool Contains(Type formatterType);

		/// <summary>
		///		Remove the formatter of the specified type (if it is present in the collection).
		/// </summary>
		/// <param name="formatterType">
		///		The type of formatter to remove.
		/// </param>
		/// <returns>
		///		<c>true</c>, if the formatter was removed; otherwise, <c>false</c>.
		/// </returns>
		bool Remove(Type formatterType);
	}
}

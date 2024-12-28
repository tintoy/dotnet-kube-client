using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KubeClient.Http.Formatters
{
	/// <summary>
	///		A collection of <see cref="IFormatter">content formatter</see>s.
	/// </summary>
	public class FormatterCollection
		: IFormatterCollection
    {
		#region Instance data

		/// <summary>
		///		The underlying collection of formatters, keyed by type.
		/// </summary>
		readonly Dictionary<Type, IFormatter> _formatters = new Dictionary<Type, IFormatter>();

		#endregion // Instance data

		#region Construction

		/// <summary>
		///		Create a new <see cref="FormatterCollection"/>.
		/// </summary>
		public FormatterCollection()
		{
		}

		/// <summary>
		///		Create a new <see cref="FormatterCollection"/> by copying the specified <see cref="FormatterCollection"/>.
		/// </summary>
		/// <param name="formatterCollection">
		///		The <see cref="FormatterCollection"/> to copy.
		/// </param>
		public FormatterCollection(FormatterCollection formatterCollection)
		{
			if (formatterCollection == null)
				throw new ArgumentNullException(nameof(formatterCollection));

			foreach (KeyValuePair<Type, IFormatter> formatter in formatterCollection._formatters)
				_formatters.Add(formatter.Key, formatter.Value);
		}

		/// <summary>
		///		Create a new <see cref="FormatterCollection"/>.
		/// </summary>
		/// <param name="formatters">
		///		The formatters that the collection will initially contain.
		/// </param>
		public FormatterCollection(IEnumerable<IFormatter> formatters)
		{
			if (formatters == null)
				throw new ArgumentNullException(nameof(formatters));

			foreach (IFormatter formatter in formatters)
				Add(formatter);
		}

		/// <summary>
		///		Create a new <see cref="FormatterCollection"/>.
		/// </summary>
		/// <param name="formatters">
		///		The formatters that the collection will initially contain.
		/// </param>
		public FormatterCollection(params IFormatter[] formatters)
			: this((IEnumerable<IFormatter>)formatters)
		{
		}

		#endregion // Construction

		#region Collection

		/// <summary>
		///		The number of formatters in the collection.
		/// </summary>
		public int Count => _formatters.Count;

		/// <summary>
		///		Add a formatter to the collection.
		/// </summary>
		/// <param name="formatter">
		///		The formatter to add.
		/// </param>
		public void Add(IFormatter formatter)
		{
			if (formatter == null)
				throw new ArgumentNullException(nameof(formatter));

			Type formatterType = formatter.GetType();
			if (_formatters.ContainsKey(formatterType))
				throw new InvalidOperationException($"The collection already contains a formatter of type '{formatterType.FullName}'.");

			_formatters.Add(formatterType, formatter);
		}

		/// <summary>
		///		Determine whether the collection contains the specified formatter instance.
		/// </summary>
		/// <param name="formatter">
		///		The formatter.
		/// </param>
		/// <returns>
		///		<c>true</c>, if the collection contains the formatter; otherwise, <c>false</c>.
		/// </returns>
		public bool Contains(IFormatter formatter)
		{
			if (formatter == null)
				throw new ArgumentNullException(nameof(formatter));

			Type formatterType = formatter.GetType();

			return _formatters.ContainsKey(formatterType);
		}

		/// <summary>
		///		Determine whether the collection contains a formatter of the specified type.
		/// </summary>
		/// <param name="formatterType">
		///		The formatter type.
		/// </param>
		/// <returns>
		///		<c>true</c>, if the collection contains a formatter of the specified type; otherwise, <c>false</c>.
		/// </returns>
		public bool Contains(Type formatterType)
		{
			if (formatterType == null)
				throw new ArgumentNullException(nameof(formatterType));

			return _formatters.ContainsKey(formatterType);
		}

		/// <summary>
		///		Remove the specified formatter (if it is present in the collection).
		/// </summary>
		/// <param name="formatter">
		///		The formatter to remove.
		/// </param>
		/// <returns>
		///		<c>true</c>, if the formatter was removed; otherwise, <c>false</c>.
		/// </returns>
		public bool Remove(IFormatter formatter)
		{
			if (formatter == null)
				throw new ArgumentNullException(nameof(formatter));

			Type formatterType = formatter.GetType();

			return _formatters.Remove(formatterType);
		}

		/// <summary>
		///		Remove the formatter of the specified type (if it is present in the collection).
		/// </summary>
		/// <param name="formatterType">
		///		The type of formatter to remove.
		/// </param>
		/// <returns>
		///		<c>true</c>, if the formatter was removed; otherwise, <c>false</c>.
		/// </returns>
		public bool Remove(Type formatterType)
		{
			if (formatterType == null)
				throw new ArgumentNullException(nameof(formatterType));

			return _formatters.Remove(formatterType);
		}

		/// <summary>
		///		Remove all formatters from the collection.
		/// </summary>
		public void Clear()
		{
			_formatters.Clear();
		}

		#endregion // Collection

		#region Find a formatter

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
		public IInputFormatter FindInputFormatter(InputFormatterContext context)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));

			return
				_formatters.Values
					.OfType<IInputFormatter>()
					.FirstOrDefault(formatter => formatter.CanRead(context));
		}

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
		public IOutputFormatter FindOutputFormatter(OutputFormatterContext context)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));

			return
				_formatters.Values
					.OfType<IOutputFormatter>()
					.FirstOrDefault(formatter => formatter.CanWrite(context));
		}

		#endregion // Find a formatter

		#region IEnumerable<IFormatter>

		/// <summary>
		///		Get a typed enumerator for the formatters in the collection.
		/// </summary>
		/// <returns>
		///		The enumerator.
		/// </returns>
		public IEnumerator<IFormatter> GetEnumerator()
		{
			return _formatters.Values.GetEnumerator();
		}

		/// <summary>
		///		Get an untyped enumerator for the formatters in the collection.
		/// </summary>
		/// <returns>
		///		The enumerator.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion // IEnumerable<IFormatter>

		#region ICollection<IFormatter>

		/// <summary>
		///		Is the collection read-only?
		/// </summary>
		bool ICollection<IFormatter>.IsReadOnly => false;

		/// <summary>
		///		Copy the formatters in the collection to an array.
		/// </summary>
		/// <param name="array">
		///		The destination array.
		/// </param>
		/// <param name="arrayIndex">
		///		The starting index in the destination array.
		/// </param>
		public void CopyTo(IFormatter[] array, int arrayIndex)
		{
			if (array == null)
				throw new ArgumentNullException(nameof(array));

			_formatters.Values.CopyTo(array, arrayIndex);
		}

		#endregion // ICollection<IFormatter>
	}
}

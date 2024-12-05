using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace KubeClient.Http
{
    using Utilities;

	using RequestProperties = ImmutableDictionary<string, object>;

	/// <summary>
	///		The base class for HTTP request templates.
	/// </summary>
	public abstract class HttpRequestBase
		: IHttpRequestProperties
	{
		#region Instance data

		/// <summary>
		///		The request properties.
		/// </summary>
		readonly RequestProperties _properties;

		#endregion // Instance data

		#region Construction

		/// <summary>
		///		Create a new HTTP request.
		/// </summary>
		/// <param name="properties">
		///		The request properties.
		/// </param>
		protected HttpRequestBase(ImmutableDictionary<string, object> properties)
		{
			if (properties == null)
				throw new ArgumentNullException(nameof(properties));

			_properties = properties;

			EnsurePropertyType<Uri>(nameof(Uri));
			EnsurePropertyType<bool>(nameof(IsUriTemplate));
		}

		#endregion // Construction

		#region IHttpRequest

		/// <summary>
		///		The request URI.
		/// </summary>
		public Uri Uri => GetOptionalProperty<Uri>();

		/// <summary>
		///		Is the request URI a template?
		/// </summary>
		public bool IsUriTemplate => GetOptionalProperty<bool>();

		/// <summary>
		///		All properties for the request.
		/// </summary>
		public ImmutableDictionary<string, object> Properties => _properties;

		#endregion // IHttpRequest

		#region Request properties

		/// <summary>
		///		Determine whether the specified property is defined for the request.
		/// </summary>
		/// <param name="propertyName">
		///		The property name.
		/// </param>
		/// <returns>
		///		<c>true</c>, if the request is defined; otherwise, <c>false</c>.
		/// </returns>
		protected bool HaveProperty([CallerMemberName] string propertyName = null)
		{
			if (String.IsNullOrWhiteSpace(propertyName))
				throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'propertyName'.", nameof(propertyName));

			return _properties.ContainsKey(propertyName);
		}

		/// <summary>
		///		Get the specified request property.
		/// </summary>
		/// <typeparam name="TProperty">
		///		The type of property to retrieve.
		/// </typeparam>
		/// <param name="propertyName">
		///		The name of the property to retrieve.
		/// </param>
		/// <returns>
		///		The property value.
		/// </returns>
		/// <exception cref="ArgumentException">
		///		<paramref name="propertyName"/> is null, empty, or entirely composed of whitespace.
		/// </exception>
		/// <exception cref="KeyNotFoundException">
		///		The specified property is not defined.
		/// </exception>
		protected TProperty GetProperty<TProperty>([CallerMemberName] string propertyName = null)
		{
			if (String.IsNullOrWhiteSpace(propertyName))
				throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'propertyName'.", nameof(propertyName));
			
			object propertyValue;
			if (!_properties.TryGetValue(propertyName, out propertyValue))
				throw new KeyNotFoundException($"Property '{propertyName}' is not defined.");

			return (TProperty)propertyValue;
		}

		/// <summary>
		///		Get the specified request property.
		/// </summary>
		/// <typeparam name="TProperty">
		///		The type of property to retrieve.
		/// </typeparam>
		/// <param name="propertyName">
		///		The name of the property to retrieve.
		/// </param>
		/// <param name="defaultValue">
		///		The default value to return if the property is not defined.
		/// </param>
		/// <returns>
		///		The property value, or the default value if the property is not defined.
		/// </returns>
		/// <exception cref="ArgumentException">
		///		<paramref name="propertyName"/> is null, empty, or entirely composed of whitespace.
		/// </exception>
		/// <exception cref="KeyNotFoundException">
		///		The specified property is not defined.
		/// </exception>
		protected TProperty GetOptionalProperty<TProperty>([CallerMemberName] string propertyName = null, TProperty defaultValue = default(TProperty))
		{
			if (String.IsNullOrWhiteSpace(propertyName))
				throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'propertyName'.", nameof(propertyName));

			object propertyValue;
			if (_properties.TryGetValue(propertyName, out propertyValue))
				return (TProperty)propertyValue;

			return defaultValue;
		}

		/// <summary>
		///		Ensure that the specified property (if defined) is of the correct type.
		/// </summary>
		/// <typeparam name="TProperty">
		///		The expected property type.
		/// </typeparam>
		/// <param name="propertyName">
		///		The name of the property to validate.
		/// </param>
		/// <exception cref="ArgumentException">
		///		<paramref name="propertyName"/> is null, empty, or entirely composed of whitespace.
		/// </exception>
		protected void EnsurePropertyType<TProperty>(string propertyName)
		{
			if (String.IsNullOrWhiteSpace(propertyName))
				throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'propertyName'.", nameof(propertyName));

			object propertyValue;
			if (!_properties.TryGetValue(propertyName, out propertyValue))
				return;

			if (propertyValue is TProperty)
				return;

			// It's not of the correct type, but is that because it's null?
			Type propertyType = typeof(TProperty);
			if (propertyValue != null)
			{
				throw new InvalidOperationException(
					$"Value for property '{propertyName}' has unexpected type '{propertyType.FullName}' (should be '{propertyValue.GetType().FullName}')."
				);
			}

			// It's null; is that legal?
			if (typeof(TProperty).IsNullable())
				return;

			throw new InvalidOperationException(
				$"Property '{propertyName}' is null but its type ('{propertyType.FullName}') is not nullable."
			);
		}

		/// <summary>
		///		Clone the request properties, but with the specified changes.
		/// </summary>
		/// <param name="modifications">
		///		A delegate that modifies the request properties.
		/// </param>
		/// <returns>
		///		The cloned request properties.
		/// </returns>
		protected ImmutableDictionary<string, object> CloneProperties(Action<RequestProperties.Builder> modifications)
		{
			if (modifications == null)
				throw new ArgumentNullException(nameof(modifications));

			RequestProperties.Builder requestProperties = _properties.ToBuilder();
			modifications(requestProperties);

			return requestProperties.ToImmutable();
		}

		#endregion // Request properties

		#region Cloning

		/// <summary>
		///		Clone the request.
		/// </summary>
		/// <param name="modifications">
		///		A delegate that performs modifications to the request properties.
		/// </param>
		/// <returns>
		///		The cloned request.
		/// </returns>
		public virtual HttpRequestBase Clone(Action<IDictionary<string, object>> modifications)
		{
			if (modifications == null)
				throw new ArgumentNullException(nameof(modifications));

			return CreateInstance(
				CloneProperties(modifications)
			);
		}

		/// <summary>
		///		Create a new instance of the HTTP request using the specified properties.
		/// </summary>
		/// <param name="requestProperties">
		///		The request properties.
		/// </param>
		/// <returns>
		///		The new HTTP request instance.
		/// </returns>
		protected abstract HttpRequestBase CreateInstance(ImmutableDictionary<string, object> requestProperties);

		#endregion // Cloning

		#region ToString

		/// <summary>
		///		Convert the HTTP request to a textual representation.
		/// </summary>
		/// <returns>
		///		The textual representation.
		/// </returns>
		public override string ToString()
		{
			return $"HTTP Request ({Uri?.ToString() ?? "empty"})";
		}

		#endregion // ToString
	}
}

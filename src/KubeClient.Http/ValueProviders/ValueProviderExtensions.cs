using System;

namespace KubeClient.Http.ValueProviders
{
    /// <summary>
    ///		Extension methods for <see cref="IValueProvider{TContext,TValue}"/>.
    /// </summary>
    public static class ValueProviderExtensions
    {
        /// <summary>
        ///		Perform a conversion on the value provider.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The source type from which the value is extracted.
        /// </typeparam>
        /// <typeparam name="TValue">
        ///		The type of value extracted by the provider.
        /// </typeparam>
        /// <param name="valueProvider">
        ///		The value provider.
        /// </param>
        /// <returns>
        ///		A <see cref="ValueProviderConversion{TContext,TValue}"/> whose methods can be used to select the conversion to perform on the value converter.
        /// </returns>
        public static ValueProviderConversion<TContext, TValue> Convert<TContext, TValue>(this IValueProvider<TContext, TValue> valueProvider)
        {
            if (valueProvider == null)
                throw new ArgumentNullException(nameof(valueProvider));

            return new ValueProviderConversion<TContext, TValue>(valueProvider);
        }
    }
}

using System;

namespace KubeClient.Http.ValueProviders
{
    /// <summary>
    ///		Conversion operations for a value provider.
    /// </summary>
    /// <typeparam name="TContext">
    ///		The type used as a context for each request.
    /// </typeparam>
    /// <typeparam name="TValue">
    ///		The type of value returned by the value provider.
    /// </typeparam>
    public struct ValueProviderConversion<TContext, TValue>
    {
        /// <summary>
        ///		Create a new value-provider conversion.
        /// </summary>
        /// <param name="valueProvider">
        ///		The value provider being converted.
        /// </param>
        public ValueProviderConversion(IValueProvider<TContext, TValue> valueProvider)
            : this()
        {
            if (valueProvider == null)
                throw new ArgumentNullException(nameof(valueProvider));

            ValueProvider = valueProvider;
        }

        /// <summary>
        ///		The value provider being converted.
        /// </summary>
        public IValueProvider<TContext, TValue> ValueProvider { get; }

        /// <summary>
        ///		Wrap the specified value provider in a value provider that utilises a more-derived context type.
        /// </summary>
        /// <typeparam name="TDerivedContext">
        ///		The more-derived type used by the new provider as a context for each request.
        /// </typeparam>
        /// <returns>
        ///		The outer (converting) value provider.
        /// </returns>
        public IValueProvider<TDerivedContext, TValue> ContextTo<TDerivedContext>()
            where TDerivedContext : TContext
        {
            // Can't close over members of structs.
            IValueProvider<TContext, TValue> valueProvider = ValueProvider;

            return ValueProvider<TDerivedContext>.FromSelector(
                context => valueProvider.Get(context)
            );
        }

        /// <summary>
        ///		Wrap the value provider in a value provider that converts its value to a string.
        /// </summary>
        /// <returns>
        ///		The outer (converting) value provider.
        /// </returns>
        /// <remarks>
        ///		If the underlying value is <c>null</c> then the converted string value will be <c>null</c>, too.
        /// </remarks>
        public IValueProvider<TContext, string> ValueToString()
        {
            // Special-case conversion to save on allocations.
            if (typeof(TValue) == typeof(string))
                return (IValueProvider<TContext, string>)ValueProvider;

            // Can't close over members of structs.
            IValueProvider<TContext, TValue> valueProvider = ValueProvider;

            return ValueProvider<TContext>.FromSelector(
                context =>
                {
                    TValue value = valueProvider.Get(context);

                    return value != null ? value.ToString() : null;
                }
            );
        }
    }
}
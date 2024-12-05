using System;

namespace KubeClient.Http.ValueProviders
{
    /// <summary>
    ///		Factory methods for creating value providers.
    /// </summary>
    /// <typeparam name="TContext">
    ///		The type used as a context for each request.
    /// </typeparam>
    public static class ValueProvider<TContext>
    {
        /// <summary>
        ///		Create a value provider from the specified selector function.
        /// </summary>
        /// <typeparam name="TValue">
        ///		The type of value returned by the selector.
        /// </typeparam>
        /// <param name="selector">
        ///		A selector function that, when given an instance of <typeparamref name="TContext"/>, and returns a well-known value of type <typeparamref name="TValue"/> derived from the context.
        /// </param>
        /// <returns>
        ///		The value provider.
        /// </returns>
        public static IValueProvider<TContext, TValue> FromSelector<TValue>(Func<TContext, TValue> selector)
        {
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return new SelectorValueProvider<TValue>(selector);
        }

        /// <summary>
        ///		Create a value provider from the specified function.
        /// </summary>
        /// <typeparam name="TValue">
        ///		The type of value returned by the function.
        /// </typeparam>
        /// <param name="getValue">
        ///		A function that returns a well-known value of type <typeparamref name="TValue"/>.
        /// </param>
        /// <returns>
        ///		The value provider.
        /// </returns>
        public static IValueProvider<TContext, TValue> FromFunction<TValue>(Func<TValue> getValue)
        {
            if (getValue == null)
                throw new ArgumentNullException(nameof(getValue));

            return new FunctionValueProvider<TValue>(getValue);
        }

        /// <summary>
        ///		Create a value provider from the specified constant value.
        /// </summary>
        /// <typeparam name="TValue">
        ///		The type of value returned by the provider.
        /// </typeparam>
        /// <param name="value">
        ///		A constant value that is returned by the provider.
        /// </param>
        /// <returns>
        ///		The value provider.
        /// </returns>
        public static IValueProvider<TContext, TValue> FromConstantValue<TValue>(TValue value)
        {
            return new ConstantValueProvider<TValue>(value);
        }

        /// <summary>
        ///		Value provider that invokes a selector function on the context to extract its value.
        /// </summary>
        /// <typeparam name="TValue">
        ///		The type of value returned by the provider.
        /// </typeparam>
        class SelectorValueProvider<TValue>
            : IValueProvider<TContext, TValue>
        {
            /// <summary>
            ///		The selector function that extracts a value from the context.
            /// </summary>
            readonly Func<TContext, TValue> _selector;

            /// <summary>
            ///		Create a new selector-based value provider.
            /// </summary>
            /// <param name="selector">
            ///		The selector function that extracts a value from the context.
            /// </param>
            public SelectorValueProvider(Func<TContext, TValue> selector)
            {
                _selector = selector;
            }

            /// <summary>
            ///		Extract the value from the specified context.
            /// </summary>
            /// <param name="source">	
            ///		The TContext instance from which the value is to be extracted.
            /// </param>
            /// <returns>
            ///		The value.
            /// </returns>
            public TValue Get(TContext source)
            {
                if (source == null)
                    throw new InvalidOperationException("The current request template has one more more deferred parameters that refer to its context; the context parameter must therefore be supplied.");

                return _selector(source);
            }
        }

        /// <summary>
        ///		Value provider that invokes a function to extract its value.
        /// </summary>
        /// <typeparam name="TValue">
        ///		The type of value returned by the provider.
        /// </typeparam>
        class FunctionValueProvider<TValue>
            : IValueProvider<TContext, TValue>
        {
            /// <summary>
            ///		The function that is invoked to provide a value.
            /// </summary>
            readonly Func<TValue> _getValue;

            /// <summary>
            ///		Create a new function-based value provider.
            /// </summary>
            /// <param name="getValue">
            ///		The function that is invoked to provide a value.
            /// </param>
            public FunctionValueProvider(Func<TValue> getValue)
            {
                _getValue = getValue;
            }

            /// <summary>
            ///		Extract the value from the specified context.
            /// </summary>
            /// <param name="source">	
            ///		The TContext instance from which the value is to be extracted.
            /// </param>
            /// <returns>
            ///		The value.
            /// </returns>
            public TValue Get(TContext source)
            {
                if (source == null)
                    return default(TValue); // AF: Is this correct?

                return _getValue();
            }
        }

        /// <summary>
        ///		Value provider that returns a constant value.
        /// </summary>
        /// <typeparam name="TValue">
        ///		The type of value returned by the provider.
        /// </typeparam>
        class ConstantValueProvider<TValue>
            : IValueProvider<TContext, TValue>
        {
            /// <summary>
            ///		The constant value returned by the provider.
            /// </summary>
            readonly TValue _value;

            /// <summary>
            ///		Create a new constant value provider.
            /// </summary>
            /// <param name="value">
            ///		The constant value returned by the provider.
            /// </param>
            public ConstantValueProvider(TValue value)
            {
                _value = value;
            }

            /// <summary>
            ///		Extract the value from the specified context.
            /// </summary>
            /// <param name="source">	
            ///		The TContext instance from which the value is to be extracted.
            /// </param>
            /// <returns>
            ///		The value.
            /// </returns>
            public TValue Get(TContext source)
            {
                if (source == null)
                    return default(TValue); // AF: Is this correct?

                return _value;
            }
        }
    }
}

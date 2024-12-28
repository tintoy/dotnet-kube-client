using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace KubeClient.Http.Utilities
{
    /// <summary>
    ///		Helper methods for working with Reflection.
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        ///		Types that are known to be nullable.
        /// </summary>
        static readonly ConcurrentDictionary<Type, bool> _nullableTypes = new ConcurrentDictionary<Type, bool>();

        /// <summary>
        ///		Determine whether a reference to an instance of the type can be <c>null</c>.
        /// </summary>
        /// <param name="type">
        ///		The type.
        /// </param>
        /// <returns>
        ///		<c>true</c>, if the <paramref name="type"/> represents a reference type or a <see cref="Nullable{T}">nullable value type</see>.
        /// </returns>
        public static bool IsNullable(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return _nullableTypes.GetOrAdd(type, targetType =>
            {
                if (type.GetTypeInfo().IsClass)
                    return true;

                // For non-nullable types, Nullable.GetUnderlyingType just returns the type supplied to it.
                return Nullable.GetUnderlyingType(type) != type;
            });
        }
    }
}

using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Helper methods for working with model metadata.
    /// </summary>
    public static class ModelMetadata
    {
        /// <summary>
        ///     Represents an empty list of keys (resource field names).
        /// </summary>
        static readonly IReadOnlyList<string> NoKeys = new string[0];

        /// <summary>
        ///     Helper methods for working with model metadata relating to strategic resource patching.
        /// </summary>
        public static class StrategicPatch
        {
            /// <summary>
            ///     Determine whether the specified resource field supports merge in K8s strategic patch.
            /// </summary>
            /// <param name="property">
            ///     The target property.
            /// </param>
            /// <returns>
            ///     <c>true</c>, if the property supports merge; otherwise, <c>false</c>.
            /// </returns>
            public static bool IsMergeProperty(PropertyInfo property)
            {
                if (property == null)
                    throw new ArgumentNullException(nameof(property));
                
                return property.GetCustomAttribute<StrategicPatchMergeAttribute>() != null;
            }

            /// <summary>
            ///     Determine whether the specified resource field discards a subset of fields if they are not supplied in K8s strategic patch.
            /// </summary>
            /// <param name="property">
            ///     The target property.
            /// </param>
            /// <returns>
            ///     <c>true</c>, if the property supports merge; otherwise, <c>false</c>.
            /// </returns>
            public static bool IsRetainKeysProperty(PropertyInfo property)
            {
                if (property == null)
                    throw new ArgumentNullException(nameof(property));
                
                return property.GetCustomAttribute<StrategicPatchRetainKeysAttribute>() != null;
            }

            /// <summary>
            ///     Get the merge key (if any) represented by the specified model property.
            /// </summary>
            /// <param name="property">
            ///     The target property.
            /// </param>
            /// <returns>
            ///     The merge key, or <c>null</c> if the property does not represent the resource's merge key.
            /// </returns>
            public static string GetMergeKey(PropertyInfo property)
            {
                if (property == null)
                    throw new ArgumentNullException(nameof(property));
                
                return property.GetCustomAttribute<StrategicPatchMergeAttribute>()?.Key;
            }

            /// <summary>
            ///     Get the names of fields (if any) that are always retained when patching the resource field represented by the specified model property.
            /// </summary>
            /// <param name="property">
            ///     The target property.
            /// </param>
            /// <returns>
            ///     A read-only list of field names.
            /// </returns>
            public static IReadOnlyList<string> GetRetainKeys(PropertyInfo property)
            {
                if (property == null)
                    throw new ArgumentNullException(nameof(property));
                
                return property.GetCustomAttribute<StrategicPatchRetainKeysAttribute>()?.RetainKeys ?? NoKeys;
            }
        }

        /// <summary>
        ///     Helper methods for working with typed model metadata relating to strategic resource patching.
        /// </summary>
        /// <typeparam name="TModel">
        ///     The model type.
        /// </typeparam>
        public static class StrategicPatchFor<TModel>
            where TModel : class
        {
            /// <summary>
            ///     Determine whether the specified property supports merge in K8s strategic resource patching.
            /// </summary>
            /// <typeparam name="TProperty">
            ///     The property type.
            /// </typeparam>
            /// <param name="propertyAccessExpression">
            ///     A property-access expression representing the target property.
            /// </param>
            /// <returns>
            ///     <c>true</c>, if the property supports merge; otherwise, <c>false</c>.
            /// </returns>
            public static bool IsMergeProperty<TProperty>(Expression<Func<TModel, TProperty>> propertyAccessExpression)
            {
                if (propertyAccessExpression == null)
                    throw new ArgumentNullException(nameof(propertyAccessExpression));

                return StrategicPatch.IsMergeProperty(
                    GetProperty(propertyAccessExpression)
                );
            }

            /// <summary>
            ///     Get the merge key (if any) represented by the specified model property.
            /// </summary>
            /// <typeparam name="TProperty">
            ///     The property type.
            /// </typeparam>
            /// <param name="propertyAccessExpression">
            ///     A property-access expression representing the target property.
            /// </param>
            /// <returns>
            ///     The merge key, or <c>null</c> if the property does not represent the resource's merge key.
            /// </returns>
            public static string GetMergeKey<TProperty>(Expression<Func<TModel, TProperty>> propertyAccessExpression)
            {
                if (propertyAccessExpression == null)
                    throw new ArgumentNullException(nameof(propertyAccessExpression));

                return StrategicPatch.GetMergeKey(
                    GetProperty(propertyAccessExpression)
                );
            }

            /// <summary>
            ///     Get the names of fields (if any) that are always retained when patching the resource field represented by the specified model property.
            /// </summary>
            /// <param name="propertyAccessExpression">
            ///     A property-access expression representing the target property.
            /// </param>
            /// <returns>
            ///     A read-only list of field names.
            /// </returns>
            public static IReadOnlyList<string> GetRetainKeys<TProperty>(Expression<Func<TModel, TProperty>> propertyAccessExpression)
            {
                if (propertyAccessExpression == null)
                    throw new ArgumentNullException(nameof(propertyAccessExpression));

                return StrategicPatch.GetRetainKeys(
                    GetProperty(propertyAccessExpression)
                );
            }
        }

        /// <summary>
        ///     Retrieve the property represented by the specified property-access expression.
        /// </summary>
        /// <param name="propertyAccessExpression">
        ///     A property-access expression representing the target property.
        /// </param>
        /// <typeparam name="TModel">
        ///     The model type.
        /// </typeparam>
        /// <typeparam name="TProperty">
        ///     The property type.
        /// </typeparam>
        /// <returns>
        ///     A <see cref="PropertyInfo"/> representing the property.
        /// </returns>
        static PropertyInfo GetProperty<TModel, TProperty>(Expression<Func<TModel, TProperty>> propertyAccessExpression)
            where TModel : class
        {
            if (propertyAccessExpression.Body.NodeType != ExpressionType.MemberAccess)
                throw new ArgumentException("The supplied expression does not represent a member-access expression.", nameof(propertyAccessExpression));

            MemberExpression memberAccess = (MemberExpression)propertyAccessExpression.Body;
            PropertyInfo property = memberAccess.Member as PropertyInfo;
            if (property == null)
                throw new ArgumentException("The supplied expression does not represent a property-access expression.", nameof(propertyAccessExpression));

            return property;
        }
    }
}

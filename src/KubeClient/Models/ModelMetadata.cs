using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Linq;

namespace KubeClient.Models
{
    /// <summary>
    ///     Helper methods for working with model metadata.
    /// </summary>
    public static class ModelMetadata
    {
        /// <summary>
        ///     Helper methods for working with model metadata relating to strategic resource patching.
        /// </summary>
        public static class StrategicPatch
        {
            /// <summary>
            ///     Determine whether the specified property supports merge in K8s strategic resource patching.
            /// </summary>
            /// <typeparam name="TModel">
            ///     The model type.
            /// </typeparam>
            /// <typeparam name="TProperty">
            ///     The property type.
            /// </typeparam>
            /// <param name="propertyAccessExpression">
            ///     A property-access expression representing the target property.
            /// </param>
            /// <returns>
            ///     <c>true</c>, if the property supports merge; otherwise, <c>false</c>.
            /// </returns>
            public static bool IsMergeProperty<TModel, TProperty>(Expression<Func<TModel, TProperty>> propertyAccessExpression)
                where TModel : class
            {
                if (propertyAccessExpression == null)
                    throw new ArgumentNullException(nameof(propertyAccessExpression));

                return IsMergeProperty(
                    GetProperty(propertyAccessExpression)
                );
            }

            /// <summary>
            ///     Determine whether the specified property represents a resource's merge key in K8s strategic resource patching.
            /// </summary>
            /// <typeparam name="TModel">
            ///     The model type.
            /// </typeparam>
            /// <typeparam name="TProperty">
            ///     The property type.
            /// </typeparam>
            /// <param name="propertyAccessExpression">
            ///     A property-access expression representing the target property.
            /// </param>
            /// <returns>
            ///     <c>true</c>, if the property represents the resource's merge key; otherwise, <c>false</c>.
            /// </returns>
            public static bool IsMergeKeyProperty<TModel, TProperty>(Expression<Func<TModel, TProperty>> propertyAccessExpression)
                where TModel : class
            {
                if (propertyAccessExpression == null)
                    throw new ArgumentNullException(nameof(propertyAccessExpression));

                return IsMergeKeyProperty(
                    GetProperty(propertyAccessExpression)
                );
            }

            /// <summary>
            ///     Get the merge key (if any) represented by the specified model property.
            /// </summary>
            /// <typeparam name="TModel">
            ///     The model type.
            /// </typeparam>
            /// <typeparam name="TProperty">
            ///     The property type.
            /// </typeparam>
            /// <param name="propertyAccessExpression">
            ///     A property-access expression representing the target property.
            /// </param>
            /// <returns>
            ///     The merge key, or <c>null</c> if the property does not represent the resource's merge key.
            /// </returns>
            public static string GetMergeKey<TModel, TProperty>(Expression<Func<TModel, TProperty>> propertyAccessExpression)
                where TModel : class
            {
                if (propertyAccessExpression == null)
                    throw new ArgumentNullException(nameof(propertyAccessExpression));

                return GetMergeKey(
                    GetProperty(propertyAccessExpression)
                );
            }

            /// <summary>
            ///     Get the merge key (if any) for the resource represented by the specified model.
            /// </summary>
            /// <typeparam name="TModel">
            ///     The model type.
            /// </typeparam>
            /// <returns>
            ///     The name of the resource's merge-key field; <c>null</c> if no merge key is defined for the resource type.
            /// </returns>
            public static string GetMergeKey<TModel>() where TModel : class => GetMergeKey(typeof(TModel));

            /// <summary>
            ///     Determine whether the specified property supports merge in K8s strategic resource patching.
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
                
                return property.GetCustomAttribute<StrategicMergePatchAttribute>() != null;
            }

            /// <summary>
            ///     Determine whether the specified property represents a resource's merge key in K8s strategic resource patching.
            /// </summary>
            /// <param name="property">
            ///     The target property.
            /// </param>
            /// <returns>
            ///     <c>true</c>, if the property represents the resource's merge key; otherwise, <c>false</c>.
            /// </returns>
            public static bool IsMergeKeyProperty(PropertyInfo property)
            {
                if (property == null)
                    throw new ArgumentNullException(nameof(property));

                return property.GetCustomAttribute<StrategicMergeKeyAttribute>() != null;
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
                
                return property.GetCustomAttribute<StrategicMergePatchAttribute>()?.MergeKey;
            }

            /// <summary>
            ///     Get the merge key (if any) for the resource represented by the specified model.
            /// </summary>
            /// <param name="modelClass">
            ///     The model type.
            /// </param>
            /// <returns>
            ///     The name of the resource's merge-key field; <c>null</c> if no merge key is defined for the resource type.
            /// </returns>
            public static string GetMergeKey(Type modelClass)
            {
                if (modelClass == null)
                    throw new ArgumentNullException(nameof(modelClass));

                if (!modelClass.GetTypeInfo().IsClass)
                    throw new InvalidOperationException($"Type '{modelClass.FullName}' is not a class.");

                PropertyInfo mergeKeyProperty = modelClass.GetProperties(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(
                    property => property.CanRead && property.CanWrite && IsMergeKeyProperty(property)
                );

                if (mergeKeyProperty == null)
                    return null;

                return GetMergeKey(mergeKeyProperty);
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
            if (propertyAccessExpression.NodeType != ExpressionType.MemberAccess)
                throw new ArgumentException("The supplied expression does not represent member access.", nameof(propertyAccessExpression));

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

using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Concurrent;
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
        ///     <see cref="KubeObjectV1"/> model metadata.
        /// </summary>
        public static class KubeObject
        {
            /// <summary>
            ///     The <see cref="Type"/> representing <see cref="KubeResourceV1"/>.
            /// </summary>
            static readonly Type KubeResourceV1Type = typeof(KubeResourceV1);

            /// <summary>
            ///     The <see cref="Type"/> representing <see cref="KubeResourceV1"/>.
            /// </summary>
            static readonly Type KubeResourceListV1Type = typeof(KubeResourceListV1);

            /// <summary>
            ///     Get kind and apiVersion metadata for all model types in the specified assembly that derive from <see cref="KubeResourceListV1"/>.
            /// </summary>
            /// <param name="assembly">
            ///     The target assembly.
            /// </param>
            /// <returns>
            ///     A dictionary of kind/apiVersion tuples, keyed by model type.
            /// </returns>
            public static Dictionary<Type, (string kind, string apiVersion)> BuildTypeToKindLookup(Assembly assembly)
            {
                if (assembly == null)
                    throw new ArgumentNullException(nameof(assembly));

                var lookup = new Dictionary<Type, (string kind, string apiVersion)>();

                foreach (Type modelType in assembly.GetTypes())
                {
                    TypeInfo modelTypeInfo = modelType.GetTypeInfo();
                    if (!modelTypeInfo.IsPublic)
                        continue;

                    if (!modelTypeInfo.IsClass)
                        continue;

                    if (modelTypeInfo.IsAbstract)
                        continue;

                    if (!KubeResourceV1Type.IsAssignableFrom(modelType))
                        continue;

                    var kubeObjectAttribute = modelTypeInfo.GetCustomAttribute<KubeObjectAttribute>();
                    if (kubeObjectAttribute == null)
                        continue;

                    var kubeKind = (kind: kubeObjectAttribute.Kind, apiVersion: kubeObjectAttribute.ApiVersion);
                    lookup[modelType] = kubeKind;
                }

                return lookup;
            }

            /// <summary>
            ///     Get kind and apiVersion metadata for all model types in the specified assembly that derive from <see cref="KubeResourceListV1"/>.
            /// </summary>
            /// <param name="assembly">
            ///     The target assembly.
            /// </param>
            /// <returns>
            ///     A dictionary of kind/apiVersion tuples, keyed by model type.
            /// </returns>
            public static Dictionary<Type, (string kind, string apiVersion)> BuildListTypeToKindLookup(Assembly assembly)
            {
                if (assembly == null)
                    throw new ArgumentNullException(nameof(assembly));

                var lookup = new Dictionary<Type, (string kind, string apiVersion)>();

                foreach (Type modelType in assembly.GetTypes())
                {
                    TypeInfo modelTypeInfo = modelType.GetTypeInfo();
                    if (!modelTypeInfo.IsPublic)
                        continue;

                    if (!modelTypeInfo.IsClass)
                        continue;

                    if (modelTypeInfo.IsAbstract)
                        continue;

                    if (!KubeResourceListV1Type.IsAssignableFrom(modelType))
                        continue;

                    var kubeObjectAttribute = modelTypeInfo.GetCustomAttribute<KubeObjectAttribute>();
                    if (kubeObjectAttribute == null)
                        continue;

                    var kubeKind = (kind: kubeObjectAttribute.Kind, apiVersion: kubeObjectAttribute.ApiVersion);
                    lookup[modelType] = kubeKind;
                }

                return lookup;
            }

            /// <summary>
            ///     Get kind and apiVersion metadata for all model types in the specified assembly that derive from <see cref="KubeObjectV1"/>.
            /// </summary>
            /// <param name="assembly">
            ///     The target assembly.
            /// </param>
            /// <returns>
            ///     A dictionary of model types, keyed by kind/apiVersion tuple.
            /// </returns>
            public static Dictionary<(string kind, string apiVersion), Type> BuildKindToTypeLookup(Assembly assembly)
            {
                if (assembly == null)
                    throw new ArgumentNullException(nameof(assembly));

                var lookup = new Dictionary<(string kind, string apiVersion), Type>();

                foreach (Type modelType in assembly.GetTypes())
                {
                    TypeInfo modelTypeInfo = modelType.GetTypeInfo();
                    if (!modelTypeInfo.IsPublic)
                        continue;

                    if (!modelTypeInfo.IsClass)
                        continue;

                    if (modelTypeInfo.IsAbstract)
                        continue;

                    if (!KubeResourceV1Type.IsAssignableFrom(modelType))
                        continue;

                    var kubeObjectAttribute = modelTypeInfo.GetCustomAttribute<KubeObjectAttribute>();
                    if (kubeObjectAttribute == null)
                        continue;

                    var kubeKind = (kind: kubeObjectAttribute.Kind, apiVersion: kubeObjectAttribute.ApiVersion);
                    lookup[kubeKind] = modelType;
                }

                return lookup;
            }

            /// <summary>
            ///     Get kind and apiVersion metadata for all model types in the specified assembly that derive from <see cref="KubeResourceListV1"/>.
            /// </summary>
            /// <param name="assembly">
            ///     The target assembly.
            /// </param>
            /// <returns>
            ///     A dictionary of model types, keyed by kind/apiVersion tuple.
            /// </returns>
            public static Dictionary<(string kind, string apiVersion), Type> BuildKindToListTypeLookup(Assembly assembly)
            {
                if (assembly == null)
                    throw new ArgumentNullException(nameof(assembly));

                var lookup = new Dictionary<(string kind, string apiVersion), Type>();

                foreach (Type modelType in assembly.GetTypes())
                {
                    TypeInfo modelTypeInfo = modelType.GetTypeInfo();
                    if (!modelTypeInfo.IsPublic)
                        continue;

                    if (!modelTypeInfo.IsClass)
                        continue;

                    if (modelTypeInfo.IsAbstract)
                        continue;

                    if (!KubeResourceListV1Type.IsAssignableFrom(modelType))
                        continue;

                    var kubeObjectAttribute = modelTypeInfo.GetCustomAttribute<KubeObjectAttribute>();
                    if (kubeObjectAttribute == null)
                        continue;

                    var kubeListItemAttribute = modelTypeInfo.GetCustomAttribute<KubeListItemAttribute>();
                    if (kubeListItemAttribute == null)
                        continue;

                    var kubeKind = (kind: kubeListItemAttribute.Kind, apiVersion: kubeListItemAttribute.ApiVersion);
                    lookup[kubeKind] = modelType;
                }

                return lookup;
            }
        }

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
            public static bool IsMergeStrategy(PropertyInfo property)
            {
                if (property == null)
                    throw new ArgumentNullException(nameof(property));

                return property.GetCustomAttribute<MergeStrategyAttribute>() != null;
            }

            /// <summary>
            ///     Determine whether the specified resource field retains existing values for fields if they are not supplied in K8s strategic patch (via the $retainKeys directive).
            /// </summary>
            /// <param name="property">
            ///     The target property.
            /// </param>
            /// <returns>
            ///     <c>true</c>, if the existing values are retained; otherwise, <c>false</c>.
            /// </returns>
            public static bool IsRetainKeysStrategy(PropertyInfo property)
            {
                if (property == null)
                    throw new ArgumentNullException(nameof(property));

                return property.GetCustomAttribute<RetainKeysStrategyAttribute>() != null;
            }

            /// <summary>
            ///     Get all patch strategies applicable to the specified property.
            /// </summary>
            /// <param name="property">
            ///     The target property.
            /// </param>
            /// <returns>
            ///     One or more <see cref="PatchStrategies"/> flags indicating the applicable strategies.
            /// </returns>
            public static PatchStrategies GetStrategies(PropertyInfo property)
            {
                PatchStrategies patchStrategies = PatchStrategies.Replace;

                foreach (PatchStrategyAttribute strategyAttribute in property.GetCustomAttributes<PatchStrategyAttribute>())
                {
                    switch (strategyAttribute)
                    {
                        case MergeStrategyAttribute mergeStrategy:
                            {
                                patchStrategies |= PatchStrategies.Merge;

                                break;
                            }
                        case RetainKeysStrategyAttribute retainKeys:
                            {
                                patchStrategies |= PatchStrategies.RetainKeys;

                                break;
                            }
                    }
                }

                return patchStrategies;
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

                return property.GetCustomAttribute<MergeStrategyAttribute>()?.Key;
            }

            /// <summary>
            ///     Helper methods for working with typed model metadata relating to strategic resource patching.
            /// </summary>
            /// <typeparam name="TModel">
            ///     The model type.
            /// </typeparam>
            public static class For<TModel>
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
                public static bool IsMergeStrategy<TProperty>(Expression<Func<TModel, TProperty>> propertyAccessExpression)
                {
                    if (propertyAccessExpression == null)
                        throw new ArgumentNullException(nameof(propertyAccessExpression));

                    return StrategicPatch.IsMergeStrategy(
                        GetProperty(propertyAccessExpression)
                    );
                }

                /// <summary>
                ///     Determine whether the specified resource field retains existing values for fields if they are not supplied in K8s strategic patch (via the $retainKeys directive).
                /// </summary>
                /// <typeparam name="TProperty">
                ///     The property type.
                /// </typeparam>
                /// <param name="propertyAccessExpression">
                ///     A property-access expression representing the target property.
                /// </param>
                /// <returns>
                ///     <c>true</c>, if existing values are retained; otherwise, <c>false</c>.
                /// </returns>
                public static bool IsRetainKeysStrategy<TProperty>(Expression<Func<TModel, TProperty>> propertyAccessExpression)
                {
                    if (propertyAccessExpression == null)
                        throw new ArgumentNullException(nameof(propertyAccessExpression));

                    return StrategicPatch.IsRetainKeysStrategy(
                        GetProperty(propertyAccessExpression)
                    );
                }

                /// <summary>
                ///     Get all patch strategies applicable to the specified property.
                /// </summary>
                /// <typeparam name="TProperty">
                ///     The property type.
                /// </typeparam>
                /// <param name="propertyAccessExpression">
                ///     A property-access expression representing the target property.
                /// </param>
                /// <returns>
                ///     One or more <see cref="PatchStrategies"/> flags indicating the applicable strategies.
                /// </returns>
                public static PatchStrategies GetStrategies<TProperty>(Expression<Func<TModel, TProperty>> propertyAccessExpression)
                {
                    if (propertyAccessExpression == null)
                        throw new ArgumentNullException(nameof(propertyAccessExpression));

                    return StrategicPatch.GetStrategies(
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

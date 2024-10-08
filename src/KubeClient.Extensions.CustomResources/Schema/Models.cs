using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace KubeClient.Extensions.CustomResources.Schema
{
    /// <summary>
    ///     Metadata that identifies a Kubernetes resource type.
    /// </summary>
    /// <param name="Group">
    ///     The resource"s API group ("group").
    /// </param>
    /// <param name="Version">
    ///     The resource"s API version ("apiVersion").
    /// </param>
    /// <param name="ResourceKind">
    ///     The resource"s type name (i.e. "kind").
    /// </param>
    public record class KubeResourceKind(string? Group, string Version, string ResourceKind)
    {
        
    }

    /// <summary>
    ///     Schema for one or more Kubernetes resource types.
    /// </summary>
    /// <param name="ResourceTypes">
    ///     Schemas for resource types, keyed by <see cref="KubeResourceKind"/>.
    /// </param>
    /// <param name="DataTypes">
    ///     Schemas for data types, keyed by name.
    /// </param>
    public record class KubeSchema(ImmutableDictionary<KubeResourceKind, KubeModel> ResourceTypes, ImmutableDictionary<string, KubeDataType> DataTypes);

    /// <summary>
    ///     A resource model in a Kubernetes API schema.
    /// </summary>
    /// <param name="ResourceType">
    ///     A <see cref="KubeResourceKind"/> representing the model's "group/version/kind" in the Kubernetes API.
    /// </param>
    /// <param name="Summary">
    ///     Summary documentation for the data type (if available).
    /// </param>
    /// <param name="Properties">
    ///     Schema for the model's properties.
    /// </param>
    public record class KubeModel(KubeResourceKind ResourceType, string? Summary, ImmutableDictionary<string, KubeModelProperty> Properties)
    {
        /// <summary>
        ///     The namne of the CLR type used to represent the model.
        /// </summary>
        public string ClrTypeName { get; } = ResourceType.ResourceKind + NameWrangler.CapitalizeName(ResourceType.Version);
    };

    /// <summary>
    ///     A model (i.e. a complex data-type) in a Kubernetes API schema.
    /// </summary>
    /// <param name="Name">
    ///     The model's name (generated from <see cref="KubeModel.ResourceType"/> and the property path where the sub-model is located).
    /// </param>
    /// <param name="Summary">
    ///     Summary documentation for the data type (if available).
    /// </param>
    /// <param name="Properties">
    ///     Schema for the model's properties.
    /// </param>
    public record class KubeSubModel(string Name, string? Summary, ImmutableDictionary<string, KubeModelProperty> Properties)
    {
        /// <summary>
        ///     The name of the CLR type used to represent the model.
        /// </summary>
        public string ClrTypeName => Name;
    };

    /// <summary>
    ///     A property of a Kubernetes resource-type model.
    /// </summary>
    /// <param name="Name">
    ///     The property name (sanitised).
    /// </param>
    /// <param name="Summary">
    ///     The property description (i.e. summary documentation).
    /// </param>
    /// <param name="DataType">
    ///     The property data-type.
    /// </param>
    /// <param name="SerializedName">
    ///     The property's name when serialised.
    /// </param>
    /// <param name="IsOptional">
    ///     Is the property optional?
    /// </param>
    /// <param name="IsMerge">
    ///     Does the property support patch-merge?
    /// </param>
    /// <param name="MergeKey">
    ///     If the property supports patch-merge, the key (i.e. fields) used to merge changes.
    /// </param>
    /// <param name="IsRetainKeys">
    ///     If the property supports patch-merge, does it support the "retainKeys" strategy?
    /// </param>
    public record class KubeModelProperty(string Name, string? Summary, KubeDataType DataType, string SerializedName, bool IsOptional, bool IsMerge, string? MergeKey, bool IsRetainKeys);

    /// <summary>
    ///     The base class for data types in a Kubernetes API schema.
    /// </summary>
    /// <param name="Name">
    ///     The name of the data type (sanitised).
    /// </param>
    /// <param name="Summary">
    ///     Summary documentation for the data type (if available).
    /// </param>
    public record class KubeDataType(string Name, string? Summary)
    {
        /// <summary>
        ///     Is the data-type an intrinsic data type (such as number or string)?
        /// </summary>
        public virtual bool IsIntrinsic => false;

        /// <summary>
        ///     Does the data-type represent a collection data type (such as an array or dictionary)?
        /// </summary>
        public virtual bool IsCollection => false;

        /// <summary>
        ///     Get the name of the CLR <see cref="Type"/> that is used to represent the data type.
        /// </summary>
        /// <param name="isNullable">
        ///     Require that the CLR <see cref="Type"/> is nullable?
        /// </param>
        public virtual string GetClrTypeName(bool isNullable = false) => GetCtsTypeName(Name);

        /// <summary>
        ///     Get the Common Type System (CTS) type name corresponding to a Swagger / OpenAPI type name.
        /// </summary>
        /// <param name="openApiTypeName">
        ///     The Swagger / OpenAPI type name (e.g. "string", "int", "boolean").
        /// </param>
        /// <returns>
        ///     The corresponding CTS type name.
        /// </returns>
        protected static string GetCtsTypeName(string openApiTypeName)
        {
            if (String.IsNullOrWhiteSpace(openApiTypeName))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(openApiTypeName)}.", nameof(openApiTypeName));

            return openApiTypeName switch
            {
                "integer" => "int",
                "boolean" => "bool",

                _ => openApiTypeName,
            };
        }
    };

    /// <summary>
    ///     An intrinsic data type in the Kubernetes API.
    /// </summary>
    /// <param name="Name">
    ///     The simplified (C#) CTS type name of the data type.
    /// </param>
    public record class KubeIntrinsicDataType(string Name)
        : KubeDataType(Name, Summary: null)
    {
        /// <summary>
        ///     Is the data-type an intrinsic data type (such as number or string)?
        /// </summary>
        public override bool IsIntrinsic => false;

        /// <summary>
        ///     Does the data-type represent a collection data type (such as an array or dictionary)?
        /// </summary>
        public override bool IsCollection => false;

        /// <summary>
        ///     Get the name of the CLR <see cref="Type"/> that is used to represent the data type.
        /// </summary>
        /// <param name="isNullable">
        ///     Require that the CLR <see cref="Type"/> is nullable?
        /// </param>
        public override string GetClrTypeName(bool isNullable = false)
        {
            string clrTypeName = base.GetClrTypeName(isNullable);
            if (isNullable && SchemaConstants.ValueTypeNames.Contains(clrTypeName))
                clrTypeName += "?";

            return clrTypeName;
        }
    };

    /// <summary>
    ///     A model (i.e. resource) data type in the Kubernetes API.
    /// </summary>
    /// <param name="Model">
    ///     A <see cref="KubeModel"/> that describes the complex data-type.
    /// </param>
    public record class KubeModelDataType(KubeModel Model)
        : KubeDataType(Model.ClrTypeName, Summary: Model.Summary)
    {
        /// <summary>
        ///     Is the data-type an intrinsic data type (such as number or string)?
        /// </summary>
        public override bool IsIntrinsic => false;

        /// <summary>
        ///     Does the data-type represent a collection data type (such as an array or dictionary)?
        /// </summary>
        public override bool IsCollection => false;

        /// <summary>
        ///     Get the name of the CLR <see cref="Type"/> that is used to represent the data type.
        /// </summary>
        /// <param name="isNullable">
        ///     Require that the CLR <see cref="Type"/> is nullable?
        /// </param>
        public override string GetClrTypeName(bool isNullable = false)
        {
            string clrTypeName = base.GetClrTypeName(isNullable);
            if (isNullable && SchemaConstants.ValueTypeNames.Contains(clrTypeName))
                clrTypeName += "?";

            return clrTypeName;
        }
    };

    /// <summary>
    ///     A sub-model (i.e. complex) data type in the Kubernetes API.
    /// </summary>
    /// <param name="SubModel">
    ///     A <see cref="KubeModel"/> that describes the complex data-type.
    /// </param>
    public record class KubeSubModelDataType(KubeSubModel SubModel)
        : KubeDataType(SubModel.ClrTypeName, Summary: SubModel.Summary)
    {
        /// <summary>
        ///     Is the data-type an intrinsic data type (such as number or string)?
        /// </summary>
        public override bool IsIntrinsic => false;

        /// <summary>
        ///     Does the data-type represent a collection data type (such as an array or dictionary)?
        /// </summary>
        public override bool IsCollection => false;

        /// <summary>
        ///     Get the name of the CLR <see cref="Type"/> that is used to represent the data type.
        /// </summary>
        /// <param name="isNullable">
        ///     Require that the CLR <see cref="Type"/> is nullable?
        /// </param>
        public override string GetClrTypeName(bool isNullable = false)
        {
            string clrTypeName = base.GetClrTypeName(isNullable);
            if (isNullable && SchemaConstants.ValueTypeNames.Contains(clrTypeName))
                clrTypeName += "?";

            return clrTypeName;
        }
    };

    /// <summary>
    ///     An array data type in the Kubernetes API.
    /// </summary>
    /// <param name="ElementType">
    ///     A <see cref="KubeDataType"/> representing the type of element contained in the array.
    /// </param>
    public record class KubeArrayDataType(KubeDataType ElementType)
        : KubeDataType(Name: $"{ElementType.Name}List", Summary: null)
    {
        /// <summary>
        ///     Does the data-type represent a collection data type (such as an array or dictionary)?
        /// </summary>
        public override bool IsCollection => true;

        /// <summary>
        ///     Get the name of the CLR <see cref="Type"/> that is used to represent the data type.
        /// </summary>
        /// <param name="isNullable">
        ///     Require that the CLR <see cref="Type"/> is nullable?
        /// </param>
        public override string GetClrTypeName(bool isNullable = false)
        {
            string elementTypeName = GetCtsTypeName(
                ElementType.GetClrTypeName(
                    isNullable: false // List<DateTime?> would be odious to deal with.
                )
            );

            return $"List<{elementTypeName}>"; // Whereas List<T> is, itself, always nullable.
        }
    };

    /// <summary>
    ///     An dictionary data type in the Kubernetes API.
    /// </summary>
    /// <param name="ElementType">
    ///     A <see cref="KubeDataType"/> representing the type of element contained in the dictionary.
    /// </param>
    /// <remarks>
    ///     We assume all dictionary types use strings as keys.
    /// </remarks>
    public record class KubeDictionaryDataType(KubeDataType ElementType)
        : KubeDataType(Name: $"{ElementType.Name}Dictionary", Summary: null)
    {
        /// <summary>
        ///     Does the data-type represent a collection data type (such as an array or dictionary)?
        /// </summary>
        public override bool IsCollection => true;

        /// <summary>
        ///     Get the name of the CLR <see cref="Type"/> that is used to represent the data type.
        /// </summary>
        /// <param name="isNullable">
        ///     Require that the CLR <see cref="Type"/> is nullable?
        /// </param>
        public override string GetClrTypeName(bool isNullable = false)
        {
            string elementTypeName = GetCtsTypeName(
                ElementType.GetClrTypeName(
                    isNullable: false // Dictionary<string, DateTime?> would be odious to deal with.
                )
            );

            return $"Dictionary<string, {elementTypeName}>"; // Whereas Dictionary<T> is, itself, always nullable.
        }
    };

    static class SchemaConstants
    {
        public static IReadOnlySet<string> ValueTypeNames = ImmutableHashSet.CreateRange([
            "bool",
            "int",
            "long",
            "double",
            "DateTime",
        ]);

        public static IReadOnlySet<string> IgnoreDataTypes = ImmutableHashSet.CreateRange([
            "io.k8s.apimachinery.pkg.apis.meta.v1.DeleteOptions",
            "io.k8s.apimachinery.pkg.apis.meta.v1.Time",
            "io.k8s.apimachinery.pkg.apis.meta.v1.MicroTime",

            "io.k8s.apimachinery.pkg.api.resource.Quantity",
            "io.k8s.apimachinery.pkg.util.intstr.IntOrString",

            // Present in both regular and and "extensions" groups:
            "io.k8s.api.extensions.v1beta1.Deployment",
            "io.k8s.api.extensions.v1beta1.DeploymentList",
            "io.k8s.api.extensions.v1beta1.DeploymentRollback",
            "io.k8s.api.extensions.v1beta1.NetworkPolicy",
            "io.k8s.api.extensions.v1beta1.NetworkPolicyList",
            "io.k8s.api.extensions.v1beta1.PodSecurityPolicy",
            "io.k8s.api.extensions.v1beta1.PodSecurityPolicyList",
            "io.k8s.api.extensions.v1beta1.ReplicaSet",
            "io.k8s.api.extensions.v1beta1.ReplicaSetList",
            "io.k8s.api.extensions.v1.Deployment",
            "io.k8s.api.extensions.v1.DeploymentList",
            "io.k8s.api.extensions.v1.DeploymentRollback",
            "io.k8s.api.extensions.v1.NetworkPolicy",
            "io.k8s.api.extensions.v1.NetworkPolicyList",
            "io.k8s.api.extensions.v1.PodSecurityPolicy",
            "io.k8s.api.extensions.v1.PodSecurityPolicyList",
            "io.k8s.api.extensions.v1.ReplicaSet",
            "io.k8s.api.extensions.v1.ReplicaSetList",
            "io.k8s.kubernetes.pkg.apis.apps.v1beta1.ControllerRevision",
            "io.k8s.kubernetes.pkg.apis.apps.v1beta1.ControllerRevisionList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.DaemonSet",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.DaemonSetList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.Deployment",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.DeploymentList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.DeploymentRollback",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.NetworkPolicy",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.NetworkPolicyList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.PodSecurityPolicy",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.PodSecurityPolicyList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.ReplicaSet",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.ReplicaSetList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.Scale",
            "io.k8s.kubernetes.pkg.apis.apps.v1.ControllerRevision",
            "io.k8s.kubernetes.pkg.apis.apps.v1.ControllerRevisionList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.DaemonSet",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.DaemonSetList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.Deployment",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.DeploymentList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.DeploymentRollback",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.NetworkPolicy",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.NetworkPolicyList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.PodSecurityPolicy",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.PodSecurityPolicyList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.ReplicaSet",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.ReplicaSetList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.Scale",

            // Special case for EventV1
            "io.k8s.api.events.v1.Event",
            "io.k8s.api.events.v1.EventList",

            // Hand-coded:
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.ThirdPartyResource",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.ThirdPartyResourceList",
        ]);
    }

    static class NameWrangler
    {
        static readonly Regex Sanitizer = new Regex(@"([a-z]+[\-\$0-9])");
        static readonly Regex Splitter = new Regex(@"([a-z]+)([A-Z0-9]+[a-z]+)");

        static readonly TextInfo InvariantText = CultureInfo.InvariantCulture.TextInfo;

        public static string CapitalizeName(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            
            string[] nameComponents = Splitter.Split(name);
            for (int componentIndex = 0; componentIndex < nameComponents.Length; componentIndex++)
            {
                string nameComponent = nameComponents[componentIndex];
                nameComponents[componentIndex] = InvariantText.ToTitleCase(nameComponent);
            }

            return String.Join(String.Empty, nameComponents);
        }

        public static string SanitizeName(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            string[] nameComponents = Sanitizer.Split(name);
            for (int componentIndex = 0; componentIndex < nameComponents.Length; componentIndex++)
            {
                string nameComponent = nameComponents[componentIndex];
                nameComponents[componentIndex] = InvariantText.ToTitleCase(nameComponent);
            }

            return String.Join(String.Empty, nameComponents);
        }

    }

}
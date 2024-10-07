using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.SqlTypes;
using System.Text;

namespace KubeClient.Extensions.CustomResources.SchemaGenerator
{


    /// <summary>
    ///     The base class for data types in a Kubernetes API schema.
    /// </summary>
    /// <param name="Name">
    ///     The name of the data type (sanitised).
    /// </param>
    /// <param name="Summary">
    ///     Summary documentation for the data type (if available).
    /// </param>
    public abstract record class KubeDataType(string Name, string? Summary)
    {
        /// <summary>
        ///     Is the data-type an intrinsic data type (such as number or string)?
        /// </summary>
        public abstract bool IsIntrinsic { get; }

        /// <summary>
        ///     Does the data-type represent a collection data type (such as an array or dictionary)?
        /// </summary>
        public abstract bool IsCollection { get; }

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
    ///     An array data type in the Kubernetes API.
    /// </summary>
    /// <param name="Name">
    ///     The data-type name.
    /// </param>
    /// <param name="ElementType">
    ///     A <see cref="KubeDataType"/> representing the type of element contained in the array.
    /// </param>
    public record KubeArrayDataType(string Name, KubeDataType ElementType)
        : KubeDataType(Name, Summary: null)
    {
        /// <summary>
        ///     Is the data-type an intrinsic data type (such as number or string)?
        /// </summary>
        public override bool IsIntrinsic => false;

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

    static class SchemaConstants
    {
        public static IReadOnlySet<string> ValueTypeNames = ImmutableHashSet.CreateRange([
            "bool",
            "int",
            "long",
            "double",
            "DateTime",
        ]);
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace KubeClient.Extensions.CustomResources
{
    using Models;

    /// <summary>
    ///     Generator for Custom Resource Definition (CRD) validation schemas.
    /// </summary>
    public static class SchemaGenerator
    {
        /// <summary>
        ///     The CLR type representing <see cref="KubeCustomResourceV1"/>.
        /// </summary>
        static readonly Type CustomResourceV1Type = typeof(KubeCustomResourceV1);

        /// <summary>
        ///     Generate the CRD validation schema for a specification model type.
        /// </summary>
        /// <typeparam name="TModel">
        ///     The CLR type representing the model.
        /// </typeparam>
        /// <returns>
        ///     The generated <see cref="JSONSchemaPropsV1Beta1"/>.
        /// </returns>
        public static JSONSchemaPropsV1Beta1 GenerateSchema<TModel>()
            where TModel : KubeCustomResourceV1
        {
            return GenerateSchema(typeof(TModel));
        }

        /// <summary>
        ///     Generate the CRD validation schema for a model type.
        /// </summary>
        /// <param name="modelType">
        ///     The CLR type representing the model.
        /// </param>
        /// <returns>
        ///     The generated <see cref="JSONSchemaPropsV1Beta1"/>.
        /// </returns>
        public static JSONSchemaPropsV1Beta1 GenerateSchema(Type modelType)
        {
            if (modelType == null)
                throw new ArgumentNullException(nameof(modelType));

            if (!CustomResourceV1Type.IsAssignableFrom(modelType))
                throw new ArgumentException($"Cannot generate JSON schema for model type '{modelType.FullName}' because it does not derive from '{CustomResourceV1Type.FullName}'.");

            TypeInfo modelTypeInfo = modelType.GetTypeInfo();
            if (modelTypeInfo.IsEnum)
                return GenerateEnumSchema(modelType);

            TypeCode modelTypeCode = Type.GetTypeCode(modelType);
            switch (modelTypeCode)
            {
                case TypeCode.String:
                {
                    return new JSONSchemaPropsV1Beta1
                    {
                        Type = "string"
                    };
                }
                case TypeCode.Boolean:
                {
                    return new JSONSchemaPropsV1Beta1
                    {
                        Type = "boolean"
                    };
                }
                case TypeCode.Int32:
                {
                    return new JSONSchemaPropsV1Beta1
                    {
                        Type = "integer",
                        Format = "int32"
                    };
                }
                case TypeCode.Int64:
                {
                    return new JSONSchemaPropsV1Beta1
                    {
                        Type = "integer",
                        Format = "int64"
                    };
                }
                case TypeCode.Single:
                {
                    return new JSONSchemaPropsV1Beta1
                    {
                        Type = "number",
                        Format = "float"
                    };
                }
                case TypeCode.Double:
                {
                    return new JSONSchemaPropsV1Beta1
                    {
                        Type = "float",
                        Format = "double"
                    };
                }
                case TypeCode.DateTime:
                {
                    return new JSONSchemaPropsV1Beta1
                    {
                        Type = "string",
                        Format = "date-time"
                    };
                }
                case TypeCode.Object:
                {
                    if (modelType.IsArray)
                    {
                        if (Type.GetTypeCode(modelType.GetElementType()) == TypeCode.Byte)
                        {
                            return new JSONSchemaPropsV1Beta1
                            {
                                Type = "string",
                                Format = "byte"
                            };
                        }

                        return GenerateArraySchema(modelType);
                    }

                    if (modelType == typeof(Guid))
                    {
                        return new JSONSchemaPropsV1Beta1
                        {
                            Type = "string",
                            Format = "uuid"
                        };
                    }

                    return GenerateObjectSchema(modelType);
                }
                default:
                {
                    throw new NotSupportedException(
                        $"Cannot generate schema for unsupported data-type '{modelType.FullName}'."
                    );
                }
            }
        }

        /// <summary>
        ///     Generate the CRD validation schema for an <see cref="Enum"/> data-type.
        /// </summary>
        /// <param name="enumType">
        ///     A <see cref="Type"/> representing the data-type.
        /// </param>
        /// <returns>
        ///     The generated <see cref="JSONSchemaPropsV1Beta1"/>.
        /// </returns>
        static JSONSchemaPropsV1Beta1 GenerateEnumSchema(Type enumType)
        {
            if (enumType == null)
                throw new ArgumentNullException(nameof(enumType));
            
            JSONSchemaPropsV1Beta1 schema = new JSONSchemaPropsV1Beta1
            {
                Type = "string",
                Description = enumType.Name
            };

            schema.Enum.AddRange(
                Enum.GetNames(enumType).Select(
                    memberName => new JSONV1Beta1
                    {
                        Raw = memberName
                    }
                )
            );

            return schema;
        }

        /// <summary>
        ///     Generate the CRD validation schema for an <see cref="Array"/> data-type.
        /// </summary>
        /// <param name="arrayType">
        ///     A <see cref="Type"/> representing the array data-type.
        /// </param>
        /// <returns>
        ///     The generated <see cref="JSONSchemaPropsV1Beta1"/>.
        /// </returns>        
        static JSONSchemaPropsV1Beta1 GenerateArraySchema(Type arrayType)
        {
            if (arrayType == null)
                throw new ArgumentNullException(nameof(arrayType));
            
            return new JSONSchemaPropsV1Beta1
            {
                Type = "array",
                Items = GenerateSchema(
                    arrayType.GetElementType()
                )
            };
        }

        /// <summary>
        ///     Generate the CRD validation schema for a complex data-type.
        /// </summary>
        /// <param name="objectType">
        ///     A <see cref="Type"/> representing the complex (object) data-type.
        /// </param>
        /// <returns>
        ///     The generated <see cref="JSONSchemaPropsV1Beta1"/>.
        /// </returns>  
        static JSONSchemaPropsV1Beta1 GenerateObjectSchema(Type objectType)
        {
            if (objectType == null)
                throw new ArgumentNullException(nameof(objectType));
            
            var schemaProps = new JSONSchemaPropsV1Beta1
            {
                Type = "object",
                Description = objectType.Name,
            };

            foreach (PropertyInfo property in objectType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (!(property.CanRead && property.CanWrite))
                    continue;
                    
                // Ignore non-serialised properties.
                if (property.GetCustomAttribute<JsonIgnoreAttribute>() != null)
                    continue;

                // We only want properties declared on KubeCustomResourceV1 (or classes derived from it).
                if (!typeof(KubeCustomResourceV1).IsAssignableFrom(property.DeclaringType))
                    continue;

                schemaProps.Properties[property.Name] = GenerateSchema(property.PropertyType);
            }

            schemaProps.Required.AddRange(
                GetRequiredPropertyNames(objectType)
            );
            
            return schemaProps;
        }

        /// <summary>
        ///     Get the names of required properties on the specified complex type.
        /// </summary>
        /// <param name="objectType">
        ///     A <see cref="Type"/> representing the complex (object) data-type.
        /// </param>
        /// <returns>
        ///     A sequence of property names.
        /// </returns>
        static IEnumerable<string> GetRequiredPropertyNames(Type objectType)
        {
            if (objectType == null)
                throw new ArgumentNullException(nameof(objectType));
            
            foreach (PropertyInfo property in objectType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (property.PropertyType.IsValueType && Nullable.GetUnderlyingType(property.PropertyType) == null)
                    yield return property.Name; // Value types (unless nullable) are required.
                else if (property.GetCustomAttribute<JsonRequiredAttribute>() != null)
                    yield return property.Name; // Marked with [JsonRequired]
                else if (property.GetCustomAttribute<RequiredAttribute>() != null)
                    yield return property.Name; // Marked with [Required]
            }
        }
    }
}

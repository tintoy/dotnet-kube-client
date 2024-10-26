using KubeClient.Models;
using System.Collections.Immutable;

namespace KubeClient.Extensions.CustomResources.Schema
{
    /// <summary>
    ///     Parses <see cref="KubeSchema"/> from <see cref="JSONSchemaPropsV1"/>.
    /// </summary>
    public static class JsonSchemaParserV1
    {
        /// <summary>
        ///     Build a <see cref="KubeSchema"/> from one or more Custom Resource Definitions (CRDs).
        /// </summary>
        /// <param name="customResourceDefinitions">
        ///     The <see cref="CustomResourceDefinitionV1"/>s.
        /// </param>
        /// <returns>
        ///     A <see cref="KubeSchema"/> representing the CRDs and any related types.
        /// </returns>
        public static KubeSchema BuildKubeSchema(params CustomResourceDefinitionV1[] customResourceDefinitions) => BuildKubeSchema(customResourceDefinitions.AsEnumerable());

        /// <summary>
        ///     Build a <see cref="KubeSchema"/> from one or more Custom Resource Definitions (CRDs).
        /// </summary>
        /// <param name="customResourceDefinitions">
        ///     The <see cref="CustomResourceDefinitionV1"/>s.
        /// </param>
        /// <returns>
        ///     A <see cref="KubeSchema"/> representing the CRDs and any related types.
        /// </returns>
        public static KubeSchema BuildKubeSchema(IEnumerable<CustomResourceDefinitionV1> customResourceDefinitions)
        {
            if (customResourceDefinitions == null)
                throw new ArgumentNullException(nameof(customResourceDefinitions));

            Dictionary<string, KubeDataType> dataTypes = new Dictionary<string, KubeDataType>();
            Dictionary<KubeResourceKind, KubeModel> resourceTypes = new Dictionary<KubeResourceKind, KubeModel>();

            foreach (CustomResourceDefinitionV1 customResourceDefinition in customResourceDefinitions)
            {
                if (customResourceDefinition.Spec.Versions.Count == 0)
                    throw new KubeClientException($"Invalid custom resource definition (CRD) '{customResourceDefinition.Metadata.Name}': no versions declared.");

                CustomResourceDefinitionVersionV1 primaryVersion = customResourceDefinition.Spec.Versions[0];

                KubeResourceKind resourceType = new KubeResourceKind(Group: customResourceDefinition.Spec.Group, Version: primaryVersion.Name, ResourceKind: customResourceDefinition.Spec.Names.Kind);
                if (resourceTypes.ContainsKey(resourceType))
                    continue;

                KubeModel resourceTypeModel = ParseResourceType(resourceType, primaryVersion.Schema.OpenAPIV3Schema, dataTypes);
                resourceTypes.Add(resourceType, resourceTypeModel);
            }

            return new KubeSchema(
                ResourceTypes: ImmutableDictionary.CreateRange(resourceTypes),
                DataTypes: ImmutableDictionary.CreateRange(dataTypes)
            );
        }

        /// <summary>
        ///     Parse a resource type into a <see cref="KubeModel"/>.
        /// </summary>
        /// <param name="resourceType">
        ///     A <see cref="KubeResourceKind"/> that identifies the target resource type.
        /// </param>
        /// <param name="resourceTypeSchema">
        ///     <see cref="JSONSchemaPropsV1"/> representing the schema for the target resource type.
        /// </param>
        /// <param name="knownDataTypes">
        ///     Known data types that have already been processed (or are well-known).
        /// </param>
        /// <returns>
        ///     The new <see cref="KubeModel"/>.
        /// </returns>
        static KubeModel ParseResourceType(KubeResourceKind resourceType, JSONSchemaPropsV1 resourceTypeSchema, Dictionary<string, KubeDataType> knownDataTypes)
        {
            if (resourceTypeSchema == null)
                throw new ArgumentNullException(nameof(resourceTypeSchema));

            if (knownDataTypes == null)
                throw new ArgumentNullException(nameof(knownDataTypes));

            if (resourceTypeSchema.Type != "object")
                throw new InvalidOperationException("Invalid resource-type schema ('type' is not 'object').");

            Stack<string> propertyPathSegments = new Stack<string>();

            Dictionary<string, KubeModelProperty> modelProperties = new Dictionary<string, KubeModelProperty>();
            foreach ((string jsonPropertyName, JSONSchemaPropsV1 propertySchema) in resourceTypeSchema.Properties)
            {
                propertyPathSegments.Push(jsonPropertyName);

                KubeDataType propertyDataType = ParseDataType(resourceType, propertyPathSegments, propertySchema, knownDataTypes);

                string sanitizedPropertyName = NameWrangler.SanitizeName(jsonPropertyName);

                string[] mergeStrategies = (resourceTypeSchema.KubernetesPatchMergeStrategy ?? String.Empty).Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                modelProperties.Add(sanitizedPropertyName,
                    new KubeModelProperty(
                        sanitizedPropertyName,
                        propertySchema.Description,
                        propertyDataType, jsonPropertyName,
                        IsOptional: !resourceTypeSchema.Required.Contains(jsonPropertyName),
                        IsMerge: mergeStrategies.Contains("merge"),
                        MergeKey: resourceTypeSchema.KubernetesPatchMergeKey,
                        IsRetainKeys: mergeStrategies.Contains("retainKeys")
                    )
                );

                propertyPathSegments.Pop();
            }

            return new KubeModel(
                ResourceType: resourceType,
                Summary: resourceTypeSchema.Description ?? "Documentation is not available for this resource type.",
                Properties: ImmutableDictionary.CreateRange(modelProperties)
            );
        }

        /// <summary>
        ///     Parse a data-type schema into a <see cref="KubeDataType"/>.
        /// </summary>
        /// <param name="resourceType">
        ///     A <see cref="KubeResourceKind"/> that identifies the target resource type.
        /// </param>
        /// <param name="propertyPathSegments">
        ///     A stack of property names representing the path from the current resource type to the current data-type.
        /// </param>
        /// <param name="schema">
        ///     <see cref="JSONSchemaPropsV1"/> representing the schema for the target data type.
        /// </param>
        /// <param name="dataTypes">
        ///     Known data types that have already been processed (or are well-known).
        /// </param>
        /// <returns>
        ///     The <see cref="KubeDataType"/>.
        /// </returns>
        static KubeDataType ParseDataType(KubeResourceKind resourceType, Stack<string> propertyPathSegments, JSONSchemaPropsV1 schema, Dictionary<string, KubeDataType> dataTypes)
        {
            if (resourceType == null)
                throw new ArgumentNullException(nameof(resourceType));

            if (propertyPathSegments == null)
                throw new ArgumentNullException(nameof(propertyPathSegments));

            if (schema == null)
                throw new ArgumentNullException(nameof(schema));

            if (dataTypes == null)
                throw new ArgumentNullException(nameof(dataTypes));

            KubeDataType? dataType;

            string dataTypeName = GetDataTypeName(resourceType, propertyPathSegments);
            if (dataTypes.TryGetValue(dataTypeName, out dataType))
                return dataType;

            string typeName, typeFormat;
            if (!String.IsNullOrWhiteSpace(schema.Type))
            {
                typeName = schema.Type;
                typeFormat = schema.Format;

                switch (typeName)
                {
                    case "array":
                    {
                        JSONSchemaPropsV1 itemSchema = schema.Items;
                        KubeDataType elementDataType = ParseDataType(resourceType, propertyPathSegments, itemSchema, dataTypes);

                        dataType = new KubeArrayDataType(elementDataType);

                        return dataType;
                    }
                    case "object":
                    {
                        if (schema.Properties.Count == 0 && schema.KubernetesPreserveUnknownFields == true)
                            return KubeDynamicObjectDataType.Instance;

                        Dictionary<string, KubeModelProperty> modelProperties = new Dictionary<string, KubeModelProperty>();
                        foreach ((string jsonPropertyName, JSONSchemaPropsV1 propertySchema) in schema.Properties)
                        {
                            propertyPathSegments.Push(jsonPropertyName);

                            KubeDataType propertyDataType = ParseDataType(resourceType, propertyPathSegments, propertySchema, dataTypes);

                            string sanitizedPropertyName = NameWrangler.CapitalizeName(jsonPropertyName);

                            string[] mergeStrategies = (propertySchema.KubernetesPatchMergeStrategy ?? String.Empty).Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                            modelProperties.Add(sanitizedPropertyName,
                                new KubeModelProperty(
                                    Name: sanitizedPropertyName,
                                    Summary: propertySchema.Description,
                                    DataType: propertyDataType,
                                    SerializedName: jsonPropertyName,
                                    IsOptional: !propertySchema.Required.Contains(jsonPropertyName),
                                    IsMerge: mergeStrategies.Contains("merge"),
                                    MergeKey: propertySchema.KubernetesPatchMergeKey,
                                    IsRetainKeys: mergeStrategies.Contains("retainKeys")
                                )
                            );

                            propertyPathSegments.Pop();
                        }

                        KubeSubModel subModel = new KubeSubModel(
                            Name: dataTypeName,
                            Summary: schema.Description ?? "No description is available",
                            Properties: ImmutableDictionary.CreateRange(modelProperties)
                        );
                        KubeSubModelDataType subModelDataType = new KubeSubModelDataType(subModel);
                        dataTypes.Add(dataTypeName, subModelDataType);


                        return subModelDataType;
                    }
                    case "number":
                    {
                        if (typeFormat == "double")
                            return new KubeIntrinsicDataType("double");

                        break;
                    }
                    case "integer":
                    {
                        if (typeFormat == "int32")
                            return new KubeIntrinsicDataType("int");
                        else if (typeFormat == "int46")
                            return new KubeIntrinsicDataType("long");

                        return new KubeIntrinsicDataType("int");
                    }
                    default:
                    {
                        if (!dataTypes.TryGetValue(typeName, out KubeDataType? intrinsicDataType))
                        {
                            intrinsicDataType =  new KubeIntrinsicDataType(typeName);
                            dataTypes.Add(typeName, intrinsicDataType);
                        }
                            
                        return intrinsicDataType;
                    }
                }
            }

            if (String.IsNullOrWhiteSpace(schema.Ref))
                throw new KubeClientException($"Schema is missing '$ref'.");

            typeName = schema.Ref.Replace("#/definitions/", String.Empty);

            if (!dataTypes.TryGetValue(typeName, out dataType))
            {
                string summary = schema.Description ?? "Description not provided.";
                dataType = new KubeDataType(typeName, summary);
                dataTypes.Add(typeName, dataType);
            }

            return dataType;
        }

        /// <summary>
        ///     Detemine the name for a <see cref="KubeDataType"/> representing a complex data-type.
        /// </summary>
        /// <param name="resourceType">
        ///     A <see cref="KubeResourceKind"/> that identifies the current resource type.
        /// </param>
        /// <param name="propertyPathSegments">
        ///     A stack of property names representing the path from the current resource type to the current data-type.
        /// </param>
        /// <returns>
        ///     The data-type name.
        /// </returns>
        static string GetDataTypeName(KubeResourceKind resourceType, Stack<string> propertyPathSegments)
        {
            if (resourceType == null)
                throw new ArgumentNullException(nameof(resourceType));

            if (propertyPathSegments == null)
                throw new ArgumentNullException(nameof(propertyPathSegments));

            string prettyApiVersion = NameWrangler.CapitalizeName(resourceType.Version);

            string typeNameFromPropertyPath = String.Join(String.Empty,
                propertyPathSegments.Reverse().Select(NameWrangler.CapitalizeName)
            );

            return $"{resourceType.ResourceKind}{typeNameFromPropertyPath}{prettyApiVersion}";
        }
    }
}

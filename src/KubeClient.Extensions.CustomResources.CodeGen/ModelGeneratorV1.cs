using KubeClient.Extensions.CustomResources.Schema;
using KubeClient.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

// Some things we do, at the moment, are C#-specific.
// If we want to be language-agnostic, we will probably need to create an MSBuildWorkspace,
// load or create a real project that imports the required libraries and packages, and then get ITypeSymbols from there.
using CS = Microsoft.CodeAnalysis.CSharp.Syntax;
using CSFactory = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using CSSyntaxKind = Microsoft.CodeAnalysis.CSharp.SyntaxKind;

namespace KubeClient.Extensions.CustomResources.CodeGen
{
    /// <summary>
    ///     Code generator for <see cref="KubeResourceV1"/> models, and associated types.
    /// </summary>
    public static class ModelGeneratorV1
    {
        /// <summary>
        ///     Predefinued <see cref="SyntaxNode"/>s representing commonly-used attributes on model types and their members.
        /// </summary>
        static class Attributes
        {
            /// <summary>
            ///     Predefinued <see cref="CS.AttributeSyntax"/> nodes representing commonly-used attributes on model types and their members.
            /// </summary>
            public static class CSharp
            {
                /// <summary>
                ///     A predefined <see cref="CS.AttributeSyntax"/> node representing a <see cref="JsonExtensionDataAttribute"/> attribute on a member of a JSON-serialisable model.
                /// </summary>
                public static readonly CS.AttributeSyntax JsonExtensionData = CSFactory.Attribute(
                    name: CSFactory.ParseName("JsonExtensionData")
                );
            }
        }

        /// <summary>
        ///     Predefinued <see cref="SyntaxToken"/>s representing commonly-used declaration modifiers.
        /// </summary>
        static class Modifiers
        {
            /// <summary>
            ///     Predefinued <see cref="SyntaxToken"/>s representing commonly-used C# declaration modifiers.
            /// </summary>
            public static class CSharp
            {
                /// <summary>
                ///     A <see cref="SyntaxToken"/> node representing the C# "readonly" declaration-modifier keyword (<see cref="CSSyntaxKind.ReadOnlyKeyword"/>).
                /// </summary>
                public static readonly SyntaxToken ReadOnly = CSFactory.Token(CSSyntaxKind.ReadOnlyKeyword);
            }
        }

        /// <summary>
        ///     Predefinued <see cref="SyntaxToken"/>s representing commonly-used type references.
        /// </summary>
        static class TypeReferences
        {
            /// <summary>
            ///     Predefinued <see cref="CS.TypeSyntax"/> nodes representing commonly-used C# type references.
            /// </summary>
            public static class CSharp
            {
                /// <summary>
                ///     A predefined <see cref="CS.TypeSyntax"/> node representing a JSON extension-data dictionary (a <see cref="Dictionary{TKey, TValue}"/> mapping <see cref="String"/> to <see cref="JToken"/>).
                /// </summary>
                public static readonly CS.TypeSyntax JsonExtensionData = CSFactory.ParseTypeName("Dictionary<string, JToken>");

                /// <summary>
                ///     A predefined <see cref="CS.TypeSyntax"/> node representing the <see cref="Models.KubeResourceV1"/> base class for resource models.
                /// </summary>
                public static readonly CS.TypeSyntax KubeResourceV1 = CSFactory.ParseTypeName("KubeResourceV1");
            }
        }

        /// <summary>
        ///     Predefinued <see cref="SyntaxToken"/>s representing commonly-used field declarations.
        /// </summary>
        static class FieldDeclarations
        {
            /// <summary>
            ///     Predefinued <see cref="CS.FieldDeclarationSyntax"/> nodes representing commonly-used C# field references.
            /// </summary>
            public static class CSharp
            {
                /// <summary>
                ///     A predefined <see cref="CS.FieldDeclarationSyntax"/> node representing the "_jsonExtensionDataField" on a JSON-serialisable model.
                /// </summary>
                public static readonly CS.FieldDeclarationSyntax JsonExtensionData =
                    CSFactory.FieldDeclaration(
                        attributeLists: [
                            CSFactory.AttributeList([
                                Attributes.CSharp.JsonExtensionData
                            ])
                        ],
                        modifiers: [
                            Modifiers.CSharp.ReadOnly
                        ],
                        declaration: CSFactory.VariableDeclaration(
                            type: TypeReferences.CSharp.JsonExtensionData,
                            variables: [
                                CSFactory.VariableDeclarator("_jsonExtensionData").WithInitializer(
                                    CSFactory.EqualsValueClause(
                                        CSFactory.ObjectCreationExpression(
                                            type: TypeReferences.CSharp.JsonExtensionData,
                                            argumentList: CSFactory.ArgumentList(),
                                            initializer: null
                                        )
                                    )
                                )
                            ]
                        )
                    )
                    .WithDocumentation(
                        CSFactory.XmlSummaryElement(
                            CodeGenHelper.NewlineText,
                            CodeGenHelper.IndentedText("Unmapped JSON data (i.e. not mapped to a member of the model type) to improve round-trip behaviour when updating resources via PUT.", indent: 1),
                            CodeGenHelper.NewlineText
                        )
                    );
            }
        }

        

        /// <summary>
        ///     Generate model code for a resource type (and any related complex types) in the specified <see cref="KubeSchema"/>.
        /// </summary>
        /// <param name="schema">
        ///     A <see cref="KubeSchema"/> representing the source schema.
        /// </param>
        /// <param name="resourceType">
        ///     A <see cref="KubeResourceType"/> that identifies the target resource type.
        /// </param>
        /// <param name="project">
        ///     The <see cref="Project"/> that the generated code will be added to.
        /// </param>
        /// <param name="targetNamespace">
        ///     The target namespace for the generated code.
        /// </param>
        /// <returns>
        ///     A modified copy of the <paramref name="project"/>  (it is the caller's responsibility to call <see cref="Workspace.TryApplyChanges(Solution)"/>).
        /// </returns>
        public static Project GenerateModels(KubeSchema schema, KubeResourceType resourceType, Project project, string targetNamespace)
        {
            if (schema == null)
                throw new ArgumentNullException(nameof(schema));

            if (resourceType == null)
                throw new ArgumentNullException(nameof(resourceType));

            if (project == null)
                throw new ArgumentNullException(nameof(project));

            if (String.IsNullOrWhiteSpace(targetNamespace))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(targetNamespace)}.", nameof(targetNamespace));

            KubeModel? model;
            if (!schema.ResourceTypes.TryGetValue(resourceType, out model))
                throw new ArgumentException($"Schema does not contain any metadata for resource type '{resourceType.ToResourceTypeName()}'.", nameof(schema));

            // TODO: Work out how, if we need to at all, to group shared complex types so we can put each resource type in its own file with any complex types that are only associated with that resource.

            var complexTypes = new HashSet<KubeComplexType>();
            DiscoverComplexTypes(model, complexTypes);

            Document document = GenerateModels(project, model, complexTypes, targetNamespace);
            
            return document.Project;
        }

        /// <summary>
        ///     Generate type declarations for a resource model and its related complex types (if any).
        /// </summary>
        /// <param name="project">
        ///     The <see cref="Project"/> that the generated code's containing <see cref="Document"/> will be added to.
        /// </param>
        /// <param name="model">
        ///     A <see cref="KubeModel"/> representing the resource-model metadata.
        /// </param>
        /// <param name="complexTypes">
        ///     A sequence of 0 or more <see cref="KubeComplexType"/> representing the complex type metadata (if any).
        /// </param>
        /// <param name="targetNamespace">
        ///     The fully-qualified namespace for the generated code.
        /// </param>
        /// <returns>
        ///     A <see cref="Document"/> containing the generated code.
        /// </returns>
        static Document GenerateModels(Project project, KubeModel model, IEnumerable<KubeComplexType> complexTypes, string targetNamespace)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (complexTypes == null)
                throw new ArgumentNullException(nameof(complexTypes));

            if (project == null)
                throw new ArgumentNullException(nameof(project));

            SyntaxGenerator syntaxGenerator = SyntaxGenerator.GetGenerator(project);

            SyntaxNode generatedModel = syntaxGenerator.CompilationUnit(
                syntaxGenerator.NamespaceImport("KubeClient.Models"),
                syntaxGenerator.NamespaceImport("Newtonsoft.Json"),
                syntaxGenerator.NamespaceImport("Newtonsoft.Json.Linq"),
                syntaxGenerator.NamespaceImport("System"),
                syntaxGenerator.NamespaceImport("System.Collections.Generic"),
                syntaxGenerator.NamespaceImport("YamlDotNet.Serialization"),
                syntaxGenerator.Namespace(targetNamespace,
                    declarations: 
                        GenerateResourceDeclarations([model], syntaxGenerator).Select(
                            classDeclaration => classDeclaration.WithTrailingNewline()
                        ).Concat(
                            GenerateComplexTypeDeclarations(complexTypes, syntaxGenerator).Select(
                                classDeclaration => classDeclaration.WithTrailingNewline()
                            )
                        )
                )
            );

            return project.AddDocument($"{model.ClrTypeName}.cs", generatedModel);
        }

        /// <summary>
        ///     Generate type declarations for resource models.
        /// </summary>
        /// <param name="models">
        ///     A sequence of <see cref="KubeModel"/>s representing the resource models.
        /// </param>
        /// <param name="syntaxGenerator">
        ///     The current (language-specific) <see cref="SyntaxGenerator"/> used to generate code.
        /// </param>
        /// <returns>
        ///     A sequence of corresponding <see cref="SyntaxNode"/>s representing the resource type declarations.
        /// </returns>
        static IEnumerable<SyntaxNode> GenerateResourceDeclarations(IEnumerable<KubeModel> models, SyntaxGenerator syntaxGenerator)
        {
            if (models == null)
                throw new ArgumentNullException(nameof(models));

            if (syntaxGenerator == null)
                throw new ArgumentNullException(nameof(syntaxGenerator));

            foreach (KubeModel model in models.OrderBy(model => model.ClrTypeName))
            {
                yield return
                    syntaxGenerator.AddAttributes(
                        declaration: syntaxGenerator.ClassDeclaration(
                            name: model.ClrTypeName,
                            baseType: TypeReferences.CSharp.KubeResourceV1,
                            accessibility: Accessibility.Public,
                            modifiers: DeclarationModifiers.Partial,
                            members: [
                                FieldDeclarations.CSharp.JsonExtensionData.WithTrailingNewline(),

                                .. GenerateProperties(model.Properties, syntaxGenerator)
                            ]
                        ),
                        attributes: [
                            syntaxGenerator.Attribute("KubeObject", attributeArguments: [
                                syntaxGenerator.LiteralExpression(model.ResourceType.ResourceKind), // kind
                                syntaxGenerator.LiteralExpression($"{model.ResourceType.Group}/{model.ResourceType.Version}") // groupVersion
                            ]),

                            .. model.ResourceApis.PrimaryApi.SupportedVerbs.Select(
                                verb => syntaxGenerator.Attribute("KubeApi", attributeArguments: [
                                    syntaxGenerator.MemberAccessExpression(
                                        CSFactory.ParseTypeName(nameof(KubeAction)),
                                        CSFactory.IdentifierName(verb.KubeAction.ToString())
                                    ),
                                    syntaxGenerator.LiteralExpression(model.ResourceApis.PrimaryApi.Path)
                                ])
                            )
                        ]
                    )
                    .WithDocumentation(
                        CSFactory.XmlSummaryElement(
                            CodeGenHelper.NewlineText,
                            CodeGenHelper.IndentedText(model.Summary ?? "No description is available.", indent: 1),
                            CodeGenHelper.NewlineText
                        )
                    )
                    .WithTrailingNewline();
            }
        }

        /// <summary>
        ///     Generate type declarations for complex type models.
        /// </summary>
        /// <param name="complexTypes">
        ///     A sequence of <see cref="KubeComplexType"/>s representing the complex types.
        /// </param>
        /// <param name="syntaxGenerator">
        ///     The current (language-specific) <see cref="SyntaxGenerator"/> used to generate code.
        /// </param>
        /// <returns>
        ///     A sequence of corresponding <see cref="SyntaxNode"/>s representing the complex type declarations.
        /// </returns>
        static IEnumerable<SyntaxNode> GenerateComplexTypeDeclarations(IEnumerable<KubeComplexType> complexTypes, SyntaxGenerator syntaxGenerator)
        {
            if (syntaxGenerator == null)
                throw new ArgumentNullException(nameof(syntaxGenerator));

            if (complexTypes == null)
                throw new ArgumentNullException(nameof(complexTypes));

            foreach (KubeComplexType complexType in complexTypes.OrderBy(complexType => complexType.ClrTypeName))
            {
                yield return syntaxGenerator
                    .ClassDeclaration(
                        complexType.ClrTypeName,
                        accessibility: Accessibility.Public,
                        modifiers: DeclarationModifiers.Partial,
                        members: [
                            FieldDeclarations.CSharp.JsonExtensionData.WithTrailingNewline(),

                            .. GenerateProperties(complexType.Properties, syntaxGenerator)
                        ]
                    )
                    .WithDocumentation(
                        CSFactory.XmlSummaryElement(
                            CodeGenHelper.NewlineText,
                            CodeGenHelper.IndentedText(complexType.Summary ?? "No description is available.", indent: 1),
                            CodeGenHelper.NewlineText
                        )
                    )
                    .WithTrailingNewline();
            }
        }

        /// <summary>
        ///     Generate property declarations for model properties.
        /// </summary>
        /// <param name="properties">
        ///     A sequence of key-value pairs, each representing a property name and corresponding <see cref="KubeModelProperty"/>.
        /// </param>
        /// <param name="syntaxGenerator">
        ///     The current (language-specific) <see cref="SyntaxGenerator"/> used to generate code.
        /// </param>
        /// <returns>
        ///     A sequence of corresponding <see cref="SyntaxNode"/>s representing the property declarations.
        /// </returns>
        static IEnumerable<SyntaxNode> GenerateProperties(IEnumerable<KeyValuePair<string, KubeModelProperty>> properties, SyntaxGenerator syntaxGenerator)
        {
            if (properties == null)
                throw new ArgumentNullException(nameof(properties));
            
            if (syntaxGenerator == null)
                throw new ArgumentNullException(nameof(syntaxGenerator));

            foreach ((string propertyName, KubeModelProperty property) in properties.OrderBy(item => item.Key))
            {
                SyntaxNode propertyType = property.DataType switch
                {
                    KubeIntrinsicDataType intrinsicDataType => GetTypeReference(intrinsicDataType, syntaxGenerator),
                    KubeDataType dataType => GetDataType(dataType, syntaxGenerator),
                };

                SyntaxNode propertyDeclaration = syntaxGenerator.PropertyDeclaration(propertyName,
                    type: propertyType,
                    accessibility: Accessibility.Public
                );

                propertyDeclaration = syntaxGenerator.AddAttributes(propertyDeclaration,
                    // [YamlMember(Alias = "myProperty")]
                    syntaxGenerator.Attribute("YamlMember",
                        syntaxGenerator.AttributeArgument("Alias",
                            syntaxGenerator.LiteralExpression(property.SerializedName)
                        )
                    ),

                    // [JsonProperty("myProperty")]
                    syntaxGenerator.Attribute("JsonProperty",
                        syntaxGenerator.AttributeArgument(
                            syntaxGenerator.LiteralExpression(property.SerializedName)
                        )
                    )
                );

                propertyDeclaration = propertyDeclaration.WithDocumentation(
                    CSFactory.XmlSummaryElement(
                        CodeGenHelper.NewlineText,
                        CodeGenHelper.IndentedText(property.Summary ?? "No description is available.", indent: 1),
                        CodeGenHelper.NewlineText
                    )
                );

                yield return propertyDeclaration.WithTrailingNewline();
            }
        }

        /// <summary>
        ///     Get a type reference corresponding to the specified intrinsic data-type.
        /// </summary>
        /// <param name="intrinsicDataType">
        ///     A <see cref="KubeIntrinsicDataType"/> representing the intrinsic data-type.
        /// </param>
        /// <param name="syntaxGenerator">
        ///     The current (language-specific) <see cref="SyntaxGenerator"/> used to generate code.
        /// </param>
        /// <returns>
        ///     A <see cref="SyntaxNode"/> representing the type reference.
        /// </returns>
        static SyntaxNode GetTypeReference(KubeIntrinsicDataType intrinsicDataType, SyntaxGenerator syntaxGenerator)
        {
            if (intrinsicDataType == null)
                throw new ArgumentNullException(nameof(intrinsicDataType));

            if (syntaxGenerator == null)
                throw new ArgumentNullException(nameof(syntaxGenerator));

            SpecialType intrinsicType = intrinsicDataType.Name switch
            {
                "int" => SpecialType.System_Int32,
                "double" => SpecialType.System_Double,
                "long" => SpecialType.System_Int64,
                "string" => SpecialType.System_String,

                _ => SpecialType.None
            };

            if (intrinsicType != SpecialType.None)
                return syntaxGenerator.TypeExpression(intrinsicType);

            // TODO: This won't work for any language other than C#; find a non-broken way to handle it!
            //       Will probably need to create an MSBuildWorkspace, load or create a real project that imports the required libraries and packages (then get ITypeSymbols from there).
            return CSFactory.ParseTypeName(
                intrinsicDataType.GetClrTypeName()
            );
        }

        /// <summary>
        ///     Get a type reference corresponding to the specified data-type.
        /// </summary>
        /// <param name="dataType">
        ///     A <see cref="KubeDataType"/> representing the data-type.
        /// </param>
        /// <param name="syntaxGenerator">
        ///     The current (language-specific) <see cref="SyntaxGenerator"/> used to generate code.
        /// </param>
        /// <returns>
        ///     A <see cref="SyntaxNode"/> representing the type reference.
        /// </returns>
        static SyntaxNode GetDataType(KubeDataType dataType, SyntaxGenerator syntaxGenerator)
        {
            if (dataType == null)
                throw new ArgumentNullException(nameof(dataType));

            if (syntaxGenerator == null)
                throw new ArgumentNullException(nameof(syntaxGenerator));

            // TODO: This won't work for any language other than C#; find a non-broken way to handle it!
            //       Will probably need to create an MSBuildWorkspace, load or create a real project that imports the required libraries and packages (then get ITypeSymbols from there).
            return CSFactory.ParseTypeName(
                dataType.GetClrTypeName()
            );
        }

        /// <summary>
        ///     Recursively discover complex types referenced by a Kubernetes resource type.
        /// </summary>
        /// <param name="containingModel">
        ///     A <see cref="KubeModel"/> representing the target resource type.
        /// </param>
        /// <param name="complexTypes">
        ///     A set of <see cref="KubeComplexType"/>s representing all discovered complex types.
        /// </param>
        static void DiscoverComplexTypes(KubeModel containingModel, HashSet<KubeComplexType> complexTypes)
        {
            if (containingModel == null)
                throw new ArgumentNullException(nameof(containingModel));

            if (complexTypes == null)
                throw new ArgumentNullException(nameof(complexTypes));

            foreach (KubeModelProperty property in containingModel.Properties.Values)
            {
                KubeComplexType? complexType;
                if (!TryGetComplexType(property.DataType, out complexType))
                    continue;

                if (!complexTypes.Add(complexType))
                    continue; // Already processed.

                DiscoverComplexTypes(complexType, complexTypes);
            }
        }

        /// <summary>
        ///     Recursively discover complex types referenced by a Kubernetes complex type.
        /// </summary>
        /// <param name="containingType">
        ///     A <see cref="KubeComplexType"/> representing the target complex type.
        /// </param>
        /// <param name="complexTypes">
        ///     A set of <see cref="KubeComplexType"/>s representing all discovered complex types.
        /// </param>
        static void DiscoverComplexTypes(KubeComplexType containingType, HashSet<KubeComplexType> complexTypes)
        {
            if (containingType == null)
                throw new ArgumentNullException(nameof(containingType));

            if (complexTypes == null)
                throw new ArgumentNullException(nameof(complexTypes));

            foreach (KubeModelProperty property in containingType.Properties.Values)
            {
                KubeComplexType? complexType;
                if (!TryGetComplexType(property.DataType, out complexType))
                    continue;

                if (!complexTypes.Add(complexType))
                    continue; // Already processed.

                // Recurse.
                DiscoverComplexTypes(complexType, complexTypes);
            }
        }

        /// <summary>
        ///     Attempt to resolve a <see cref="KubeComplexType"/> from a <see cref="KubeDataType"/>.
        /// </summary>
        /// <param name="dataType">
        ///     The <see cref="KubeDataType"/> to inspect.
        /// </param>
        /// <param name="complexType">
        ///     If successful, receives the <see cref="KubeComplexType"/> (otherwise, <c>null</c>).
        /// </param>
        /// <returns>
        ///     <c>true</c>, if successful; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        ///     Recurses into element types for array / dictionary data types.
        /// </remarks>
        static bool TryGetComplexType(KubeDataType dataType, [NotNullWhen(returnValue: true)] out KubeComplexType? complexType)
        {
            if (dataType == null)
                throw new ArgumentNullException(nameof(dataType));

            complexType = null;

            if (dataType is KubeArrayDataType arrayDataType && arrayDataType.ElementType is KubeComplexDataType arrayElementDataType)
                complexType = arrayElementDataType.ComplexType;
            else if (dataType is KubeDictionaryDataType dictionaryDataType && dictionaryDataType.ElementType is KubeComplexDataType dictionaryElementDataType)
                complexType = dictionaryElementDataType.ComplexType;
            else if (dataType is KubeComplexDataType complexDataType)
                complexType = complexDataType.ComplexType;

            return complexType != null;
        }
    }
}

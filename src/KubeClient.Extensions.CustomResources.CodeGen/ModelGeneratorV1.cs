using KubeClient.Extensions.CustomResources.Schema;
using KubeClient.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using System;
using System.Collections.Generic;
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
        static class Attributes
        {
            public static class CSharp
            {
                public static readonly CS.AttributeSyntax JsonExtensionData = CSFactory.Attribute(
                    name: CSFactory.ParseName("JsonExtensionData")
                );
            }
        }

        static class Modifiers
        { 
            public static class CSharp
            {
                public static readonly SyntaxToken ReadOnly = CSFactory.Token(CSSyntaxKind.ReadOnlyKeyword);
            }
        }

        static class FieldTypes
        {
            public static class CSharp
            {
                public static readonly CS.TypeSyntax JsonExtensionData = CSFactory.ParseTypeName("Dictionary<string, JToken>");
            }
        }

        static class FieldDeclarations
        {
            public static class CSharp
            {
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
                            type: FieldTypes.CSharp.JsonExtensionData,
                            variables: [
                                CSFactory.VariableDeclarator("_jsonExtensionData").WithInitializer(
                                    CSFactory.EqualsValueClause(
                                        CSFactory.ObjectCreationExpression(
                                            type: FieldTypes.CSharp.JsonExtensionData,
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
        ///     A <see cref="KubeResourceKind"/> that identifies the target resource type.
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
        public static Project GenerateModels(KubeSchema schema, KubeResourceKind resourceType, Project project, string targetNamespace)
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

            var subModels = new HashSet<KubeSubModel>();
            DiscoverComplexTypes(model, subModels);

            Document document = GenerateModels(project, model, subModels, targetNamespace);
            
            return document.Project;
        }

        static Document GenerateModels(Project project, KubeModel model, IEnumerable<KubeSubModel> subModels, string targetNamespace)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (subModels == null)
                throw new ArgumentNullException(nameof(subModels));

            if (project == null)
                throw new ArgumentNullException(nameof(project));

            SyntaxGenerator syntaxGenerator = SyntaxGenerator.GetGenerator(project);

            SyntaxNode generatedModel = syntaxGenerator.CompilationUnit(
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
                            GenerateComplexTypeDeclarations(subModels, syntaxGenerator).Select(
                                classDeclaration => classDeclaration.WithTrailingNewline()
                            )
                        )
                )
            );

            return project.AddDocument($"{model.ClrTypeName}.cs", generatedModel);
        }

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
                            model.ClrTypeName,
                            accessibility: Accessibility.Public,
                            modifiers: DeclarationModifiers.Partial,
                            members: [
                                FieldDeclarations.CSharp.JsonExtensionData.WithTrailingNewline(),

                                .. GenerateProperties(model.Properties, syntaxGenerator)
                            ]
                        ),
                        attributes: model.ResourceApis.PrimaryApi.SupportedVerbs.Select(
                            verb => syntaxGenerator.Attribute("KubeApi", attributeArguments: [
                                syntaxGenerator.MemberAccessExpression(
                                    CSFactory.ParseTypeName(nameof(KubeAction)),
                                    CSFactory.IdentifierName(verb.KubeAction.ToString())
                                ),
                                syntaxGenerator.LiteralExpression(model.ResourceApis.PrimaryApi.Path)
                            ])
                        )
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

        static IEnumerable<SyntaxNode> GenerateComplexTypeDeclarations(IEnumerable<KubeSubModel> subModels, SyntaxGenerator syntaxGenerator)
        {
            if (syntaxGenerator == null)
                throw new ArgumentNullException(nameof(syntaxGenerator));

            if (subModels == null)
                throw new ArgumentNullException(nameof(subModels));

            foreach (KubeSubModel subModel in subModels.OrderBy(subModel => subModel.ClrTypeName))
            {
                yield return syntaxGenerator
                    .ClassDeclaration(
                        subModel.ClrTypeName,
                        accessibility: Accessibility.Public,
                        modifiers: DeclarationModifiers.Partial,
                        members: [
                            FieldDeclarations.CSharp.JsonExtensionData.WithTrailingNewline(),

                            .. GenerateProperties(subModel.Properties, syntaxGenerator)
                        ]
                    )
                    .WithDocumentation(
                        CSFactory.XmlSummaryElement(
                            CodeGenHelper.NewlineText,
                            CodeGenHelper.IndentedText(subModel.Summary ?? "No description is available.", indent: 1),
                            CodeGenHelper.NewlineText
                        )
                    )
                    .WithTrailingNewline();
            }
        }

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
                    KubeIntrinsicDataType intrinsicDataType => GetDataType(intrinsicDataType, syntaxGenerator),
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
                        ),
                        syntaxGenerator.AttributeArgument("Alias",
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

        static SyntaxNode GetDataType(KubeIntrinsicDataType intrinsicDataType, SyntaxGenerator syntaxGenerator)
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

        static void DiscoverComplexTypes(KubeModel containingModel, HashSet<KubeSubModel> complexTypeModels)
        {
            if (containingModel == null)
                throw new ArgumentNullException(nameof(containingModel));

            if (complexTypeModels == null)
                throw new ArgumentNullException(nameof(complexTypeModels));

            foreach (KubeModelProperty property in containingModel.Properties.Values)
            {
                KubeSubModelDataType? complexType = property.DataType as KubeSubModelDataType;
                if (complexType is null)
                    continue;

                if (!complexTypeModels.Add(complexType.SubModel))
                    continue;

                DiscoverComplexTypes(complexType.SubModel, complexTypeModels);
            }
        }

        static void DiscoverComplexTypes(KubeSubModel containingModel, HashSet<KubeSubModel> complexTypeModels)
        {
            if (containingModel == null)
                throw new ArgumentNullException(nameof(containingModel));

            if (complexTypeModels == null)
                throw new ArgumentNullException(nameof(complexTypeModels));

            foreach (KubeModelProperty property in containingModel.Properties.Values)
            {
                KubeSubModelDataType? complexType = property.DataType as KubeSubModelDataType;
                if (complexType is null)
                    continue;

                if (!complexTypeModels.Add(complexType.SubModel))
                    continue;

                // Recurse.
                DiscoverComplexTypes(complexType.SubModel, complexTypeModels);
            }
        }
    }
}

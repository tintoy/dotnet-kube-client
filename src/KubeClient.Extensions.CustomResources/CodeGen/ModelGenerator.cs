using KubeClient.Extensions.CustomResources.Schema;
using KubeClient.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Host.Mef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

// Some things we do, at the moment, are C#-specific.
// If we want to be language-agnostic, we will probably need to create an MSBuildWorkspace,
// load or create a real project that imports the required libraries and packages, and then get ITypeSymbols from there.
using CSFactory = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace KubeClient.Extensions.CustomResources.CodeGen
{
    /// <summary>
    ///     Code generator for <see cref="KubeResourceV1"/> models, and associated types.
    /// </summary>
    public static class ModelGenerator
    {
        public static Project GenerateModels(KubeSchema schema, KubeResourceKind resourceType, Project project, string @namespace)
        {
            if (schema == null)
                throw new ArgumentNullException(nameof(schema));

            if (resourceType == null)
                throw new ArgumentNullException(nameof(resourceType));

            if (project == null)
                throw new ArgumentNullException(nameof(project));

            if (String.IsNullOrWhiteSpace(@namespace))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(@namespace)}.", nameof(@namespace));

            KubeModel? model;
            if (!schema.ResourceTypes.TryGetValue(resourceType, out model))
                throw new ArgumentException($"Schema does not contain any metadata for resource type '{resourceType.ToResourceTypeName()}'.", nameof(schema));

            // TODO: Recursively walk the model's properties and their data types to determine the set of sub-models to include here. See if there's an elegant way to handle shared models.

            string resourceKindPrefix = resourceType.ResourceKind;
            string versionSuffix = resourceType.ToVersionSuffix();
            KubeSubModelDataType[] subModelDataTypes = schema.DataTypes.Values
                .OfType<KubeSubModelDataType>()
                .Where(subModel =>
                    subModel.Name.StartsWith(resourceKindPrefix)
                    &&
                    subModel.Name.EndsWith(versionSuffix)
                )
                .ToArray();

            Document document = GenerateModels(
                project,
                model,
                subModelDataTypes.Select(subModelDataType => subModelDataType.SubModel),
                @namespace: @namespace
            );
            
            return document.Project;
        }

        static Document GenerateModel(Project project, KubeSubModel subModel, string @namespace)
        {
            if (subModel == null)
                throw new ArgumentNullException(nameof(subModel));

            if (project == null)
                throw new ArgumentNullException(nameof(project));

            SyntaxGenerator syntaxGenerator = SyntaxGenerator.GetGenerator(project);

            SyntaxNode generatedModel = syntaxGenerator.CompilationUnit(
                syntaxGenerator.NamespaceImport("Newtonsoft.Json"),
                syntaxGenerator.NamespaceImport("System"),
                syntaxGenerator.NamespaceImport("System.Collections.Generic"),
                syntaxGenerator.NamespaceImport("YamlDotNet.Serialization"),
                syntaxGenerator.Namespace(@namespace,
                    declarations: [
                        syntaxGenerator.ClassDeclaration(subModel.Name, accessibility: Accessibility.Public, members: [
                            // TODO: Generate properties.
                            syntaxGenerator.PropertyDeclaration("Property",
                                type: syntaxGenerator.TypeExpression(SpecialType.System_Boolean),
                                accessibility: Accessibility.Public
                            )
                            .WithDocumentation(
                                CSFactory.XmlSummaryElement(
                                    RoslynHelper.NewlineText,
                                    RoslynHelper.IndentedText("My property.", indent: 1),
                                    RoslynHelper.NewlineText
                                )
                            )
                            .WithTrailingNewline()
                        ])
                        .WithDocumentation(
                            CSFactory.XmlSummaryElement(
                                RoslynHelper.NewlineText,
                                RoslynHelper.IndentedText(subModel.Summary ?? "No description is available.", indent: 1),
                                RoslynHelper.NewlineText
                            )
                        )
                        .WithTrailingNewline()
                    ]
                )
            );

            return project.AddDocument($"{subModel.Name}.cs", generatedModel);
        }

        static Document GenerateModels(Project project, KubeModel model, IEnumerable<KubeSubModel> subModels, string @namespace)
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
                syntaxGenerator.Namespace(@namespace,
                    declarations: 
                        GenerateClassDeclarations([model], syntaxGenerator).Select(
                            classDeclaration => classDeclaration.WithTrailingNewline()
                        ).Concat(
                            GenerateClassDeclarations(subModels, syntaxGenerator).Select(
                                classDeclaration => classDeclaration.WithTrailingNewline()
                            )
                        )
                )
            );

            return project.AddDocument($"{model.ClrTypeName}.cs", generatedModel);
        }

        static IEnumerable<SyntaxNode> GenerateClassDeclarations(IEnumerable<KubeModel> models, SyntaxGenerator syntaxGenerator)
        {
            if (models == null)
                throw new ArgumentNullException(nameof(models));

            if (syntaxGenerator == null)
                throw new ArgumentNullException(nameof(syntaxGenerator));

            foreach (KubeModel model in models)
            {
                yield return syntaxGenerator
                    .ClassDeclaration(
                        model.ClrTypeName,
                        accessibility: Accessibility.Public,
                        members: GenerateProperties(model.Properties, syntaxGenerator)
                    )
                    .WithDocumentation(
                        CSFactory.XmlSummaryElement(
                            RoslynHelper.NewlineText,
                            RoslynHelper.IndentedText(model.Summary ?? "No description is available.", indent: 1),
                            RoslynHelper.NewlineText
                        )
                    )
                    .WithTrailingNewline();
            }
        }

        static IEnumerable<SyntaxNode> GenerateClassDeclarations(IEnumerable<KubeSubModel> subModels, SyntaxGenerator syntaxGenerator)
        {
            if (syntaxGenerator == null)
                throw new ArgumentNullException(nameof(syntaxGenerator));

            if (subModels == null)
                throw new ArgumentNullException(nameof(subModels));

            foreach (KubeSubModel subModel in subModels)
            {
                yield return syntaxGenerator
                    .ClassDeclaration(
                        subModel.ClrTypeName,
                        accessibility: Accessibility.Public,
                        members: GenerateProperties(subModel.Properties, syntaxGenerator)
                    )
                    .WithDocumentation(
                        CSFactory.XmlSummaryElement(
                            RoslynHelper.NewlineText,
                            RoslynHelper.IndentedText(subModel.Summary ?? "No description is available.", indent: 1),
                            RoslynHelper.NewlineText
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
                        RoslynHelper.NewlineText,
                        RoslynHelper.IndentedText(property.Summary ?? "No description is available.", indent: 1),
                        RoslynHelper.NewlineText
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
    }

    static class RoslynHelper
    {
        public static readonly SyntaxList<XmlNodeSyntax> EmptyXmlNodeList = CSFactory.List<XmlNodeSyntax>();

        public static readonly XmlTextSyntax NewlineText = CSFactory.XmlText(CSFactory.TokenList(
            CSFactory.XmlTextNewLine("\n")
        ));

        public static string IndentString(int indent = 0) => new String(' ', indent * 4);

        public static XmlTextSyntax IndentText(int indent = 0)
        {
            return CSFactory.XmlText(
                IndentString(indent)
            );
        }

        public static XmlTextSyntax IndentedText(string text, int indent = 0)
        {
            StringBuilder textBuilder = new StringBuilder()
                .Append(
                    IndentString(indent)
                )
                .Append(text);

            return CSFactory.XmlText(
                textBuilder.ToString()
            );
        }

        public static string GetLanguageName(this SyntaxGenerator syntaxGenerator)
        {
            if (syntaxGenerator == null)
                throw new ArgumentNullException(nameof(syntaxGenerator));

            string serviceType = typeof(SyntaxGenerator).FullName!;

            Type generatorType = syntaxGenerator.GetType();
            ExportLanguageServiceAttribute? exportLanguageServiceAttribute = generatorType
                .GetCustomAttributes<ExportLanguageServiceAttribute>()
                .FirstOrDefault(
                    attribute => attribute.ServiceType.StartsWith(serviceType)
                );
            if (exportLanguageServiceAttribute != null)
                return exportLanguageServiceAttribute.Language;

            throw new NotSupportedException($"Cannot determine language name for SyntaxGenerator: '{generatorType.FullName}'.");
        }

        public static TSyntax WithLeadingNewline<TSyntax>(this TSyntax syntaxNode)
            where TSyntax : SyntaxNode
        {
            if (syntaxNode == null)
                throw new ArgumentNullException(nameof(syntaxNode));

            return syntaxNode.WithLeadingTrivia(
                syntaxNode.GetLeadingTrivia().Add(CSFactory.LineFeed)
            );
        }

        public static TSyntax WithTrailingNewline<TSyntax>(this TSyntax syntaxNode)
            where TSyntax : SyntaxNode
        {
            if (syntaxNode == null)
                throw new ArgumentNullException(nameof(syntaxNode));

            return syntaxNode.WithTrailingTrivia(
                syntaxNode.GetTrailingTrivia().Add(CSFactory.LineFeed)
            );
        }

        public static SyntaxNode WithTrailingSpace(this SyntaxNode syntaxNode)
        {
            if (syntaxNode == null)
                throw new ArgumentNullException(nameof(syntaxNode));

            return syntaxNode.WithTrailingTrivia(
                syntaxNode.GetTrailingTrivia().Add(CSFactory.Space)
            );
        }

        public static TSyntax WithIndent<TSyntax>(this TSyntax node, int indent)
            where TSyntax : SyntaxNode
        {
            return node.WithLeadingTrivia(
                node.GetLeadingTrivia().Add(
                    CSFactory.Whitespace(
                        IndentString(indent)
                    )
                )
            );
        }

        public static SyntaxNode NamespaceImport(this SyntaxGenerator generator, string @namespace)
        {
            if (generator == null)
                throw new ArgumentNullException(nameof(generator));

            if (String.IsNullOrWhiteSpace(@namespace))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(@namespace)}.", nameof(@namespace));

            return generator
                .NamespaceImportDeclaration(
                    name: generator.DottedName(@namespace)
                )
                .NormalizeWhitespace();
        }

        public static SyntaxNode Namespace(this SyntaxGenerator generator, string @namespace, params SyntaxNode[] declarations)
        {
            if (generator == null)
                throw new ArgumentNullException(nameof(generator));

            if (String.IsNullOrWhiteSpace(@namespace))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(@namespace)}.", nameof(@namespace));

            return generator.Namespace(
                @namespace,
                declarations.AsEnumerable()
            );
        }

        public static SyntaxNode Namespace(this SyntaxGenerator generator, string @namespace, IEnumerable<SyntaxNode> declarations)
        {
            if (generator == null)
                throw new ArgumentNullException(nameof(generator));

            if (String.IsNullOrWhiteSpace(@namespace))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(@namespace)}.", nameof(@namespace));

            if (declarations == null)
                throw new ArgumentNullException(nameof(declarations));

            return generator
                .NamespaceDeclaration(
                    name: generator.DottedName(@namespace),
                    declarations: declarations
                )
                .WithLeadingNewline();
        }

        public static TSyntax WithDocumentation<TSyntax>(this TSyntax syntaxNode, params XmlNodeSyntax[] content)
            where TSyntax : SyntaxNode
        {
            if (syntaxNode == null)
                throw new ArgumentNullException(nameof(syntaxNode));

            if (content == null)
                throw new ArgumentNullException(nameof(content));

            return syntaxNode.WithDocumentation(
                content.AsEnumerable()
            );
        }

        public static TSyntax WithDocumentation<TSyntax>(this TSyntax syntaxNode, IEnumerable<XmlNodeSyntax> content)
            where TSyntax : SyntaxNode
        {
            if (syntaxNode == null)
                throw new ArgumentNullException(nameof(syntaxNode));

            if (content == null)
                throw new ArgumentNullException(nameof(content));

            return syntaxNode.WithoutLeadingTrivia().WithLeadingTrivia(
                CSFactory.Trivia(
                    CSFactory.DocumentationCommentTrivia(
                        kind: SyntaxKind.SingleLineDocumentationCommentTrivia,
                        content: CSFactory.List(content)
                    )
                    .WithLeadingTrivia(
                        CSFactory.DocumentationCommentExterior("/// ")
                    )
                    .WithoutTrailingTrivia()
                    .WithTrailingNewline()
                )
            );
        }

        public static XmlElementSyntax ListElement(this SyntaxGenerator generator, string type, int indent, params string[] itemContent)
        {
            if (generator == null)
                throw new ArgumentNullException(nameof(generator));

            if (String.IsNullOrWhiteSpace(type))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(type)}.", nameof(type));

            return generator.ListElement(type, indent,
                itemContent.Select(CSFactory.XmlText)
            );
        }

        public static XmlElementSyntax ListElement(this SyntaxGenerator generator, string type, int indent, params XmlTextSyntax[] itemContent)
        {
            if (generator == null)
                throw new ArgumentNullException(nameof(generator));

            if (String.IsNullOrWhiteSpace(type))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(type)}.", nameof(type));

            return generator.ListElement(type, indent,
                itemContent.AsEnumerable()
            );
        }

        public static XmlElementSyntax ListElement(this SyntaxGenerator generator, string type, int indent, IEnumerable<XmlNodeSyntax> itemContent)
        {
            if (generator == null)
                throw new ArgumentNullException(nameof(generator));

            if (String.IsNullOrWhiteSpace(type))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(type)}.", nameof(type));

            if (itemContent == null)
                throw new ArgumentNullException(nameof(itemContent));

            SyntaxList<XmlNodeSyntax> listContent = CSFactory.List<XmlNodeSyntax>()
                .Add(NewlineText)
                .AddRange(
                    itemContent.SelectMany<XmlNodeSyntax, XmlNodeSyntax>(itemContent => [
                        CSFactory.XmlElement("item", [ itemContent ])
                            .WithIndent(indent + 1),
                        NewlineText,
                    ])
                );

            return CSFactory
                .XmlMultiLineElement("list", listContent)
                    .AddStartTagAttributes(
                        CSFactory.XmlTextAttribute("type", type)
                    )
                    .WithEndTag(
                        CSFactory.XmlElementEndTag(
                            CSFactory.XmlName("list")
                        )
                        .WithIndent(indent)
                    )
                    .WithIndent(indent);
        }
    }
}

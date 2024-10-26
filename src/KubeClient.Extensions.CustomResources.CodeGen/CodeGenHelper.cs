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

// Some things we do, at the moment, are C#-specific.
// If we want to be language-agnostic, we will probably need to create an MSBuildWorkspace,
// load or create a real project that imports the required libraries and packages, and then get ITypeSymbols from there.
using CSFactory = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace KubeClient.Extensions.CustomResources.CodeGen
{
    /// <summary>
    ///     Helper methods for code generation with Microsoft Roslyn.
    /// </summary>
    static class CodeGenHelper
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

        public static SyntaxNode NamespaceImport(this SyntaxGenerator generator, string namespaceName)
        {
            if (generator == null)
                throw new ArgumentNullException(nameof(generator));

            if (String.IsNullOrWhiteSpace(namespaceName))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(namespaceName)}.", nameof(namespaceName));

            return generator
                .NamespaceImportDeclaration(
                    name: generator.DottedName(namespaceName)
                )
                .NormalizeWhitespace();
        }

        public static SyntaxNode Namespace(this SyntaxGenerator generator, string namespaceName, params SyntaxNode[] declarations)
        {
            if (generator == null)
                throw new ArgumentNullException(nameof(generator));

            if (String.IsNullOrWhiteSpace(namespaceName))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(namespaceName)}.", nameof(namespaceName));

            return generator.Namespace(
                namespaceName,
                declarations.AsEnumerable()
            );
        }

        public static SyntaxNode Namespace(this SyntaxGenerator generator, string namespaceName, IEnumerable<SyntaxNode> declarations)
        {
            if (generator == null)
                throw new ArgumentNullException(nameof(generator));

            if (String.IsNullOrWhiteSpace(namespaceName))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(namespaceName)}.", nameof(namespaceName));

            if (declarations == null)
                throw new ArgumentNullException(nameof(declarations));

            return generator
                .NamespaceDeclaration(
                    name: generator.DottedName(namespaceName),
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

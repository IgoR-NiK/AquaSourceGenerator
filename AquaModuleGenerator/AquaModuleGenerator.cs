using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AquaModuleGenerator
{
    [Generator]
    public class AquaModuleGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
        }
        
        public void Execute(GeneratorExecutionContext context)
        {
            var iResolvable = context.Compilation.GetTypeByMetadataName("Core.IResolvable");

            var allResolvable = context.Compilation.SyntaxTrees
                .Select(it => new
                {
                    Semantic = context.Compilation.GetSemanticModel(it),
                    DescendantNodes = it.GetRoot()
                        .DescendantNodes()
                        .Where(node => node is InterfaceDeclarationSyntax or ClassDeclarationSyntax)
                })
                .Where(it => it.DescendantNodes.Any())
                .SelectMany(it => it.DescendantNodes
                    .Select(node => new
                    {
                        Node = node,
                        Symbol = it.Semantic.GetDeclaredSymbol(node) as INamedTypeSymbol
                    })
                    .Where(pair => pair
                        .Symbol?
                        .AllInterfaces
                        .Any(@interface => SymbolEqualityComparer.Default.Equals(@interface, iResolvable)) ?? false))
                .ToList();
            
            var implementations = allResolvable.Where(it => it.Symbol.TypeKind == TypeKind.Class).ToList();
            var services = allResolvable.Where(it => it.Symbol.TypeKind == TypeKind.Interface);

            var result = new List<KeyValuePair<string, string>>();
            foreach (var service in services)
            {
                var descendants = implementations
                    .Where(it => !it.Symbol.IsAbstract 
                                 && it.Symbol.AllInterfaces.Any(@interface => SymbolEqualityComparer.Default.Equals(@interface, service.Symbol)))
                    .ToList();
                
                if (!descendants.Any())
                    continue;

                descendants.ForEach(it =>
                {
                    var serviceDocumentationCommentId = service.Symbol.GetDocumentationCommentId()?[2..];
                    var implementationDocumentationCommentId = it.Symbol.GetDocumentationCommentId()?[2..];

                    if (serviceDocumentationCommentId != null && implementationDocumentationCommentId != null)
                    {
                        result.Add(new KeyValuePair<string, string>(serviceDocumentationCommentId, implementationDocumentationCommentId));
                    }
                });
            }

            var code = 
$@"using Core;
using DryIoc;

namespace {context.Compilation.AssemblyName}
{{
    public class __GeneratedModule : IAquaModule
    {{
        public void Register(IRegistrator registrator)
        {{
{string.Join(Environment.NewLine, result.Select(it => $@"            registrator.Register<{it.Key}, {it.Value}>();"))}
        }}
    }}
}}";
            
            context.AddSource($"{context.Compilation.AssemblyName}.__GeneratedModule", code);
        }
    }
}
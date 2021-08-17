using System.Linq;
using Microsoft.CodeAnalysis;

namespace AquaApplicationExtensionsGenerator
{
    [Generator]
    public class AquaApplicationExtensionsGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var result = context.Compilation.ReferencedAssemblyNames
                .Where(it => context.Compilation.GetTypeByMetadataName($"{it.Name}.__GeneratedModule") != null)
                .Select(it => $"new {it.Name}.__GeneratedModule()")
                .Concat(new[] { $"new {context.Compilation.Assembly.Name}.__GeneratedModule()" });
            
            var code = 
$@"using Core;

namespace {context.Compilation.AssemblyName}
{{
    public static class __AquaApplicationExtensions
    {{
        public static void Init(this AquaSourceGeneratorApplication application)
        {{
            var aquaModules = new IAquaModule[] {{ {string.Join(", ", result)} }};

            foreach (var aquaModule in aquaModules)
            {{
                aquaModule.Register(application.Registrator);
            }}
        }}
    }}
}}";
            
            context.AddSource($"{context.Compilation.AssemblyName}.__AquaApplicationExtensions", code);
        }
    }
}
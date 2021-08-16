using System.Linq;
using DryIoc;

namespace Core
{
    public class AquaReflectionApplication
    {
        Container Container { get; } = new();
        
        public TViewModel Run<TViewModel>()
            where TViewModel : IViewModel
        {
            RegisterAll(Container);
            
            return Container.Resolve<TViewModel>();
        }

        private static void RegisterAll(IRegistrator registrator)
        {
            var assemblies = typeof(IResolvable).GetDependentAssemblies()
                .Union(new[] { typeof(IResolvable).Assembly })
                .ToArray();
            
            var allResolvable = assemblies
                .SelectMany(it => it.GetTypes()
                    .Where(type => typeof(IResolvable).IsAssignableFrom(type) 
                                   && type != typeof(IResolvable)))
                .ToArray();
            
            var implementations = allResolvable.Where(it => it.IsClass).ToHashSet();
            var services = allResolvable.Where(it => it.IsInterface);

            foreach (var service in services)
            {
                var descendants = implementations
                    .Where(it => !it.IsAbstract && service.IsAssignableFrom(it))
                    .ToHashSet();

                if (!descendants.Any())
                    continue;

                descendants.ForEach(it => registrator.Register(service, it));
            }
        }
    }
}
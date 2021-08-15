using DryIoc;

namespace Core
{
    public partial class AquaSourceGeneratorApplication
    {
        Container Container { get; } = new();
        
        public TViewModel Run<TViewModel>()
            where TViewModel : IViewModel
        {
            RegisterAll(Container);
            
            return Container.Resolve<TViewModel>();
        }

        static partial void RegisterAll(IRegistrator registrator);
    }
}
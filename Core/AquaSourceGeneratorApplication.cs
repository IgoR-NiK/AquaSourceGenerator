using DryIoc;

namespace Core
{
    public class AquaSourceGeneratorApplication
    {
        Container Container { get; } = new();

        public IRegistrator Registrator => Container;

        public TViewModel Run<TViewModel>()
            where TViewModel : IViewModel
        {
            return Container.Resolve<TViewModel>();
        }
    }
}
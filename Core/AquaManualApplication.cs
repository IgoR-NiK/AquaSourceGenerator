using DryIoc;

namespace Core
{
    public class AquaManualApplication
    {
        Container Container { get; } = new();

        IAquaModule[] AquaModules { get; }

        public AquaManualApplication(params IAquaModule[] aquaModules)
        {
            AquaModules = aquaModules;
        }
        
        public TViewModel Run<TViewModel>()
            where TViewModel : IViewModel
        {
            RegisterAll(Container);
            
            return Container.Resolve<TViewModel>();
        }

        private void RegisterAll(IRegistrator registrator)
        {
            foreach (var aquaModule in AquaModules)
            {
                aquaModule.Register(registrator);
            }
        }
    }
}
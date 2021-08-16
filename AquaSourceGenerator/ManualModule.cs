using Core;
using DryIoc;

namespace AquaSourceGenerator
{
    public class ManualModule : IAquaModule
    {
        public void Register(IRegistrator registrator)
        {
            registrator.Register<ICalculator, AddCalculator>();
            registrator.Register<IMainViewModel, MainViewModel>();
            registrator.Register<ILogger, Logger>();
        }
    }
}
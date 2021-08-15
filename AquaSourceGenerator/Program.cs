using Core;

namespace AquaSourceGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var application = new AquaReflectionApplication();
            var viewModel = application.Run<IMainViewModel>();
            viewModel.Calculate(2, 3);
        }
    }
}
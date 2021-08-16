using System;
using Core;

namespace AquaSourceGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var application = new AquaManualApplication(new ManualModule());
            var viewModel = application.Run<IMainViewModel>();
            viewModel.Calculate(2, 3);
            
            Console.WriteLine("Click any key...");
            Console.ReadKey();
        }
    }
}
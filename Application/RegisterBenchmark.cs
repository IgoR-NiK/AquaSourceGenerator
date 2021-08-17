using Application;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Core;

namespace Application
{
    [SimpleJob(RuntimeMoniker.Net50)]
    public class RegisterBenchmark
    {
        [GlobalSetup]
        public void Setup()
        {
            new Source.Sources.Source1();
        }
        
        [Benchmark]
        public void ManualRegistration()
        {
            var application = new AquaManualApplication(new ManualModule());
            var viewModel = application.Run<IMainViewModel>();
            viewModel.Calculate(2, 3);
        }
        
        [Benchmark]
        public void SourceGeneratorRegistration()
        {
            var application = new AquaSourceGeneratorApplication();
            application.Init();
            var viewModel = application.Run<IMainViewModel>();
            viewModel.Calculate(2, 3);
        }

        [Benchmark]
        public void ReflectionRegistration()
        {
            var application = new AquaReflectionApplication();
            var viewModel = application.Run<IMainViewModel>();
            viewModel.Calculate(2, 3);
        }
    }
}
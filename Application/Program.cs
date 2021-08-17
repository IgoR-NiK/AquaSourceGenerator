using BenchmarkDotNet.Running;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<RegisterBenchmark>();
        }
    }
}
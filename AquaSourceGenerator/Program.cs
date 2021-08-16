using BenchmarkDotNet.Running;

namespace AquaSourceGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<RegisterBenchmark>();
        }
    }
}
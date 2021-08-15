using Core;

namespace AquaSourceGenerator
{
    public interface ICalculator : IResolvable
    {
        int Calculate(int x, int y);
    }
}
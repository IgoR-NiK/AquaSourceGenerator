using Core;

namespace Application
{
    public interface ICalculator : IResolvable
    {
        int Calculate(int x, int y);
    }
}
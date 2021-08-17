namespace Application
{
    public class AddCalculator : ICalculator
    {
        public int Calculate(int x, int y)
        {
            return x + y;
        }
    }
}
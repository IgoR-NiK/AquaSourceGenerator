using Core;

namespace AquaSourceGenerator
{
    public interface IMainViewModel : IViewModel
    {
        void Calculate(int x, int y);
    }
}
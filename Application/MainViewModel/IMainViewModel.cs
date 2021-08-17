using Core;

namespace Application
{
    public interface IMainViewModel : IViewModel
    {
        void Calculate(int x, int y);
    }
}
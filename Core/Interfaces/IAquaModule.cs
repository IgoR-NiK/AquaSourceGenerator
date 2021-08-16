using DryIoc;

namespace Core
{
    public interface IAquaModule
    {
        void Register(IRegistrator registrator);
    }
}
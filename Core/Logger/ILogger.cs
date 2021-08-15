namespace Core
{
    public interface ILogger : IResolvable
    {
        void Log(string text);
    }
}
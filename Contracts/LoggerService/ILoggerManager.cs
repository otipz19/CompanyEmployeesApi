namespace Contracts.LoggerService
{
    public interface ILoggerManager
    {
        public void LogInfo(string message);

        public void LogWarning(string message);

        public void LogDebug(string message);

        public void LogError(string message);
    }
}

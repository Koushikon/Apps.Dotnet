namespace Web_Api.StartupConfig
{
    public interface IUtilityLogger
    {
        void Critical(string message, Exception? ex = null, bool withEx = true);
        void Error(string message, Exception? ex = null, bool withEx = true);
        void Information(string message, Exception? ex = null, bool withEx = true);
        void Warning(string message, Exception? ex = null, bool withEx = true);
    }
}
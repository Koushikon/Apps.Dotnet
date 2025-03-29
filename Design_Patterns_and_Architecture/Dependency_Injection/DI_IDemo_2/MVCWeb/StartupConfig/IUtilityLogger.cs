namespace MVCWeb.StartupConfig
{
    public interface IUtilityLogger<T>
    {
        void Information(string message, Exception? ex = null, bool withEx = true);
    }
}
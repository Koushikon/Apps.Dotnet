namespace MVCWeb.StartupConfig;

public class UtilityLogger<T> : IUtilityLogger<T>
{
    private readonly ILogger<T> _log;
    private static bool _logSwitch = false;

    public UtilityLogger(IConfiguration config, ILogger<T> log)
    {
        _logSwitch = config.GetValue<bool>("LoggingSwitch");
        _log = log;
    }

    public void Information(string message, Exception? ex = null, bool withEx = true)
    {
        if (!_logSwitch) return;

        if (withEx)
        {
            _log.LogInformation(ex, "{Message}", message);
        }
        else
        {
            _log.LogInformation("{Message}", message);
        }
    }
}

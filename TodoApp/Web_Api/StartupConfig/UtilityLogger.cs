namespace Web_Api.StartupConfig;

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

    public void Warning(string message, Exception? ex = null, bool withEx = true)
    {
        if (!_logSwitch) return;

        if (withEx)
        {
            _log.LogWarning(ex, "{Message}", message);
        }
        else
        {
            _log.LogWarning("{Message}", message);
        }
    }

    public void Error(string message, Exception? ex = null, bool withEx = true)
    {
        if (!_logSwitch) return;

        if (withEx)
        {
            _log.LogError(ex, "{Message}", message);
        }
        else
        {
            _log.LogError("{Message}", message);
        }
    }

    public void Critical(string message, Exception? ex = null, bool withEx = true)
    {
        if (!_logSwitch) return;

        if (withEx)
        {
            _log.LogCritical(ex, "{Message}", message);
        }
        else
        {
            _log.LogCritical("{Message}", message);
        }
    }
}

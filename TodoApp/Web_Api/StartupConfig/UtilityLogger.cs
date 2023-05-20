namespace Web_Api.StartupConfig;

public class UtilityLogger<T> : IUtilityLogger
{
    private readonly IConfiguration _config;
    private readonly ILogger<T> _log;

    public UtilityLogger(IConfiguration config, ILogger<T> log)
    {
        _config = config;
        _log = log;
    }

    public void Information(string message, Exception? ex = null, bool withEx = true)
    {
        var loggerSwitch = _config.GetValue<bool>("LoggingSwitch");

        if (!loggerSwitch)
            return;

        if (withEx)
        {
            _log.LogInformation(ex, message);
        }
        else
        {
            _log.LogInformation(message);
        }
    }

    public void Warning(string message, Exception? ex = null, bool withEx = true)
    {
        var loggerSwitch = _config.GetValue<bool>("LoggingSwitch");

        if (!loggerSwitch)
            return;

        if (withEx)
        {
            _log.LogWarning(ex, message);
        }
        else
        {
            _log.LogWarning(message);
        }
    }

    public void Error(string message, Exception? ex = null, bool withEx = true)
    {
        var loggerSwitch = _config.GetValue<bool>("LoggingSwitch");

        if (!loggerSwitch)
            return;

        if (withEx)
        {
            _log.LogError(ex, message);
        }
        else
        {
            _log.LogError(message);
        }
    }

    public void Critical(string message, Exception? ex = null, bool withEx = true)
    {
        var loggerSwitch = _config.GetValue<bool>("LoggingSwitch");

        if (!loggerSwitch)
            return;

        if (withEx)
        {
            _log.LogCritical(ex, message);
        }
        else
        {
            _log.LogCritical(message);
        }
    }
}

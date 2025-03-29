namespace BlazorWeb.Data;

public class UTCDemo : IDemo, IUTCDemo
{
    public DateTime StartupTime { get; set; }

    // Set current universal datetime
    public UTCDemo()
    {
        StartupTime = DateTime.UtcNow;
    }
}

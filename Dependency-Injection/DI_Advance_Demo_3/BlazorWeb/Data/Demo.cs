namespace BlazorWeb.Data;

public class Demo : IDemo
{
    public DateTime StartupTime { get; set; }

    // Set current datetime
    public Demo()
    {
        StartupTime = DateTime.Now;
    }
}

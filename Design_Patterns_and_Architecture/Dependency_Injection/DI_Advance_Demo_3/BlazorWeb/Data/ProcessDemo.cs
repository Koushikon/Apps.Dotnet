namespace BlazorWeb.Data;

public class ProcessDemo
{
    private readonly IDemo _demo;

    // With Constructor Injection of Demo Class
    public ProcessDemo(IDemo demo)
    {
        _demo = demo;
    }

    // Gets the days of the current month
    public int GetDaysMonth()
    {
        return _demo.StartupTime.Month switch
        {
            1 or 3 or 5 or 7 or 8 or 10 or 12 => 31,
            4 or 6 or 9 or 11 => 30,
            2 => (_demo.StartupTime.Year % 4 == 0) ? 29 : 28,
            _ => throw new IndexOutOfRangeException()
        };
    }
}
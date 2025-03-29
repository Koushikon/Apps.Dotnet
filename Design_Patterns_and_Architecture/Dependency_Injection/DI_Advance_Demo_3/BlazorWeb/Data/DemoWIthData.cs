namespace BlazorWeb.Data;

public class DemoWIthData
{
    private readonly int _counter;

    public DemoWIthData(int counter)
    {
        _counter = counter;
    }

    public int Get()
    {
        return _counter;
    }
}
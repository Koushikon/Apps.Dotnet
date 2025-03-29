namespace Singleton_Pattern.Models;

/***
 * The same class behaves incorrectly in a multithreaded environment. Multiple threads can
 * call the creation method simultaneously and get several instances of Singleton class.
 */
public class TableServers
{
    private static readonly TableServers Instance = new TableServers();
    private readonly List<string> _servers = [];
    private int _nextServer;

    private TableServers()
    {
        _servers.Add("Susan");
        _servers.Add("Rose");
        _servers.Add("Nami");
        _servers.Add("Robin");
        _servers.Add("Franky");
    }

    public static TableServers GetInstance()
    {
        return Instance;
    }

    public string GetNextServer()
    {
        string server = _servers[_nextServer];
        
        _nextServer = (++_nextServer >= _servers.Count) ? 0 : _nextServer;
        return server;
    }
}
namespace Naive.Models;

public class TableServers
{
    private readonly List<string> _servers = [];
    private int _nextServer;

    public TableServers()
    {
        _servers.Add("Susan");
        _servers.Add("Rose");
        _servers.Add("Nami");
        _servers.Add("Robin");
        _servers.Add("Franky");
    }

    public string GetNextServer()
    {
        string server = _servers[_nextServer];
        
        _nextServer = (++_nextServer >= _servers.Count) ? 0 : _nextServer;
        return server;
    }
}
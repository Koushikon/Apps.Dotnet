using System.Threading;

namespace Singleton_Pattern.Models;

/***
 * To fix the problem, you have to synchronize threads during the first creation
 * of the Singleton object.
 *
 * This Singleton implementation is called "double check lock". It is safe
 * in multithreaded environment and provides lazy initialization for the
 * Singleton object.
 */
public class TableServersThreadSafe
{
    private static TableServersThreadSafe _instance;
    private readonly List<string> _servers = [];
    private int _nextServer;
    
    // We now have a lock object that will be used to synchronize threads
    // during first access to the Singleton.
    private static readonly object Lock = new object();

    private TableServersThreadSafe()
    {
        _servers.Add("Susan");
        _servers.Add("Rose");
        _servers.Add("Nami");
        _servers.Add("Robin");
        _servers.Add("Franky");
    }

    public static TableServersThreadSafe GetInstance()
    {
        if (_instance == null)
        {
            // Now, imagine that the program has just been launched. Since
            // there's no Singleton instance yet, multiple threads can
            // simultaneously pass the previous conditional and reach this
            // point almost at the same time. The first of them will acquire
            // lock and will proceed further, while the rest will wait here.
            lock (Lock)
            {
                // The first thread to acquire the lock, reaches this
                // conditional, goes inside and creates the Singleton
                // instance. Once it leaves the lock block, a thread that
                // might have been waiting for the lock release may then
                // enter this section. But since the Singleton field is
                // already initialized, the thread won't create a new
                // object.
                if (_instance == null)
                {
                    _instance = new TableServersThreadSafe();
                }
            }
        }
        return _instance;
    }

    public string GetNextServer()
    {
        string server = _servers[_nextServer];
        
        _nextServer = (++_nextServer >= _servers.Count) ? 0 : _nextServer;
        return server;
    }
}
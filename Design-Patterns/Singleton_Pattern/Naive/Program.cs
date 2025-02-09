using Naive.Models;

namespace Naive;

internal static class Program
{
    private static readonly TableServers FoodServers = new TableServers();
    private static readonly TableServers WelcomeServers = new TableServers();
    
    private static void Main()
    {
        // var servers = new TableServers();

        for (var i = 0; i < 5; i++)
        {
            // Console.WriteLine($"The next server is {servers.GetNextServer()}");
            
            // Now we're getting the same server for both work, and That is not ok.
            HostGetNextFoodServer();
            HostGetNextGreetingServer();
            
            // We need to synchronise the servers to serve to work both Greeting and Food serving
        }

        Console.ReadKey();
    }
    
    // To serve the people food
    private static void HostGetNextFoodServer()
    {
        Console.WriteLine($"The next food server is {FoodServers.GetNextServer()}");
    }
    
    // To welcome & greet the people and bring to their table
    private static void HostGetNextGreetingServer()
    {
        Console.WriteLine($"The next welcome server is {WelcomeServers.GetNextServer()}");
    }
}
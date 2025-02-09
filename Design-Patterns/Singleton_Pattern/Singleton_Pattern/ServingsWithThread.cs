using Singleton_Pattern.Models;

namespace Singleton_Pattern;

public static class ServingsWithThread
{
    // Now with Singleton we can use this class anywhere in the program,
    // and we'll get the same instance
    private static readonly TableServersThreadSafe FoodServers = TableServersThreadSafe.GetInstance();
    private static readonly TableServersThreadSafe WelcomeServers = TableServersThreadSafe.GetInstance();
    
    public static void Start()
    {
        for (var i = 0; i < 5; i++)
        {
            var process1 = new Thread(() =>
            {
                HostGetNextFoodServer();
            });
            var process2 = new Thread(() =>
            {
                HostGetNextGreetingServer();
            });
        
            process1.Start();
            process2.Start();
            
            process1.Join();
            process2.Join();
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
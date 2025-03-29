using Singleton_Pattern.Models;

namespace Singleton_Pattern;

public static class Servings
{
    // Now with Singleton we can use this class anywhere in the program,
    // and we'll get the same instance
    private static readonly TableServers FoodServers = TableServers.GetInstance();
    private static readonly TableServers WelcomeServers = TableServers.GetInstance();
    
    public static void Start()
    {
        for (var i = 0; i < 5; i++)
        {
            HostGetNextFoodServer();
            HostGetNextGreetingServer();
        }
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
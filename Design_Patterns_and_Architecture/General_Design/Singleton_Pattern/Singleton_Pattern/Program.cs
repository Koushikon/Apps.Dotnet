
namespace Singleton_Pattern;

internal static class Program
{
    private static void Main()
    {
        // Naive Singleton Approach - Without Thread Safe
        Servings.Start();
        
        // Singleton Approach - With Thread Safe
        Console.WriteLine("\nWith Thread Safe");
        ServingsWithThread.Start();
        
        Console.ReadKey();
    }
}


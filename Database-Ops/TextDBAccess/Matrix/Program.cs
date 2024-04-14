
using Microsoft.Extensions.Configuration;

namespace Matrix;

static class Program
{
    private static IConfiguration _config;
    private static string _textFile;

    static Program()
    {
        var builder = new ConfigurationBuilder()    // using Microsoft.Extensions.Configuration;
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");   // With Microsoft.Extensions.Configuration.Json;

        _config = builder.Build();
    }

    static void Main()
    {
        string _textFile = _config.GetValue<>();

        Console.WriteLine("Hello from user.");
        
    }
}
using BenchmarkDotNet.Running;

namespace Matrix;

/***
 * Note: #0001
 * Explain: Check if a URL Is Valid
 * Source: https://code-maze.com/csharp-how-to-check-if-an-url-is-valid/
 */

public class Program
{
    public static async Task Main()
    {
        await RunAll();

        //BenchmarkRunner.Run<BenchmarkUrlValidate>();

        Console.WriteLine("End of World.");
    }

    
    public async static Task RunAll()
    {
        // With Regex expression Url validate
        string url = "https://github.com/";
        bool success = UrlValidator.ValidateUrlWithRegex(url);
        Console.WriteLine($"This Url {url} is {(success ? "valid" : "invalid")}.");

        url = "ftp:////example.com///one?param=true";
        success = UrlValidator.ValidateUrlWithRegex(url);
        Console.WriteLine($"This Url {url} is {(success ? "valid" : "invalid")}.");


        // With Uri.TryCreate Url validate
        url = "https://api.facebook.com:443";
        success = UrlValidator.ValidateUrlWithUriCreate(url, out _);
        Console.WriteLine($"This url {url} is {(success ? "valid" : "invalid")}.");

        url = "ftp:///api.site.com?value=word1 word2";
        success = UrlValidator.ValidateUrlWithUriCreate(url, out _);
        Console.WriteLine($"This url {url} is {(success ? "valid" : "invalid")}.");


        // With Uri.IsWellFormedUriString to Url validate
        url = "https://site.company?q=search";
        success = UrlValidator.ValidateUrlWithUriWellFormedString(url);
        Console.WriteLine($"This url {url} is {(success ? "valid" : "invalid")}.");

        url = "ftp://api.site.com?value=word1 word2";
        success = UrlValidator.ValidateUrlWithUriWellFormedString(url);
        Console.WriteLine($"This url {url} is {(success ? "valid" : "invalid")}.");


        // With HttpClient to Url validate
        url = "https://api.facebook.com";
        success = await UrlValidator.ValidateWithHttpClientAsync(url);
        Console.WriteLine($"This url {url} is {(success ? "valid" : "invalid")}.");

        url = "https://www.example-nonexistent-url.com";
        success = await UrlValidator.ValidateWithHttpClientAsync(url);
        Console.WriteLine($"This url {url} is {(success ? "valid" : "invalid")}.");
    }    
}
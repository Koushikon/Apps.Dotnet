using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Matrix;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[MarkdownExporterAttribute.Default]
[MarkdownExporterAttribute.GitHub]
public class BenchmarkUrlValidate
{
    string url = "https://site.company?q=search";

    [Benchmark]
    public void RegexUrlValidationBenchmark()
    {
        var success = UrlValidator.ValidateUrlWithRegex(url);
    }

    [Benchmark]
    public void UriCreateValidationBenchmark()
    {
        var success = UrlValidator.ValidateUrlWithUriCreate(url, out _);
    }

    [Benchmark]
    public void UriWellFormedStringValidationBenchmark()
    {
        var success = UrlValidator.ValidateUrlWithUriWellFormedString(url);
    }

    [Benchmark]
    public async Task HttpClientValidationBenchmarkAsync()
    {
        var success = await UrlValidator.ValidateWithHttpClientAsync(url);
    }

    [Benchmark]
    public void HttpClientValidationBenchmark()
    {
        var success = UrlValidator.ValidateWithHttpClient(url);
    }
}
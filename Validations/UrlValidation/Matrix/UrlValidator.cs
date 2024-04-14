
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace Matrix;

public static class UrlValidator
{
    public static bool ValidateUrlWithRegex(string url)
    {
        var urlRegex = new Regex(@"^(https?|ftps?):\/\/(?:[a-zA-Z0-9]" +
            @"(?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,}" +
            @"(?::(?:0|[1-9]\d{0,3}|[1-5]\d{4}|6[0-4]\d{3}" +
            @"|65[0-4]\d{2}|655[0-2]\d|6553[0-5]))?" + @"(?:\/(?:[-a-zA-Z0-9@%_\+.~#?&=]+\/?)*)?$",
            RegexOptions.IgnoreCase);

        return urlRegex.IsMatch(url);
    }


    // Uri.TryCreate its a ease of use and makes good choice for most use cases.
    public static bool ValidateUrlWithUriCreate(string url, out Uri? uri)
    {
        return Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uri);
    }


    // Uri.IsWellFormedUriString makes sure is that the string is well formed Url following the Standard of Uri syntax.
    public static bool ValidateUrlWithUriWellFormedString(string url)
    {
        return Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute);
    }


    // Sending Http request to check if url is valid
    // While it provides real-time validation, drawbacks include the dependency on network
    // connectivity and potential performance overhead due to the need for an actual request.
    public static async Task<bool> ValidateWithHttpClientAsync(string url)
    {
        using var client = new HttpClient();

        try
        {
            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));

            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
            when (ex.InnerException is SocketException { SocketErrorCode: SocketError.HostNotFound })
        {
            return false;
        }
        catch (HttpRequestException ex)
            when (ex.StatusCode.HasValue && (int)ex.StatusCode.Value > 500)
        {
            return true;
        }
    }


    public static bool ValidateWithHttpClient(string url)
    {
        using var client = new HttpClient();

        try
        {
            var response = client.Send(new HttpRequestMessage(HttpMethod.Head, url));

            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
            when (ex.InnerException is SocketException { SocketErrorCode: SocketError.HostNotFound })
        {
            return false;
        }
        catch (HttpRequestException ex)
            when (ex.StatusCode.HasValue && (int)ex.StatusCode.Value > 500)
        {
            return true;
        }
    }
}
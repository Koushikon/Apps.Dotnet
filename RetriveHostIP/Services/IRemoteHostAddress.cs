using System.Net;

namespace RetriveHostIP.Services
{
    public interface IRemoteHostAddress
    {
        IPAddress? GetRemoteHostIpAddressUsingRemoteIpAddress(HttpContext http);
        IPAddress? GetRemoteHostIpAddressUsingXForwardedFor(HttpContext http);
        IPAddress? GetRemoteHostIpAddressUsingXRealIp(HttpContext http);
    }
}
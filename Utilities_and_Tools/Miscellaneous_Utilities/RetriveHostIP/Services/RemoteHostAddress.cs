using System.Net;
using System.Net.Sockets;

namespace RetriveHostIP.Services;

public class RemoteHostAddress : IRemoteHostAddress
{
    public IPAddress? GetRemoteHostIpAddressUsingRemoteIpAddress(HttpContext http)
    {
        return http.Connection.RemoteIpAddress;
    }

    public IPAddress? GetRemoteHostIpAddressUsingXForwardedFor(HttpContext http)
    {
        IPAddress? remoteIpAddress = null;
        var forwardedFor = http.Request.Headers["X-Forwarded-For"].FirstOrDefault();

        if (!string.IsNullOrEmpty(forwardedFor))
        {
            var ips = forwardedFor.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                  .Select(s => s.Trim());

            foreach (var ip in ips)
            {
                if (IPAddress.TryParse(ip, out var address) &&
                    (address.AddressFamily is AddressFamily.InterNetwork
                    or AddressFamily.InterNetworkV6))
                {
                    remoteIpAddress = address;
                    break;
                }
            }
        }

        return remoteIpAddress;
    }

    public IPAddress? GetRemoteHostIpAddressUsingXRealIp(HttpContext http)
    {
        IPAddress? remoteIpAddress = null;
        var xRealIpExists = http.Request.Headers.TryGetValue("X-Real-IP", out var xRealIp);

        if (xRealIpExists)
        {
            if (!IPAddress.TryParse(xRealIp, out IPAddress? address))
            {
                return remoteIpAddress;
            }

            var isValidIP = (address.AddressFamily is AddressFamily.InterNetwork
                            or AddressFamily.InterNetworkV6);

            if (isValidIP)
            {
                remoteIpAddress = address;
            }

            return remoteIpAddress;
        }

        return remoteIpAddress;
    }
}

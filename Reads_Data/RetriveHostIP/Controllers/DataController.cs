using Microsoft.AspNetCore.Mvc;
using RetriveHostIP.Services;

namespace RetriveHostIP.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DataController : ControllerBase
{
    private readonly IRemoteHostAddress _ipAddress;

    public DataController(IRemoteHostAddress ipAddress)
    {
        _ipAddress = ipAddress;
    }

    [HttpGet]
    public IActionResult UserData()
    {
        /**
         * Learn from: https://code-maze.com/aspnetcore-how-to-get-the-remote-host-ip-address/amp/
         * Source: https://github.com/CodeMazeBlog/CodeMazeGuides
         * Get_Host_IP_ASP.NET::
         * 
         * For Local machine request comes from same machine as Web Server
         * IPv6 type loopback address is ::1
         * IPv4 address type it’s 127.0.0.1
         * 
         * In case no X-Forwarded-For header found it will return null
         * In case no value found for X-Real-IP header it will return null
         */
        var first = _ipAddress.GetRemoteHostIpAddressUsingRemoteIpAddress(HttpContext);
        var second = _ipAddress.GetRemoteHostIpAddressUsingXForwardedFor(HttpContext);
        var third = _ipAddress.GetRemoteHostIpAddressUsingXRealIp(HttpContext);

        string message = $"Getting IP Address:\n First Way: {first},\n Second way: {second},\n Third Way: {third}";

        return Ok(message);
    }
}

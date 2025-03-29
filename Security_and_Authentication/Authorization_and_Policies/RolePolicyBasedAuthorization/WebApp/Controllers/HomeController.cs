using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers;

// Matches "UserPolicy" Policy and "Admin" Roles to access
// This applies to every Page inside this Controller, Except [AllowAnonymous] Attribute ActionResult
[Authorize(Policy = "UserPolicy")]
[Authorize(Roles = "Admin")]
public class HomeController : Controller
{
    public HomeController()
    { }

    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class UsersController : Controller
{
    [Authorize]
    public IActionResult Index()
    {
        var users = new Users();
        return View(users.GetUsers());
    }

    // Matches the User Policy which is Email and DateOfBirth Claim : Found
    [Authorize(Policy = "UserPolicy")]
    public IActionResult UserPolicy()
    {
        var users = new Users();
        return View("Index", users.GetUsers());
    }

    // Matches the UserName Claim with "User" : Not Found
    [Authorize(Roles = "User")]
    public IActionResult UserRole()
    {
        var users = new Users();
        return View("Index", users.GetUsers());
    }

    // Matches the UserName Claim with "Admin" : Found
    [Authorize(Roles = "Admin")]
    public IActionResult UserAdmin()
    {
        var users = new Users();
        return View("Index", users.GetUsers());
    }

    // Matches Both Roles "Admin" and "Super" if Both true Only Then this Page is accessable
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Super")]
    public IActionResult UserHall1()
    {
        var users = new Users();
        return View("Index", users.GetUsers());
    }

    // Matches One of the Roles "Admin" or "Super" to access this Page
    [Authorize(Roles = "Admin, Super")]
    public IActionResult UserHall2()
    {
        var users = new Users();
        return View("Index", users.GetUsers());
    }
}
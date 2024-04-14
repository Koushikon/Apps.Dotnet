using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Models;

namespace WebApp.Controllers;

public class LoginController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index([Bind] Users userModel)
    {
        var user = new Users().GetUsers().Where(x => x.UserName == userModel.UserName).SingleOrDefault();

        if(user == null)
        {
            return View(user);
        }

        var userClaims = new List<Claim>
        {
            new("UserName", user.UserName),
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Email, user.EmailId),
            new(ClaimTypes.DateOfBirth, user.DateOfBirth),
            new(ClaimTypes.Role, user.Role)
        };

        var userIdentity = new ClaimsIdentity(userClaims, "User Identity");
        var userPrinciple = new ClaimsPrincipal([userIdentity]);

        HttpContext.SignInAsync(userPrinciple);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult UserAccessDenied()
    {
        return View();
    }
}
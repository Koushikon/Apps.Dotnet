using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;


// Only Accessable by the users who's Role is "Admin" or "Super".
// This applies to every  ActionResult inside this Controller
[Authorize(Roles = "Admin,Super")]
public class OfficeController : Controller
{
    public IActionResult Index()
    {
        var users = new Users();
        return View("Index", users.GetUsers().Where(x => x.EmailId.Contains("gmail")));
    }
}
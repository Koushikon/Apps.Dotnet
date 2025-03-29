using Microsoft.AspNetCore.Mvc;
using Web.Attributes;

namespace Web.Controllers;

public class AuthController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    /***
     * Authorization Filter Demo
     */

    [Authorize("Read")]
    public IActionResult Read()
    {
        return View();
    }

    [Authorize("Write")]
    public IActionResult Edit()
    {
        return View();
    }
}
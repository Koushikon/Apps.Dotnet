using Microsoft.AspNetCore.Mvc;
using Web.Attributes;

namespace Web.Controllers;

public class HeaderController : Controller
{
    [AddHeader]
    public IActionResult Index()
    {
        return View();
    }
}
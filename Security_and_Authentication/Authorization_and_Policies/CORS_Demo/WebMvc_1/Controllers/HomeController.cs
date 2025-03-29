using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebMvc_1.Models;

namespace WebMvc_1.Controllers;

/***
 * [EnableCors(<policy_name>)
 * This enables for speific Controller/Action to allow Cors polcy
 */
[EnableCors("Policy1")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public JsonResult LoadCors()
    {
        return Json("Hello from MVC 1 with Cors");
    }


    /***
     * [DisableCors]
     * This disables for speific Controller/Action to allow Cors polcy
     */
    [DisableCors]
    [HttpGet]
    public JsonResult Load()
    {
        return Json("Hello from MVC 1 without Cors");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
using Microsoft.AspNetCore.Mvc;
using MVCWeb.StartupConfig;
using System.Diagnostics;

namespace MVCWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IThing<HomeController> _thing;
        private readonly IUtilityLogger<HomeController> _log;

        public HomeController(IThing<HomeController> thing, IUtilityLogger<HomeController> log)
        {
            _thing = thing;
            _log = log;
        }

        public IActionResult Index()
        {
            ViewBag.Message = _thing.GetName;
            _log.Information("With DI in MVC Controller");
            return View();
        }
    }
}
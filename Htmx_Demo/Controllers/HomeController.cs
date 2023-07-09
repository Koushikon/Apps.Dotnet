using Htmx_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Htmx_Demo.Controllers
{
    public class HomeController : Controller
    {
        public static int _counter = 0;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetData()
        {
            _counter++;

            return Ok($"Clicked {_counter} Times");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
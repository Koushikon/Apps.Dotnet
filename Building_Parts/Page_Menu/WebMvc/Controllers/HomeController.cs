using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebMvc.Data;
using WebMvc.Models;

namespace WebMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebMvcContext _context;

        public HomeController(WebMvcContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var result = _context.Menu.ToList();
            foreach(var item in result)
            {
                item.MenuItems = _context.MenuItem
                    .Where(m => m.ParentMenu.Id == item.Id)
                    .ToList();
            }
            ViewBag.MList = result;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

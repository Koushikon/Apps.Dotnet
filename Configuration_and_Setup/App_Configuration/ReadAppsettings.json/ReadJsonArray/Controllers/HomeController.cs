using Microsoft.AspNetCore.Mvc;
using ReadJsonArray.Models;
using ReadJsonArray.Services;
using System.Diagnostics;

namespace ReadJsonArray.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var userList1 = AppSettingsService.GetUsersV1(_configuration);
            var userList2 = AppSettingsService.GetUsersV2(_configuration);

            var members = AppSettingsService.GetGroupMembers(_configuration);

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
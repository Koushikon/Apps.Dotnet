using CaptchaV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CaptchaV2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptionsMonitor<GoogleCaptchaConfig> _config;

        public HomeController(IOptionsMonitor<GoogleCaptchaConfig> config)
        {
            _config = config;
        }


        // GET: Home/Index
        public IActionResult Index(Login formData)
        {
            return View();
        }


        // POST: Home/VerifyToken
        [HttpPost]
        public async Task<JsonResult> VerifyToken(string token)
        {
            string url = $"https://www.google.com/recaptcha/api/siteverify?secret={_config.CurrentValue.SecretKey}&response={token}";

            using var client = new HttpClient();
            string response = await client.GetStringAsync(url);

            return Json(response);
        }
    }
}
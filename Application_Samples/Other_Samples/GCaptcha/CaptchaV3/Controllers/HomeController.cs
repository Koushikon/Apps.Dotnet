using CaptchaV3.Models;
using CaptchaV3.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CaptchaV3.Controllers
{
    public class HomeController : Controller
    {
        private readonly GoogleCaptchaService _captchaService;

        public HomeController(GoogleCaptchaService captchaService)
        {
            _captchaService = captchaService;
        }


        // GET: Home/Index
        public IActionResult Index()
        {
            return View();
        }


        // POST: Home/Index
        [HttpPost]
        public async Task<IActionResult> Index(Login formData)
        {
            var captchaResult = await _captchaService.VerifyTokenStatus(formData.Token);

            if (!captchaResult || !ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Dashboard");
        }


        // GET: Home/Dashboard
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
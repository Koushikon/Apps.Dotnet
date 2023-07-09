using CaptchaV2_AsService.Models;
using CaptchaV2_AsService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CaptchaV2_AsService.Controllers;

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
        // get the VerifyToken status
        var vStatus = await _captchaService.VerifyTokenStatus(formData.Token);

        // get the VerifyToken with proper results
        var vResult = await _captchaService.VerifyTokenResult(formData.Token);

        if (!vStatus || !ModelState.IsValid)
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
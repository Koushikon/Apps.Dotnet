using Localize_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;

namespace Localize_1.Controllers;

public class LocalizationController : Controller
{
    private readonly IStringLocalizer<LocalizationController> _localizer;
    private readonly IHtmlLocalizer<LocalizationController> _htmlLocalizer;

    public LocalizationController(IStringLocalizer<LocalizationController> localizer, IHtmlLocalizer<LocalizationController> htmlLocalizer)
    {
        _localizer = localizer;
        _htmlLocalizer = htmlLocalizer;
    }

    // Used to show IStringLocalizer and IHtmlLocalizer
    // GET: Localization/Index
    public IActionResult Index()
    {
        ViewBag.Greeting = _localizer["Greeting"];
        ViewBag.HtmlMovieUpdate = _htmlLocalizer["HtmlMovieUpdate"];

        return View();
    }

    // Used to show IViewLocalizer
    // GET: Localization/LocalizedView
    public IActionResult LocalizedView()
    {
        return View();
    }


    // Used to show Data Annotation Localization
    // GET: Localization/DataAnnotationView
    [HttpGet]
    public IActionResult DataAnnotationView([FromQuery] string? culture)
    {
        ViewBag.Culture = culture;

        return View();
    }

    // POST: Localization/DataAnnotationView
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DataAnnotationView(PersonViewModel personViewModel, [FromQuery] string? culture)
    {
        ViewBag.Culture = culture;

        if (!ModelState.IsValid)
        {
            return View();
        }

        return View();
    }
}

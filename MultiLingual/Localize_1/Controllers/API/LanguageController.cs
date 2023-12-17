using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;

namespace Localize_1.Controllers.API;

[Route("api/[controller]")]
[ApiController]
public class LanguageController
{
    private readonly IStringLocalizer<LanguageController> _localizer;
    private readonly IHtmlLocalizer<LanguageController> _htmlLocalizer;

    public LanguageController(IStringLocalizer<LanguageController> localizer, IHtmlLocalizer<LanguageController> htmlLocalizer)
    {
        _localizer = localizer;
        _htmlLocalizer = htmlLocalizer;
    }

    [HttpGet("GetString")]
    public string GetString()
    {
        string greeting = _localizer["Greeting"];

        return greeting;
    }

    [HttpGet("GetHtmlString")]
    public string GetHtmlString()
    {
        string htmlGreeting = _htmlLocalizer["HtmlMovieUpdate"].Value;

        return htmlGreeting;
    }
}

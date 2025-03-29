using Microsoft.AspNetCore.Mvc;
using Web.Attributes;
using Web.Models;

namespace Web.Controllers;

public class ValidateController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    /***
     * Adding the Validate Model Action Filter
     * to Validate the submitting model data
     */
    [ValidateModel]
    [HttpPost]
    public IActionResult Index(Doctor doctor)
    {
        return RedirectToAction(nameof(Index));
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Models;

namespace Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index(Ticket ticket, string FormTask)
    {

        if (FormTask == "Save" && ModelState.IsValid)
        {
            if(!ticket.IsAgreeOnPrivacy)
            {
                TempData["ErrorMsg"] = "You must agree on Terms and Condition.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Msg"] = $"{ticket.Name} record are inserted.";
            return RedirectToAction(nameof(Index));
        }

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
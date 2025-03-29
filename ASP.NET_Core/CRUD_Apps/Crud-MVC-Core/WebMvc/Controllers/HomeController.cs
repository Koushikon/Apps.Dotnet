using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebMvc.Models;

namespace WebMvc.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Upsert()
    {
        Person person = new Person();
        person.DateOfBirth = DateTime.Now.AddDays(10);

        return View(person);
    }

    [HttpPost]
    public IActionResult Upsert(Person person)
    {
        return View();
    }


    public IActionResult SelectTagHelper(Person person)
    {
        var group1 = new SelectListGroup { Name = "Developer" };
        var group2 = new SelectListGroup { Name = "Tester" };
        ViewBag.employeeList = new List<SelectListItem>()
        {
            new() { Value = "1", Text = "Ashish", Group = group1, Disabled = true },
            new() { Value = "2", Text = "Anirban", Group = group1 },
            new() { Value = "3", Text = "Koushik", Group = group1, Selected = true },
            new() { Value = "4", Text = "Sourav", Group = group1 },

            new() { Value = "5", Text = "Suman", Group = group2 },
            new() { Value = "6", Text = "Bikram", Group = group2 }
        };

        var pObj = new Person();

        if (ModelState.IsValid)
        {
            return View(pObj);
        }

        return View(pObj);
    }
}
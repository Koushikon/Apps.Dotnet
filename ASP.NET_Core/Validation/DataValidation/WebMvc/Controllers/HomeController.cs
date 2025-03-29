using Microsoft.AspNetCore.Mvc;
using WebMvc.Models;

namespace WebMvc.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult TripManagerSample()
    {
        return View();
    }

    public IActionResult HotelSample()
    {
        var lstHotel = new List<Hotel>();
        lstHotel.Add(new Hotel { Id = 1, Name = null });
        lstHotel.Add(new Hotel { Id = 2, Name = "Best one ever" });
        return View(lstHotel);
    }

    public IActionResult PersonSample()
    {
        return View();
    }

    public IActionResult AddCar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddCar(Car car)
    {
        if (ModelState.IsValid)
        {
            string? brand = car.Brand;
        }

        return View();
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _context;

        public HomeController(ApplicationDBContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            Employee empObj = new()
            {
                Employees = _context.Employee.ToList()
            };

            return View(empObj);
        }


        [HttpPost]
        public IActionResult Upsert(Employee model)
        {
            if (ModelState.IsValid)
            {
                if (model.ID <= 0)
                {
                    _context.Employee.Add(model);
                }
                else
                {
                    _context.Employee.Update(model);
                }
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpDelete]
        public IActionResult Delete([FromQuery] List<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return Json(new { success = false, message = "No IDs provided." });
            }

            try
            {
                foreach (var id in ids)
                {
                    var employee = _context.Employee.Find(id);
                    if (employee != null)
                    {
                        _context.Employee.Remove(employee);
                    }
                }
                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

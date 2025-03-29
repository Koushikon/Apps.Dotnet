using AppDataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MenuItemController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _hostEnvironment;

    public MenuItemController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _hostEnvironment = hostEnvironment;
    }

    // GET: api/MenuItem
    [HttpGet]
    public IActionResult Get()
    {
        /***
         * We're getting all the data from MenuItem But not the Food Type, Category
         * To the End-user Category Id doesn't matter, for our case Category Name would be best
         * Also Sometime we need more than Id, Name. We can use "include" property
         * To bring the data of MenuItem with related Category and FoodType Data
         */
        //var menuItemList = _unitOfWork.MenuItem.GetAll();


        /***
         * With allowing Include property
         * Proding includeProperties string parameter is Case Sensetive
         * Table name must match
         */
        var menuItemList = _unitOfWork.MenuItem.GetAll(includeProperties: "Category,FoodType");

        return Json(new { data = menuItemList });
    }


    // DELETE: api/MenuItem
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var objFromDb = _unitOfWork.MenuItem.GetById(u => u.Id == id);

        if (objFromDb == null)
        {
            return Json(new { success = false, message = "Not found." });
        }

        // Delete Image file
        bool _ = FileDelete(@"images\MenuItems", objFromDb.Image ?? string.Empty);

        _unitOfWork.MenuItem.Remove(objFromDb);
        _unitOfWork.Save();

        return Json(new { success = true, message = "Delete success." });
    }

    /***
     * - Single Responsibility Principle - for this function
     * 
     * FileUpload: only upload the file to a specified location
     * FileDelete: only delete the file from a specified location
     */

    private bool FileDelete(string path, string oldName = "")
    {
        if (string.IsNullOrWhiteSpace(oldName))
        {
            return false;
        }

        string filePath = Path.Combine(_hostEnvironment.WebRootPath, path, oldName);

        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
            return true;
        }

        return false;
    }
}
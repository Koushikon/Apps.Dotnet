using AppDataAccess.Repository.IRepository;
using AppModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Web.Models;

namespace Web.Pages.Admin.MenuItems;

[BindProperties]
public class AddEditModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _hostEnvironment;

    public string TinyMiceKey { get; set; } = default!;
    public MenuItem MenuItem { get; set; } = default!;
    public IEnumerable<SelectListItem> DDCategory { get; set; } = default!;
    public IEnumerable<SelectListItem> DDFoodType { get; set; } = default!;

    public AddEditModel(IOptionsMonitor<AppSettings> config, IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
    {
        TinyMiceKey = config.CurrentValue.key;
        _unitOfWork = unitOfWork;
        _hostEnvironment = hostEnvironment;
    }

    public void OnGet(int? id)
    {
        MenuItem = new();

        DDCategory = _unitOfWork.Category.GetAll().Select(item => new SelectListItem()
        {
            Text = item.Name,
            Value = item.Id.ToString()
        });

        DDFoodType = _unitOfWork.FoodType.GetAll().Select(item => new SelectListItem()
        {
            Text = item.Name,
            Value = item.Id.ToString()
        });

        if (id != null && id > 0)
        {
            MenuItem = _unitOfWork.MenuItem.GetById(u => u.Id == id);
        }
    }


    public IActionResult OnPost()
    {
        // Getting the old data
        var objFromDb = _unitOfWork.MenuItem.GetById(u => u.Id == MenuItem.Id);

        var files = HttpContext.Request.Form.Files;
        if (files?.Count > 0)
        {
            // Delete Image file
            bool _ = FileDelete(@"images\MenuItems", objFromDb?.Image ?? string.Empty);

            // Upload Image
            MenuItem.Image = FileUpload(file: files[0], path: @"images\MenuItems");
        }

        string? message = null;
        if (MenuItem.Id <= 0)
        {
            _unitOfWork.MenuItem.Add(MenuItem);
            message = $"Menu {MenuItem.Name} created successfully";
        }
        else
        {
            if (string.IsNullOrWhiteSpace(MenuItem.Image))
            {
                MenuItem.Image = objFromDb?.Image;
            }

            _unitOfWork.MenuItem.Update(MenuItem);
            message = $"Menu {MenuItem.Name} updated successfully";
        }
        _unitOfWork.Save();
        TempData["success"] = message;

        return RedirectToPage("Index");
    }

    /***
     * - Single Responsibility Principle - for this function
     * 
     * FileUpload: only upload the file to a specified location
     * FileDelete: only delete the file from a specified location
     */


    private string? FileUpload(IFormFile file, string path)
    {
        if (file.Length <= 0)
        {
            return null;
        }

        string fileExtension = Path.GetExtension(file.FileName);
        string fileNameNew = Guid.NewGuid().ToString() + fileExtension;
        string filePath = Path.Combine(_hostEnvironment.WebRootPath, path, fileNameNew);

        using (FileStream fileStream = new(filePath, FileMode.Create))
        {
            file.CopyTo(fileStream);
        }

        return fileNameNew;
    }

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
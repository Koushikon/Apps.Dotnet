using AppDataAccess.Repository.IRepository;
using AppModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Categories;

[BindProperties]
public class AddEditModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public Category Category { get; set; } = default!;

    public AddEditModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void OnGet(int id)
    {
        if (id > 0)
        {
            Category = _unitOfWork.Category.GetById(u => u.Id == id);
        }
    }

    public async Task<IActionResult> OnPost()
    {
        if (Category.Name == Category.Description)
        {
            ModelState.AddModelError("Category.Name", "Desciption cannot exactly match the name.");
        }

        if (ModelState.IsValid)
        {
            string? message = null;
            if (Category.Id <= 0)
            {
                _unitOfWork.Category.Add(Category);
                message = $"Category {Category.Name} created successfully";
            }
            else
            {
                _unitOfWork.Category.Update(Category);
                message = $"Category {Category.Name} updated successfully";
            }
            _unitOfWork.Save();
            TempData["success"] = message;

            return RedirectToPage("Index");
        }

        return Page();
    }
}
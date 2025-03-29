using AppDataAccess.Repository.IRepository;
using AppModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Categories;

[BindProperties]
public class DeleteModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public Category Category { get; set; } = default!;

    public DeleteModel(IUnitOfWork unitOfWork)
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

    public IActionResult OnPost()
    {
        var dataCategory = _unitOfWork.Category.GetById(u => u.Id == Category.Id);

        if (dataCategory != null)
        {
            _unitOfWork.Category.Remove(dataCategory);
            _unitOfWork.Save();
            TempData["success"] = $"Category {dataCategory.Name} deleted successfully";

            return RedirectToPage("Index");
        }

        return Page();
    }
}
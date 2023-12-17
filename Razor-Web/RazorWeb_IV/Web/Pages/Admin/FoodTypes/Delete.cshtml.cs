using AppDataAccess.Data;
using AppDataAccess.Migrations;
using AppDataAccess.Repository.IRepository;
using AppModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.FoodTypes;

[BindProperties]
public class DeleteModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public FoodType FoodType { get; set; } = default!;

    public DeleteModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void OnGet(int id)
    {
        if (id > 0)
        {
            FoodType = _unitOfWork.FoodType.GetById(u => u.Id == id);
        }
    }

    public async Task<IActionResult> OnPost()
    {
        var dataCategory = _unitOfWork.FoodType.GetById(u => u.Id == FoodType.Id);

        if (dataCategory != null)
        {
            _unitOfWork.FoodType.Remove(dataCategory);
            _unitOfWork.Save();
            TempData["success"] = $"Food Type {dataCategory.Name} deleted successfully";

            return RedirectToPage("Index");
        }

        return Page();
    }
}

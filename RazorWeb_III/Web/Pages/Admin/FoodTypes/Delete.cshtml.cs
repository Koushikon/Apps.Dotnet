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
    private readonly IFoodTypeRepository _dbFoodType;

    public FoodType FoodType { get; set; } = default!;

    public DeleteModel(IFoodTypeRepository dbFoodType)
    {
        _dbFoodType = dbFoodType;
    }

    public void OnGet(int id)
    {
        if (id > 0)
        {
            FoodType = _dbFoodType.GetById(u => u.Id == id);
        }
    }

    public async Task<IActionResult> OnPost()
    {
        var dataCategory = _dbFoodType.GetById(u => u.Id == FoodType.Id);

        if (dataCategory != null)
        {
            _dbFoodType.Remove(dataCategory);
            _dbFoodType.Save();
            TempData["success"] = $"Food Type {dataCategory.Name} deleted successfully";

            return RedirectToPage("Index");
        }

        return Page();
    }
}

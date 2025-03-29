using AppDataAccess.Data;
using AppDataAccess.Migrations;
using AppDataAccess.Repository.IRepository;
using AppModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.FoodTypes;

[BindProperties]
public class AddEditModel : PageModel
{
    private readonly IFoodTypeRepository _dbFoodType;

    public FoodType FoodType { get; set; } = default!;

    public AddEditModel(IFoodTypeRepository dbFoodType)
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
        if (ModelState.IsValid)
        {
            string? message = null;
            if (FoodType.Id <= 0)
            {
                _dbFoodType.Add(FoodType);
                message = $"Food Type {FoodType.Name} created successfully";
            }
            else
            {
                _dbFoodType.Update(FoodType);
                message = $"Food Type {FoodType.Name} updated successfully";
            }
            _dbFoodType.Save();
            TempData["success"] = message;

            return RedirectToPage("Index");
        }

        return Page();
    }
}

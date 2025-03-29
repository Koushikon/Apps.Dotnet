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
    private readonly IUnitOfWork _unitOfWork;

    public FoodType FoodType { get; set; } = default!;

    public AddEditModel(IUnitOfWork unitOfWork)
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
        if (ModelState.IsValid)
        {
            string? message = null;
            if (FoodType.Id <= 0)
            {
                _unitOfWork.FoodType.Add(FoodType);
                message = $"Food Type {FoodType.Name} created successfully";
            }
            else
            {
                _unitOfWork.FoodType.Update(FoodType);
                message = $"Food Type {FoodType.Name} updated successfully";
            }
            _unitOfWork.Save();
            TempData["success"] = message;

            return RedirectToPage("Index");
        }

        return Page();
    }
}

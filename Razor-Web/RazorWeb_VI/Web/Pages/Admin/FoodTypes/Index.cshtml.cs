using AppDataAccess.Repository.IRepository;
using AppModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.FoodTypes;

[BindProperties]
public class IndexModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public IEnumerable<FoodType> FoodTypes { get; set; } = default!;

    public IndexModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void OnGet()
    {
        FoodTypes = _unitOfWork.FoodType.GetAll();
    }
}

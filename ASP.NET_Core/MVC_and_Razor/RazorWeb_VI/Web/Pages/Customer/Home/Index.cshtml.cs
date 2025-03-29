using AppDataAccess.Repository.IRepository;
using AppModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Customer.Home;

public class IndexModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    public IEnumerable<MenuItem> MenuItemList { get; set; } = default!;
    public IEnumerable<Category> CategoryList { get; set; } = default!;

    public IndexModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void OnGet()
    {
        MenuItemList = _unitOfWork.MenuItem.GetAll(includeProperties: "Category,FoodType");
        CategoryList = _unitOfWork.Category.GetAll(orderby: u => u.OrderBy(c => c.DisplayOrder));
    }
}
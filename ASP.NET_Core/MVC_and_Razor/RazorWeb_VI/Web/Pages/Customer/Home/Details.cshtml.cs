using AppDataAccess.Repository.IRepository;
using AppModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Web.Pages.Customer.Home;

/***
 * With this [Authorize] attribute we can add Authorization at Page level 
 * Because Every Page has its own Page Model we can add in here
 */
[Authorize]
public class DetailsModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    //public MenuItem MenuItem { get; set; } = default!;

    //[Range(1, 50, ErrorMessage = "Please select count between 1 to 50.")]
    //public int Count { get; set; }

    public DetailsModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [BindProperty]
    public ShoppingCart ShoppingCart { get; set; } = default!;

    public void OnGet(int id)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity!;

        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!;

        ShoppingCart = new()
        {
            ApplicationUserId = claim.Value,
            MenuItemId = id,
            MenuItem = _unitOfWork.MenuItem.GetById(u => u.Id == id, includeProperties: "Category,FoodType")
        };
    }


    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            ShoppingCart shoppingCartFromDb = _unitOfWork.ShoppingCart.GetById(filter: u =>
                u.ApplicationUserId == ShoppingCart.ApplicationUserId &&
                u.MenuItemId == ShoppingCart.MenuItemId);

            if (shoppingCartFromDb == null)
            {
                _unitOfWork.ShoppingCart.Add(ShoppingCart);
                _unitOfWork.Save();
            }
            else
            {
                _unitOfWork.ShoppingCart.IncrementCount(shoppingCartFromDb, ShoppingCart.Count);
                _unitOfWork.Save();
            }

            return RedirectToPage("Index");
        }

        return Page();
    }
}
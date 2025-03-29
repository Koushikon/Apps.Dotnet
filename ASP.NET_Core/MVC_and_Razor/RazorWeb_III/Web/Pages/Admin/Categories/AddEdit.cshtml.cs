using AppDataAccess.Repository.IRepository;
using AppModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Categories;

[BindProperties]
public class AddEditModel : PageModel
{
    private readonly ICategoryRepository _dbCategory;

    public Category Category { get; set; } = default!;

    public AddEditModel(ICategoryRepository dbCategory)
    {
        _dbCategory = dbCategory;
    }

    public void OnGet(int id)
    {
        if (id > 0)
        {
            Category = _dbCategory.GetById(u => u.Id == id);
        }
    }


    /***
     * With [BindProperty] on Model Object we don't need to explicily add model name as parameter
     * 
     * * We Can use as Parameter: OnPost(Category category)
     */

    public async Task<IActionResult> OnPost()
    {
        // Adding Server side validation
        // Adding Custom Validations with providing Error message
        if (Category.Name == Category.Description)
        {
            /***
             * Info: ModelState.AddModelError(<Unique Key>, <Error Message>)
             * 
             * Key name can be ("Category.Name" or "Category.Description") to show error messages on that field
             */

            // ModelState.AddModelError("Category.Name", "Desciption cannot exactly match the same.");
            ModelState.AddModelError(string.Empty, "Desciption cannot exactly match the same.");
        }


        /***
         * With ModelState.IsValid Checks the form every properties state, i.e. Valid or Invalid
         * If every field state is Valid only then its True.
         * 
         * In ModelState Results View We can check out every field State
         */
        if (ModelState.IsValid)
        {
            string? message = null;
            if (Category.Id <= 0)
            {
                _dbCategory.Add(Category);
                message = $"Category {Category.Name} created successfully";
            }
            else
            {
                _dbCategory.Update(Category);
                message = $"Category {Category.Name} updated successfully";
            }
            _dbCategory.Save();
            TempData["success"] = message;

            return RedirectToPage("Index");
        }

        return Page();
    }
}

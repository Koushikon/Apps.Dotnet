using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Data;
using Web.Models;

namespace Web.Pages.Categories;

/***
 * * [BindProperties]
 * Explain: With this the whole class public properties will be available for the Page
 */

[BindProperties]
public class AddEditModel : PageModel
{
    private readonly ApplicationDBContext _db;


    /***
     * On Class Level: With [BindProperties] we don't need to explicily add each property separately
     * 
     * * [BindProperty]
     * Explain: With this we need to explicily add on each property separately and it will be available for the Page
     */

    //[BindProperty]
    public Category Category { get; set; } = default!;

    public AddEditModel(ApplicationDBContext db)
    {
        _db = db;
    }

    public void OnGet(int id)
    {
        if (id > 0)
        {
            Category = _db.Category.Find(id)!;   // returns the founded data
            //Category = _db.Category.First(u => u.Id == id); // returns the founded first data if not returns exception
            //Category = _db.Category.FirstOrDefault(u => u.Id == id)!;    // returns the founded first data if not returns null
            //Category = _db.Category.Single(u => u.Id == id);    // returns the founded only data if not returns exception
            //Category = _db.Category.SingleOrDefault(u => u.Id == id)!;   // returns the founded only data if not returns null
            //Category = _db.Category.Where(u => u.Id == id).FirstOrDefault()!;    // returns all the results that matches, Then reuturn fist data or null
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
                await _db.Category.AddAsync(Category);
                message = $"The Category {Category.Name} is created successfully";
            }
            else
            {
                _db.Category.Update(Category);
                message = $"The Category {Category.Name} is updated successfully";
            }
            await _db.SaveChangesAsync();
            TempData["success"] = message;

            return RedirectToPage("Index");
        }

        return Page();
    }
}

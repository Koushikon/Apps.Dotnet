using System.ComponentModel.DataAnnotations;

namespace Library.Models;

public class CategoriesModel : BaseModel
{
    [Display(Name = "Category Name")]
    public string Name { get; set; }
}
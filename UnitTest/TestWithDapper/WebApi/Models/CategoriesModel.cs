using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class CategoriesModel : BaseModel
{
    [Display(Name = "Category Name")]
    public string Name { get; set; }
}
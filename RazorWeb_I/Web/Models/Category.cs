using System.ComponentModel.DataAnnotations;

namespace Web.Models;

/***
 * Data Annotation More Info: https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-7.0
 */

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    [Display(Name = "Display Order")]
    [Range(0, 100, ErrorMessage = "Display Order must be in range of 1 to 100.")]
    public int DisplayOrder { get; set; }
}

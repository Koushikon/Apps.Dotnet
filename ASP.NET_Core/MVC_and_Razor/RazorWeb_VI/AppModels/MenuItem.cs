using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppModels;

public class MenuItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(150, ErrorMessage = "Manu Item name must be between 150 characters.")]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    [StringLength(200)]
    public string? Image { get; set; }

    [Range(1, 1000, ErrorMessage = "Price should be between $1 to $1000")]
    public double Price { get; set; }

    /***
     * One way to implement Foreign key with EF.Core is:
     * Create a Foreign Table Obj like Category, with data annotation
     * [ForeignKey("<Foreign_Key_Name_for_Current_Table>")]
     */
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public Category Category { get; set; } = default!;

    /***
     * Another way to implement Foreign key with EF.Core is:
     * If re Create a Property With name "<Table_Name>Id" like FoodType Table Foreign key name "FoodTypeId"
     * EF.Core is smart enough to use that property as a Foreign Key of another Table
     */
    [Display(Name = "Food Type")]
    public int FoodTypeId { get; set; }

    public FoodType FoodType { get; set; } = default!;
}
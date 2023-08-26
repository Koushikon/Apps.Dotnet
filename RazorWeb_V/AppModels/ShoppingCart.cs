using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppModels;

public class ShoppingCart
{
    public int Id { get; set; }

    public int MenuItemId { get; set; }

    /***
     * [NotMapped] attribute can be applied to properties of an entity class for which
     * we do not want to create corresponding columns in the database.
     * This Property is used where we do not want any Navigation properties to be Mapped and populated
     * 
     * By default, EF creates a column for each property (must have get; & set;) in an entity class.
     * 
     * [ValidateNever] attribute ensures that the this property is never validated in ModelState
     * 
     * [NotMapped] attribute is not used for Navigation properties to be Mapped and populated
     */
    [ForeignKey("MenuItemId")]
    //[NotMapped]
    [ValidateNever]
    public MenuItem MenuItem { get; set; } = default!;

    [Range(1, 100, ErrorMessage = "Please select a count between 1 to 100.")]
    public int Count { get; set; }

    public string ApplicationUserId { get; set; } = default!;

    [ForeignKey("ApplicationUserId")]
    //[NotMapped]
    [ValidateNever]
    public ApplicationUser ApplicationUser { get; set; } = default!;
}
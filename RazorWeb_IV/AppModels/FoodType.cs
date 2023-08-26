using System.ComponentModel.DataAnnotations;

namespace AppModels;

public class FoodType
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(150, ErrorMessage = "Food Type name cannot exceed 150 characters.")]
    [MaxLength(150, ErrorMessage = "Food Type name cannot exceed 150 characters.")]
    public string Name { get; set; } = default!;
}

using System.ComponentModel.DataAnnotations;

namespace WebMvc.Models;

public class Car
{
    // [Key]: Denotes one or more properties that uniquely identify an entity.
    [Key]
    public int Id { get; set; }

    // [Required]: Specifies that a data field value is required.
    [Required]
    public string Model { get; set; } = string.Empty;

    [Length(5, 25)] // Supports C# 12
    public string? Brand { get; set; }

    // MaximumIsExclusive means maximum number also be considered
    // MinimumIsExclusive means minimum number also be considered
    [Range(25, 625, MaximumIsExclusive = true, MinimumIsExclusive = true)]  // Supports C# 12
    public int MPS { get; set; }
}
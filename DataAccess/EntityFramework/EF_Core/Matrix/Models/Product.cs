using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Matrix.Models;

public class Product
{
    // Key attribute would be considered the Primary Key of the Table
    // Its option If the property name is Id but if anything else its needed.
    [Key]
    public int Id { get; set; }

    // This column would not allow null
    public string Name { get; set; } = null!;

    // This column set the decimal data type with size (6,2)
    [Column(TypeName = "decimal(6, 2)")]
    public decimal Price { get; set; }
}

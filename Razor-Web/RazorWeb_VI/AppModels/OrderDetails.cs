using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppModels;

public class OrderDetails
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int OrderId { get; set; }

    [ForeignKey("OrderId")]
    public OrderHeader OrderHeader { get; set; } = default!;

    [Required]
    public int MenuItemId { get; set; }

    [ForeignKey("MenuItemId")]
    public MenuItem MenuItem { get; set; } = default!;

    public int Count { get; set; }

    [Required]
    public double Price { get; set; }

    public string Name { get; set; } = default!;
}
using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos.Stock;

public class StockRequestFormDto
{
    [Required]
    [Length(1, 10, ErrorMessage = "Symbol must be between 1 to 10 characters.")]
    public string Symbol { get; set; } = string.Empty;

    [Required]
    [Length(1, 10, ErrorMessage = "Symbol must be between 1 to 10 characters.")]
    public string CompanyName { get; set; } = string.Empty;

    [Required]
    [Range(1, 1000000000)]
    public decimal Purchase { get; set; }

    [Required]
    [Range(0.001, 100)]
    public decimal LastDiv { get; set; }

    [Required]
    [Length(1, 10, ErrorMessage = "Industry must be between 1 to 10 characters.")]
    public string Industry { get; set; } = string.Empty;

    [Range(1, 5000000000)]
    public long MarketCap { get; set; }
}
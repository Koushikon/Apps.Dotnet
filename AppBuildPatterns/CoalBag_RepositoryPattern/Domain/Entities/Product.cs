namespace Domain.Entities;

public class Product : CommonFields
{
	public int? Id { get; set; }
	public string? Name { get; set; }
	public string? Description { get; set; }
	public decimal? Price { get; set; }
    public int? Rank { get; set; }

    public IList<Product> ProductList { get; set; } = default!;
}
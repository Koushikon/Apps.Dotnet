namespace WebApi.Contracts;

public class OrderDto
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public List<string> Products { get; set; } = [];
    public decimal TotalAmount { get; set; }
}
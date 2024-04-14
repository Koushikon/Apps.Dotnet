namespace WebApi.Data.Models;

public class Order
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; }
    public List<string> Products { get; set; }
    public decimal TotalAmount { get; set; }
}
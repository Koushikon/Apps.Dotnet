namespace WebApi.Contracts;

public class OrderForCreateDto
{
    public string CustomerName { get; set; } = string.Empty;
    public List<string> Products { get; set; } = [];
    public decimal TotalAmount { get; set; }
}
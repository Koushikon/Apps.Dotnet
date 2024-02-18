namespace Web.Models;

public class Ticket
{
    public int? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsAgreeOnPrivacy { get; set; }
    public DateTime? IssuedOn { get; set; }
}
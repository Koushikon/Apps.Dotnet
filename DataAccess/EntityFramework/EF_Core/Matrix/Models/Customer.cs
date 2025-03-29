namespace Matrix.Models;

public class Customer
{
    // Id name propertywould be considered the Primary Key of the Table
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public ICollection<Order> Order { get; set; } = null!;
}

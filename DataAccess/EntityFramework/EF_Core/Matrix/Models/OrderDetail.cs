namespace Matrix.Models;

public class OrderDetail
{
    // Id name property would be considered the Primary Key of the Table
    public int Id { get; set; }

    public int Quantity { get; set; }

    // This will Implement Foreign Key relationship with Product class Id property
    public int ProductId { get; set; }

    // This will Implement Foreign Key relationship with Order class Id property
    public int OrderId { get; set; }


    public Order Order { get; set; } = null!;

    public Product Product { get; set; } = null!;
}
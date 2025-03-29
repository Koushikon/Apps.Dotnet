namespace Matrix.Models;

public class Order
{
    // Id name property would be considered the Primary Key of the Table
    public int Id { get; set; }

    public DateTime OrderPlaced { get; set; }

    public DateTime? OrderFulfilled { get; set; }

    // This will Implement Foreign Key relationship with Customer class Id property
    public int CustomerId { get; set; }


    public Customer Customer { get; set; } = null!;

    public ICollection<OrderDetail> OrderDetails { get; set; } = null!;
}
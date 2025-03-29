namespace Library;

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime DateOfBirth { get; set; }
    public double Discount { get; set; } = default!;
    public Address Address { get; set; } = new Address();
}

public class Address
{
    public int AddressId { get; set; }
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public string State { get; set; } = default!;
    public string ZipCode { get; set; } = default!;
}

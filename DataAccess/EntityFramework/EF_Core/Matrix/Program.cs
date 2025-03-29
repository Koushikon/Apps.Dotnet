using Matrix.Models;
using Matrix.Data;
using System.Diagnostics;

namespace Matrix;

public class Program
{
    public static void Main()
    {
        using PizzaBarContext context = new PizzaBarContext();

        // Create: operation
        //CreateProduct(context);

        // Update: operation
        //UpdateProduct(context, "Vegitable Special Pizza", 17.55m);

        // Read: operation
        //ReadAllProducts(context);

        // Delete: operation
        //DeleteProduct(context, "Vegitable Special Pizza");

        ReadAllProducts(context);
    }

    public static void CreateProduct(PizzaBarContext context)
    {
        Product product1 = new Product()
        {
            Name = "Vegitable Special Pizza",
            Price = 10.7m,
        };
        context.Products.Add(product1);


        Product product2 = new Product()
        {
            Name = "Chiken Pizza",
            Price = 34.5m,
        };
        context.Products.Add(product2);

        context.SaveChanges();
    }

    public static void ReadAllProducts(PizzaBarContext context)
    {
        // Fluent expression Query
        //var products = context.Products
        //    .Where(p => p.Price > 12.0m)
        //    .OrderBy(p => p.Name);

        // LINQ expression Query
        var products = from product in context.Products
                       where product.Price > 12.0m
                       orderby product.Name
                       select product;

        foreach (var item in products)
        {
            Console.WriteLine($"Id: {item.Id}");
            Console.WriteLine($"Name: {item.Name}");
            Console.WriteLine($"Price: {item.Price}");
            Console.WriteLine(new string('-', 20));
        }
    }

    public static void UpdateProduct(PizzaBarContext context, string name, decimal price)
    {
        var vegiSpecial = context.Products
            .Where(p => p.Name == name)
            .FirstOrDefault();

        if(vegiSpecial is not null)
        {
            vegiSpecial.Price = price;
        }

        context.SaveChanges();
    }

    public static void DeleteProduct(PizzaBarContext context, string name)
    {
        var vegiSpecial = context.Products
            .Where(p => p.Name == name)
            .FirstOrDefault();

        if (vegiSpecial is not null)
        {
            context.Remove(vegiSpecial);
        }

        context.SaveChanges();
    }
}
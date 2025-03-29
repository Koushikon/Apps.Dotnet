namespace WebMvc.Models;

public class Menu
{
    public Menu()
    {
        MenuItems = new List<MenuItem>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<MenuItem> MenuItems { get; set; }
}
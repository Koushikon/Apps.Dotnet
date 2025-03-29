using System.ComponentModel.DataAnnotations;

namespace WebMvc.Models;

public class Place
{
    // [Key]: Denotes one or more properties that uniquely identify an entity.
    [Key]
    public int Id { get; set; }

    public string? Location { get; set; }
}
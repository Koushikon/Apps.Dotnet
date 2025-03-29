using System.ComponentModel.DataAnnotations;

namespace WebMvc.Models;

public class Hotel
{
    // [Key]: Denotes one or more properties that uniquely identify an entity.
    [Key]
    public int Id { get; set; }

    // [DisplayFormat()]: Specifies how data fields are displayed and formatted by ASP.NET Dynamic Data.
    [DisplayFormat(NullDisplayText = "Empty name")]
    public string? Name { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMvc.Models;

public class Person
{
    // [Key]: Denotes one or more properties that uniquely identify an entity.
    [Key]
    public int Id { get; set; }

    // [StringLength(<minimum>, <maximum>)]: Specifies the minimum and maximum length of characters that are allowed in a data field.
    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
    public string Name { get; set; } = string.Empty;

    // [NotMapped]: Used to specify that an entity or property is not to be mapped to a table or column in the database
    [NotMapped]
    public List<Place> VisitedPlaces { get; set; } = new List<Place>();
}
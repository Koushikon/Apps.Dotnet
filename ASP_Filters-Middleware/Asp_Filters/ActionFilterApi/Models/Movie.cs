using ActionFilterApi.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActionFilterApi.Models;

[Table("Movie")]
public class Movie : IEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Genre is required")]
    public string Genre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Director is required")]
    public string Director { get; set; } = string.Empty;
}
using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class Doctor
{
    public int? Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public DateTime? AppoinmentDate { get; set; }
}
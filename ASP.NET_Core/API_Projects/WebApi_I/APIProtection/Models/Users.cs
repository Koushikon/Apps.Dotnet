
using System.ComponentModel.DataAnnotations;

namespace APIProtection.Models;

// With Microsoft data annotation validation

public class Users
{
    // Add validation after evaluating possible data scenarios

    [Required]
    [Range(1, int.MaxValue)]
    public int Id { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(100)]
    public string LastName { get; set; }

    [EmailAddress]
    public string EmailAddress { get; set; }

    [Phone] // Can be used as Country wise
    public string PhoneNumber { get; set; }

    [Url]
    public string HomePage { get; set; }

    [Range(0, 5)]
    public int NumberOfVehicles { get; set; }
}

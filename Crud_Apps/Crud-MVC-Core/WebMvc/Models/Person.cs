using System.ComponentModel.DataAnnotations;

namespace WebMvc.Models;

public class Person
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public int? EmployeeId { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Localize_1.Models;

public class PersonViewModel
{
    [Display(Name = "First Name")]
    [Required(ErrorMessage = "{0} is a required field.")]
    public string FirstName { get; set; } = string.Empty;

    [Display(Name = "Last Name")]
    [Required(ErrorMessage = "{0} is a required field.")]
    public string LastName { get; set; } = string.Empty;

    [Display(Name = "Age")]
    [Range(1, 100, ErrorMessage = "{0} must be a number between {1} and {2}.")]
    public int Age { get; set; }

    [Display(Name = "Date Of Join")]
    [DataType(DataType.Date, ErrorMessage = "Date only")]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? DateOfJoin { get; set; }
}
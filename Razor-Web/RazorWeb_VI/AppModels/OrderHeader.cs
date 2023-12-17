using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppModels;

public class OrderHeader
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = default!;

    [ForeignKey("UserId")]
    [ValidateNever]
    public ApplicationUser ApplicationUser { get; set; } = default!;

    [Required]
    public DateTime OrderDate { get; set; }

    [Required]
    [DisplayFormat(DataFormatString = "{0:C}")]
    [Display(Name = "Order Total")]
    public double OrderTotal { get; set; }

    [Required]
    [Display(Name = "Pick Up Time")]
    public DateTime PickUpTime { get; set; }

    [Required]
    [Display(Name = "Pick Up Date")]
    public DateTime PickUpDate { get; set; }

    public string Status { get; set; } = default!;

    public string? Comments { get; set; }

    public string? SessionId { get; set; }

    public string? PaymentIntentId { get; set; }

    [Required]
    [Display(Name = "PickUp Name")]
    public string PickUpName { get; set; } = default!;

    [Required]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = default!;
}
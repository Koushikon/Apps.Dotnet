using System.ComponentModel.DataAnnotations;

namespace Library.Models;

public class BaseModel
{
    public Guid CategoryUId { get; set; }

    [Display(Name = "Id")]
    public int CategoryIId { get; set; }

    [Display(Name = "Create Date")]
    public DateTime CreateDate { get; set; }

    [Display(Name = "Update Date")]
    public DateTime UpdateDate { get; set; }

    [Display(Name = "Delete Date")]
    public DateTime DeleteDate { get; set; }
}

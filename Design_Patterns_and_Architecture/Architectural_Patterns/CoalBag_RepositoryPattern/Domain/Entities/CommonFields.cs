namespace Domain.Entities;

public class CommonFields
{
    public DateTime? DateEntered { get; set; }
	public int? EnteredById { get; set; }
	public DateTime? DateUpdated { get; set; }
	public int? UpdatedById { get; set; }
    public bool? IsActive { get; set; }
}
namespace Library.Models;

public class Transfer
{
    public string TransferId { get; set; } = string.Empty;
    public int TransferAmount { get; set; }
    public string FromAccount { get; set; } = string.Empty;
	public string ToAccount { get; set; } = string.Empty;
}
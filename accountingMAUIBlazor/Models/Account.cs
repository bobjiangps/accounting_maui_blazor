namespace accountingMAUIBlazor.Models;

public class Account
{
    public int AccountId { get; set; }
    public string AccountName { get; set; }
    public decimal Amount { get; set; }
    public string Icon { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public int Currency { get; set; }
}

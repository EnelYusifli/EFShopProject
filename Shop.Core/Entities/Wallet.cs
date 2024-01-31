namespace Shop.Core.Entities;

public class Wallet:BaseEntities
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public string Number { get; set; } = null!;
    public string ExpirationDate { get; set; }=null!;
    public string CVC { get; set; }=null!;
    public decimal Balance { get; set; }
    public ICollection<Invoice>? Invoices { get; set; }
}

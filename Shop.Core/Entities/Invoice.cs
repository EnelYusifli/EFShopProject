using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class Invoice : BaseEntities
{
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }
    public Wallet Wallet { get; set; } = null!;
    public int WalletId { get; set; }
    public bool IsCanceled { get; set; } = false;
    public bool IsUnpaid { get; set; } = true;
    public bool IsPaid { get; set; } = false;
    public ICollection<ProductInvoice>? ProductInvoices { get; set; }
}

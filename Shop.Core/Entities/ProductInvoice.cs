namespace Shop.Core.Entities;

public class ProductInvoice:BaseEntities
{
    public Invoice Invoice { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public int InvoiceId { get; set; }
    public int ProductId { get; set; }
}

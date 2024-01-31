﻿namespace Shop.Core.Entities;

public class Invoice : BaseEntities
{
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }
    public Wallet Wallet { get; set; } = null!;
    public int WalletId { get; set; }
    public ICollection<ProductInvoice>? ProductInvoices { get; set; }
}
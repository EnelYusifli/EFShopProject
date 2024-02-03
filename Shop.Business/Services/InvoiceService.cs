using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess;

namespace Shop.Business.Services;

public class InvoiceService : IInvoiceService
{
    ShopDbContext context = new ShopDbContext();
    public void CreateInvoice(int walletId, User user, Product product, decimal totalPrice)
    {
        bool isExist = context.Wallets.Any(w => w.Id == walletId && w.UserId == user.Id);
        if (isExist)
        {
            Wallet wallet = context.Wallets.Find(walletId);
            if (wallet.Balance < totalPrice) throw new LessThanMinimumException("Balance of this cart is not enough");
            Invoice invoice = new Invoice()
            {
                TotalPrice = totalPrice,
                Wallet = wallet,
                WalletId = wallet.Id
            };
            ProductInvoice productInvoice = new()
            {
                InvoiceId = invoice.Id,
                ProductId = product.Id,
                Product = product,
                Invoice = invoice,
                //ProductCountInInvoice = 
            };
            product.AvailableCount -= productInvoice.ProductCountInInvoice;

            context.Invoices.Add(invoice);
            context.ProductInvoices.Add(productInvoice);
            context.SaveChanges();
            Console.Out.WriteLine("Bought Successfully");
            wallet.Balance -= totalPrice;
        }
        else throw new CannotBeFoundException("This card cannot be found in your wallet");
    }

}


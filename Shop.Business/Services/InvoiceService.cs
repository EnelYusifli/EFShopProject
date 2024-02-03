using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess;

namespace Shop.Business.Services;

public class InvoiceService : IInvoiceService
{
    ShopDbContext context = new ShopDbContext();

    public void CreateInvoice(int walletId, User user, Product product, decimal totalPrice, int count)
    {
        Wallet wallet = context.Wallets.FirstOrDefault(w => w.Id == walletId && w.UserId == user.Id);
        if (wallet != null)
        {
            if (wallet.Balance < totalPrice)
            {
                throw new LessThanMinimumException("Balance of this wallet is not enough");
            }
            if (product.AvailableCount < count)
            {
                throw new MoreThanMaximumException("The count is not available");
            }
            Invoice invoice = new Invoice()
            {
                TotalPrice = totalPrice,
                Wallet = wallet,
                WalletId = wallet.Id
            };
            ProductInvoice productInvoice = new ProductInvoice()
            {
                Invoice = invoice,
                InvoiceId = invoice.Id,
                ProductId = product.Id,
                ProductCountInInvoice = count
            };
            product.AvailableCount -= count;
            if (product.AvailableCount == 0)
            {
                product.IsDeactive = true;
            }
            context.Invoices.Add(invoice);
            context.ProductInvoices.Add(productInvoice);
            context.SaveChanges();
            wallet.Balance -= totalPrice;
            Console.Out.WriteLine("Purchase Successful");
        }
        else
        {
            throw new CannotBeFoundException("The wallet cannot be found in your account");
        }
    }

}

    


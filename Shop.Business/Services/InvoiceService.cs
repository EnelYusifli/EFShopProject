using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess;
using System.ComponentModel.Design;

namespace Shop.Business.Services;

public class InvoiceService : IInvoiceService
{
    ShopDbContext context = new ShopDbContext();

    public void CreateInvoice(int walletId, User user, List<Product> products, decimal totalPrice)
    {
        Wallet wallet = context.Wallets.FirstOrDefault(w => w.Id == walletId && w.UserId == user.Id);
        if (wallet != null)
        {
            if (wallet.Balance < totalPrice)
            {
                throw new LessThanMinimumException("Balance of this wallet is not enough");
            }
            Invoice invoice = new Invoice()
            {
                TotalPrice = totalPrice,
                Wallet = wallet,
                WalletId = wallet.Id
            };
            foreach (var product in products)
            {
                //if (product.AvailableCount < count)
                //    throw new MoreThanMaximumException("The count is not available");
                CartProduct cartProduct = context.CartProducts.Find(user.Id, product.Id);
                if (cartProduct is not null)
                {
                    ProductInvoice productInvoice = new ProductInvoice()
                    {
                        Invoice = invoice,
                        ProductId = product.Id,
                        //ProductCountInInvoice = count
                    };
                   // product.AvailableCount -= count;

                    if (product.AvailableCount == 0)
                                  product.IsDeactive = true;
                    context.ProductInvoices.Add(productInvoice);
                    wallet.Balance -= totalPrice;
                    Console.Out.WriteLine("Purchase Successful");
                    context.Invoices.Add(invoice);
                    cartProduct.IsDeactive = true;
                    context.SaveChanges();
                }
                else throw new CannotBeFoundException("User cannot br found");
            };
               
        }
        else
            throw new CannotBeFoundException("The wallet cannot be found in your account");
    }


}







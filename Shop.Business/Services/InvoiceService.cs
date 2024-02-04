using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess;

namespace Shop.Business.Services;

public class InvoiceService : IInvoiceService
{
    ShopDbContext context = new ShopDbContext();

    public void CreateInvoice(int walletId, User user, List<Product> products, decimal totalPrice)
    {
        if (user is not null && user.IsDeactive == false)
        {
            Wallet wallet = context.Wallets.FirstOrDefault(w => w.Id == walletId && w.UserId == user.Id && w.IsDeactive == false);
            if (wallet is not null)
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
                    CartProduct cartProduct = context.CartProducts.Find(user.Id, product.Id);
                    if (cartProduct is not null)
                    {
                        CreateProuctInvoice(invoice, product, cartProduct.ProductCountInCart);
                        wallet.Balance -= totalPrice;
                        cartProduct.ProductCountInCart = 0;
                        cartProduct.IsDeactive = true;
                    }
                    else throw new CannotBeFoundException("User cannot be found");
                };
                Console.Out.WriteLine("Purchase Successful");
                context.Invoices.Add(invoice);
                context.SaveChanges();
            }
            else
                throw new CannotBeFoundException("The wallet cannot be found in your account");
        }
        else throw new CannotBeFoundException("User cannot be found");
    }

    public void CreateProuctInvoice(Invoice invoice, Product product, int count)
    {
        //if (invoice is not null && product is not null && product.IsDeactive == false )
        //{
        if (product.AvailableCount >= count)
        {
            ProductInvoice productInvoice = new ProductInvoice()
            {
                Invoice = invoice,
                ProductId = product.Id,
                ProductCountInInvoice = count
            };
            product.AvailableCount -= count;
            context.ProductInvoices.Add(productInvoice);
            if (product.AvailableCount == 0)
            {
                product.IsDeactive = true;
                product.ModifiedTime = DateTime.Now;

            }
        }
        else throw new MoreThanMaximumException("The count is not available");

        //}
        //else throw new CannotBeFoundException("Value cannot be found");
    }
}







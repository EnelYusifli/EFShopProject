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
            Wallet wallet = context.Wallets.FirstOrDefault(w => w.Id == walletId && w.UserId == user.Id);
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
                    if (cartProduct is not null && cartProduct.IsDeactive==false)
                    {
                        if (product.AvailableCount >= cartProduct.ProductCountInCart)
                        {
                            ProductInvoice productInvoice = new ProductInvoice()
                            {
                                Invoice = invoice,
                                ProductId = product.Id,
                                ProductCountInInvoice = cartProduct.ProductCountInCart
                            };
                            product.AvailableCount -= cartProduct.ProductCountInCart;
                            context.ProductInvoices.Add(productInvoice);
                            if (product.AvailableCount == 0)
                            {
                                product.IsDeactive = true;
                                product.ModifiedTime = DateTime.Now;
                            }
                            context.Entry(product).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else throw new MoreThanMaximumException("The count is not available");
                        wallet.Balance -= totalPrice;
                        cartProduct.ProductCountInCart = 0;
                        cartProduct.IsDeactive = true;
                        context.Entry(cartProduct).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
}







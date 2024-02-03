using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess;

namespace Shop.Business.Services;

public class InvoiceService: IInvoiceService
{
    ShopDbContext context = new ShopDbContext();
    public void CreateInvoice(Wallet wallet,Cart cart,decimal totalPrice)
    {
        bool isExist = context.Wallets.Any(w => w.Id == wallet.Id && w.UserId==cart.Id);
        if (isExist)
        {
            if (cart.CartProducts.Any(c => c.CartId == cart.Id))
            {
                if (wallet.Balance < totalPrice) throw new LessThanMinimumException("Balance of this cart is not enough");
                Invoice invoice = new Invoice()
                {
                    TotalPrice = totalPrice,
                    Wallet = wallet,
                    WalletId = wallet.Id,
                };
                context.Invoices.Add(invoice);
                context.SaveChanges();
                Console.Out.WriteLine("Bought Successfully");
                wallet.Balance -= totalPrice;
            }
            else throw new CannotBeNullException("Your cart is empty");


        }
        else throw new CannotBeFoundException("This cart cannot be found in your wallet");
    }

}


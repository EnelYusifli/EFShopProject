using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess;

namespace Shop.Business.Services;

public class InvoiceService: IInvoiceService
{
    //ShopDbContext context = new ShopDbContext();
    //public void CreateInvoice(Wallet wallet, CartProduct cartProduct)
    //{
    //    bool isExist=context.Wallets.Any(w=>w.Id==wallet.Id);
    //    if (isExist)
    //    //{
    //    //    foreach (var item in collection)
    //    //    {

    //    //    }
    //        decimal price = cartProduct.ProductCountInCart * cartProduct.Product.Price;
    //    }
    //    else throw new CannotBeFoundException("You do not have any saved card");

    //}
}

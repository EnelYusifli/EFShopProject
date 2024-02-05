using Microsoft.EntityFrameworkCore;
using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess;

namespace Shop.Business.Services;

public class InvoiceService : IInvoiceService
{
    ShopDbContext context = new ShopDbContext();
    WalletService walletService = new();

    public void CreateInvoice(User user, List<Product> products, decimal total)
    {
        if (user is not null && user.IsDeactive == false)
        {
            if (products is not null)
            {
                Invoice invoice = new Invoice()
                {
                    TotalPrice = total
                };
                context.Add(invoice);
            }
            else throw new CannotBeFoundException("Product cannot be found");
        }
        else throw new CannotBeFoundException("User cannot be found");
    }

    public void BuyProduct(int walletId, User user, List<Product> products, decimal totalPrice)
    {
        if (user is not null && user.IsDeactive == false)
        {
            if (products is not null)
            {
                if (totalPrice <= 0) throw new LessThanMinimumException("Your Cart is empty");
                Invoice invoice = new Invoice()
                {
                    TotalPrice = totalPrice
                };
                context.Add(invoice);
                foreach (var product in products)
                {
                    CartProduct cartProduct = context.CartProducts.Find(user.Id, product.Id);
                    if (cartProduct is not null && cartProduct.IsDeactive == false)
                    {
                        if (product.AvailableCount >= cartProduct.ProductCountInCart)
                        {
                            product.AvailableCount -= cartProduct.ProductCountInCart;
                            if (product.AvailableCount == 0)
                            {
                                product.IsDeactive = true;
                                product.ModifiedTime = DateTime.Now;
                            }
                            context.Entry(product).State = EntityState.Modified;
                        }
                        else throw new MoreThanMaximumException("Count is not available");
                    }
                }
                try
                {
                    Wallet wallet = context.Wallets.FirstOrDefault(w => w.Id == walletId && w.UserId == user.Id);
                    if (totalPrice <= 0) throw new LessThanMinimumException("Your cart is empty");
                    if (wallet is not null && wallet.IsDeactive == false)
                    {
                        if (wallet.Balance < totalPrice)
                            throw new LessThanMinimumException("Balance of this wallet is not enough");
                        wallet.Balance -= totalPrice;
                        wallet.ModifiedTime = DateTime.Now;
                        context.Entry(wallet).State = EntityState.Modified;
                    }
                    else throw new CannotBeFoundException("You do not have any saved card");
                    invoice.IsUnpaid = false;
                    invoice.IsPaid = true;
                    invoice.IsCanceled = false;
                    invoice.WalletId=walletId;
                    foreach (var product in products)
                    {
                        CartProduct cartProduct = context.CartProducts.Find(user.Id, product.Id);
                        ProductInvoice productInvoice = new ProductInvoice()
                        {
                            Invoice = invoice,
                            ProductId = product.Id,
                            ProductCountInInvoice = cartProduct.ProductCountInCart
                        };
                        cartProduct.IsDeactive = true;
                        cartProduct.ModifiedTime = DateTime.Now;
                        context.ProductInvoices.Add(productInvoice);
                        context.Entry(product).State = EntityState.Modified;
                        context.Entry(cartProduct).State = EntityState.Modified;
                    }
                    Console.WriteLine("Purchase successful");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    invoice.IsUnpaid = false;
                    invoice.IsPaid = false;
                    invoice.IsCanceled = true;
                    foreach (var product in products)
                    {
                        CartProduct cartProduct = context.CartProducts.Find(user.Id, product.Id);
                        if (product.AvailableCount >= cartProduct.ProductCountInCart)
                        {
                            if (product.IsDeactive == true)
                            {
                                product.IsDeactive = false;
                                product.ModifiedTime = DateTime.Now;
                            }
                            product.AvailableCount += cartProduct.ProductCountInCart;
                            context.Entry(product).State = EntityState.Modified;
                        }
                    }
                }
                context.SaveChanges();
            }
            else throw new CannotBeFoundException("You do not have any saved card");
        }
        else throw new CannotBeFoundException("Product cannot be found");
    }
    //else throw new CannotBeFoundException("User cannot be found");

    //var wallets = context.Wallets.Where(w => w.UserId == user.Id && w.IsDeactive == false);
    //if (wallets.Any())
    //{
    //    foreach (var wallet in context.Wallets.Where(w => w.User == user && w.IsDeactive == false))
    //    {
    //        Console.WriteLine($"Id:{wallet.Id}/Number:{wallet.Number}\nBalance:{wallet.Balance}");
    //    }
    //    Console.WriteLine("\nChoose the card that you want to pay with:\n");
    //    int walletIdForPay = Convert.ToInt32(Console.ReadLine());
    //    try
    //    {
    //    walletService.PayForInvoice(walletIdForPay,user,totalPrice);
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.Message);
    //    }
    //    invoiceService.CreateInvoice(walletId, user, productsInCart, total);
    //    }
    //    else throw new CannotBeFoundException("You do not have any saved card");
    //}


    //public void CreateInvoice(int walletId, User user, List<Product> products, decimal totalPrice)
    //{
    //    if (user is not null && user.IsDeactive == false)
    //    {
    //        Wallet wallet = context.Wallets.FirstOrDefault(w => w.Id == walletId && w.UserId == user.Id);
    //        if (wallet is not null)
    //        {
    //            if (wallet.Balance < totalPrice)
    //            {
    //                throw new LessThanMinimumException("Balance of this wallet is not enough");
    //            }
    //            Invoice invoice = new Invoice()
    //            {
    //                TotalPrice = totalPrice,
    //                Wallet = wallet,
    //                WalletId = wallet.Id
    //            };
    //            foreach (var product in products)
    //            {
    //                CartProduct cartProduct = context.CartProducts.Find(user.Id, product.Id);
    //                if (cartProduct is not null && cartProduct.IsDeactive==false)
    //                {
    //                    if (product.AvailableCount >= cartProduct.ProductCountInCart)
    //                    {
    //                        ProductInvoice productInvoice = new ProductInvoice()
    //                        {
    //                            Invoice = invoice,
    //                            ProductId = product.Id,
    //                            ProductCountInInvoice = cartProduct.ProductCountInCart
    //                        };
    //                        product.AvailableCount -= cartProduct.ProductCountInCart;
    //                        context.ProductInvoices.Add(productInvoice);
    //                        if (product.AvailableCount == 0)
    //                        {
    //                            product.IsDeactive = true;
    //                            product.ModifiedTime = DateTime.Now;
    //                        }
    //                        context.Entry(product).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
    //                    }
    //                    else throw new MoreThanMaximumException("The count is not available");
    //                    wallet.Balance -= totalPrice;
    //                    cartProduct.ProductCountInCart = 0;
    //                    cartProduct.IsDeactive = true;
    //                    context.Entry(cartProduct).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    //                }
    //                else throw new CannotBeFoundException("User cannot be found");
    //            };
    //            Console.Out.WriteLine("Purchase Successful");
    //            context.Invoices.Add(invoice);
    //            context.SaveChanges();
    //        }
    //        else
    //            throw new CannotBeFoundException("The wallet cannot be found in your account");
    //    }
    //    else throw new CannotBeFoundException("User cannot be found");
}
//}







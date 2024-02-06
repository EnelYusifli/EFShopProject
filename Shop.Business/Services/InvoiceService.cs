using Microsoft.EntityFrameworkCore;
using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Business.Utilities.Helper;
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
                    TotalPrice = total,
                    WalletId = 4
                };
                context.Add(invoice);
                context.SaveChanges();
            }
            else throw new CannotBeFoundException("Product cannot be found");
        }
        else throw new CannotBeFoundException("User cannot be found");
    }

    public void BuyProduct(User user, List<Product> products, decimal totalPrice)
    {
        if (user is not null && user.IsDeactive == false)
        {
            if (products is not null)
            {
                if (totalPrice <= 0) throw new LessThanMinimumException("Your Cart is empty");
                Invoice invoice = new Invoice()
                {
                    TotalPrice = totalPrice,
                    WalletId = 4
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
                context.SaveChanges();
                Console.WriteLine("1)Pay For Products");
                Console.WriteLine("2)Cancel");
                string? option = Console.ReadLine();
                int intOption;
                bool isContinue = int.TryParse(option, out intOption);
                if (isContinue)
                {
                    if (intOption > 0 && intOption <= 2)
                    {
                        switch (intOption)
                        {
                            case (int)InvoiceEnum.BuyProducts:
                                try
                                {
                                    var wallets = context.Wallets.Where(w => w.UserId == user.Id && w.IsDeactive == false);
                                    if (wallets.Any())
                                    {
                                        foreach (var walletOfUser in context.Wallets.Where(w => w.User == user && w.IsDeactive == false))
                                        {
                                            Console.WriteLine($"Id:{walletOfUser.Id}/Number:{walletOfUser.Number}\nBalance:{walletOfUser.Balance}");
                                        }
                                        Console.WriteLine("\nChoose the card that you want to pay with:\n");
                                        int walletIdForPay = Convert.ToInt32(Console.ReadLine());

                                        Wallet wallet = context.Wallets.FirstOrDefault(w => w.Id == walletIdForPay && w.UserId == user.Id);
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
                                        invoice.WalletId = walletIdForPay;
                                        foreach (var product in products)
                                        {
                                            CartProduct cartProduct = context.CartProducts.Find(user.Id, product.Id);
                                            ProductInvoice productInvoice = new ProductInvoice()
                                            {
                                                Invoice = invoice,
                                                ProductId = product.Id,
                                                ProductCountInInvoice = cartProduct.ProductCountInCart
                                            };
                                            cartProduct.ProductCountInCart = 0;
                                            cartProduct.IsDeactive = true;
                                            cartProduct.ModifiedTime = DateTime.Now;
                                            context.ProductInvoices.Add(productInvoice);
                                            context.Entry(product).State = EntityState.Modified;
                                            context.Entry(cartProduct).State = EntityState.Modified;
                                        }
                                        Console.WriteLine("Purchase successful");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    invoice.IsUnpaid = false;
                                    invoice.IsPaid = false;
                                    invoice.IsCanceled = true;
                                    invoice.WalletId = 4; //admin's wallet
                                    foreach (var product in products)
                                    {
                                        CartProduct cartProduct = context.CartProducts.Find(user.Id, product.Id);

                                            if (product.IsDeactive == true)
                                            {
                                                product.IsDeactive = false;
                                                product.ModifiedTime = DateTime.Now;
                                            }
                                            product.AvailableCount += cartProduct.ProductCountInCart;
                                            context.Entry(product).State = EntityState.Modified;
                                    }
                                }
                                context.SaveChanges();
                                break;
                            case (int)InvoiceEnum.Cancel:
                                invoice.IsUnpaid = false;
                                invoice.IsPaid = false;
                                invoice.IsCanceled = true;
                                invoice.WalletId = 4; //admin's wallet
                                foreach (var product in products)
                                {
                                    CartProduct cartProduct = context.CartProducts.Find(user.Id, product.Id);
                                    if (product.IsDeactive == true)
                                    {
                                        product.IsDeactive = false;
                                        product.ModifiedTime = DateTime.Now;
                                    }
                                    product.AvailableCount += cartProduct.ProductCountInCart;
                                    context.Entry(product).State = EntityState.Modified;
                                }
                                //context.Entry(invoice).State = EntityState.Modified;
                                invoice.ModifiedTime = DateTime.Now;
                                context.SaveChanges();
                                break;
                        }
                    }
                    else Console.WriteLine("Invalid option. Please select again.");
                }
                else Console.WriteLine("Please enter correct format.");
            }
            else throw new CannotBeFoundException("Product cannot be found");
        }
        else throw new CannotBeFoundException("User cannot be found");
    }
   
}







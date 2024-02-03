﻿using Microsoft.EntityFrameworkCore;
using Shop.Business.Services;
using Shop.Business.Utilities.Exceptions;
using Shop.Business.Utilities.Helper;
using Shop.Core.Entities;
using Shop.DataAccess;
using System.Linq;

string appStart = "Application started...";
string Welcome = "Welcome!";
Console.SetCursorPosition((Console.WindowWidth - appStart.Length) / 2, Console.CursorTop);
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine(appStart);
Console.SetCursorPosition((Console.WindowWidth - Welcome.Length) / 2, Console.CursorTop);
Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine(Welcome);
Console.ResetColor();

bool isContinue = true;
User? user = null;
ShopDbContext context = new ShopDbContext();
UserService userService = new UserService();
ProductService productService = new ProductService();
WalletService walletService = new WalletService();
InvoiceService invoiceService = new InvoiceService();
while (isContinue)
{
    Console.WriteLine("\n1)Register");
    Console.WriteLine("2)Login\n");
    string? option = Console.ReadLine();
    int intOption;
    bool isInt = int.TryParse(option, out intOption);
    if (isInt)
    {
        if (intOption >= 0 && intOption <= 2)
        {
            switch (intOption)
            {
                case (int)LoginOrRegisterEnum.Register:
                    try
                    {
                        Console.WriteLine("Enter Name");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter Surname");
                        string surname = Console.ReadLine();
                        Console.WriteLine("Enter Age");
                        int? age = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Email");
                        string email = Console.ReadLine();
                        Console.WriteLine("Enter Password");
                        string password = Console.ReadLine();
                        Console.WriteLine("Enter UserName For App");
                        string username = Console.ReadLine();
                        Console.WriteLine("Enter Phone");
                        string phone = Console.ReadLine();
                        Console.WriteLine("Enter Address");
                        string address = Console.ReadLine();
                        userService.CreateUser(name, surname, age, email, password, username, phone, address);
                    YesOrNo:
                        Console.WriteLine("\nDo you want to continue? (yes/no)");
                        string? continueOption = Console.ReadLine();
                        if (continueOption is null || continueOption.ToLower() != "yes" && continueOption.ToLower() != "no")
                        {
                            Console.WriteLine("Please write yes/no");
                            goto YesOrNo;
                        }
                        if (continueOption.ToLower() == "no")
                            return;
                        if (continueOption.ToLower() == "yes")
                        {
                            user = userService.FindUserByEmail(email);
                            isContinue = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case (int)LoginOrRegisterEnum.Login:
                    Console.WriteLine("1)Login With Email");
                    Console.WriteLine("2)Login With Username");
                    Console.WriteLine("Select an option");
                    string? loginOption = Console.ReadLine();
                    int loginIntOption;
                    bool isIntLogin = int.TryParse(loginOption, out loginIntOption);
                    if (isIntLogin)
                    {
                        if (loginIntOption > 0 && loginIntOption <= 2)
                        {


                            switch (loginIntOption)
                            {
                                case (int)LoginMethod.LoginWithMail:
                                    try
                                    {
                                        Console.WriteLine("Enter Email");
                                        string email = Console.ReadLine();
                                        Console.WriteLine("Enter Password");
                                        string password = Console.ReadLine();
                                        userService.LoginUserWithEmail(email, password);
                                    YesOrNo:
                                        Console.WriteLine("\nDo you want to continue? (yes/no)");
                                        string? continueOption = Console.ReadLine();
                                        if (continueOption is null || continueOption.ToLower() != "yes" && continueOption.ToLower() != "no")
                                        {
                                            Console.WriteLine("Please write yes/no");
                                            goto YesOrNo;
                                        }
                                        if (continueOption.ToLower() == "no")
                                            return;
                                        if (continueOption.ToLower() == "yes")
                                        {
                                            user = userService.FindUserByEmail(email);
                                            isContinue = false;
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                case (int)LoginMethod.LoginWithUsername:
                                    try
                                    {
                                        Console.WriteLine("Enter Username");
                                        string username = Console.ReadLine();
                                        Console.WriteLine("Enter Password");
                                        string password = Console.ReadLine();
                                        userService.LoginUserWithUsername(username, password);
                                    YesOrNo:
                                        Console.WriteLine("\nDo you want to continue? (yes/no)");
                                        string? continueOption = Console.ReadLine();
                                        if (continueOption is null || continueOption.ToLower() != "yes" && continueOption.ToLower() != "no")
                                        {
                                            Console.WriteLine("Please write yes/no");
                                            goto YesOrNo;
                                        }
                                        if (continueOption.ToLower() == "no")
                                            return;
                                        if (continueOption.ToLower() == "yes")
                                        {
                                            user = userService.FindUserByUsername(username);
                                            isContinue = false;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                            }
                        }
                        else Console.WriteLine("Invalid option. Please select again.");
                    }
                    else Console.WriteLine("Please enter correct format");
                    break;
            }
        }
        else Console.WriteLine("Invalid option. Please select again.");
    }
    else Console.WriteLine("Please enter correct format");
}
bool isMainPageContinue = true;
Console.WriteLine("\nWe wish you a pleasant experience :))\n");
while (isMainPageContinue)
{
    Console.WriteLine("\n1)Go To HomePage");
    Console.WriteLine("2)Go To Cart");
    Console.WriteLine("3)See UserInfo");
    Console.WriteLine("0)Exit\n");
    string? option = Console.ReadLine();
    int intOption;
    bool isInt = int.TryParse(option, out intOption);
    if (isInt)
    {
        if (intOption > 0 && intOption <= 3)
        {
            switch (intOption)
            {
                case (int)MainPage.GoToHomePage:

                    foreach (var product in context.Products.OrderByDescending(p => p.CreatedDate).Take(5).Where(p => p.IsDeactive == false))
                    {
                        Console.Write($"\n Id:{product.Id}/Name:{product.Name.ToUpper()},\n" +
                            $"Price:{product.Price},\n" +
                            $"Description:{product.Description},\n" +
                            $"Available Count:{product.AvailableCount}\n\n");
                    }
                    bool isContinueHomePage = true;
                    while (isContinueHomePage)
                    {
                        Console.WriteLine("1)Continue Scrolling");
                        Console.WriteLine("2)Add Product To Cart");
                        Console.WriteLine("0)Return to main page");
                        string? homePageOption = Console.ReadLine();
                        int homePageIntOption;
                        bool isIntHomePage = int.TryParse(homePageOption, out homePageIntOption);
                        if (isIntHomePage)
                        {
                            if (homePageIntOption >= 0 && homePageIntOption <= 2)
                            {
                                int n = 1;
                                switch (homePageIntOption)
                                {
                                    case (int)HomePage.ContinueScrolling:
                                        foreach (var product in context.Products
                                            .OrderByDescending(p => p.CreatedDate)
                                            .Skip(5 * n)
                                            .Take(5)
                                            .Where(p => p.IsDeactive == false))
                                        {
                                            Console.Write($"\n Id:{product.Id}/Name:{product.Name.ToUpper()},\n" +
                                                $"Price:{product.Price},\n" +
                                                $"Description:{product.Description},\n" +
                                                $"Available Count:{product.AvailableCount}\n\n");
                                            n++;
                                        }
                                        break;
                                    case (int)HomePage.AddProductToCart:
                                        try
                                        {
                                            Console.WriteLine("Please enter product id");
                                            int productId = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("And the count you want to add");
                                            int productCount = Convert.ToInt32(Console.ReadLine());
                                            productService.AddProductToCart(productId, user, productCount);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;
                                    default:
                                        isContinueHomePage = false;
                                        break;
                                }
                            }
                            else Console.WriteLine("Invalid option. Please select again.");
                        }
                        else Console.WriteLine("Please enter correct format");
                    }
                    break;
                case (int)MainPage.GoToCart:
                    try
                    {
                        var products = context.Products
                                        .Where(p => p.CartProducts.Any(cp => cp.CartId == user.Id))
                                        .ToList();

                        if (products is not null)
                        {
                            decimal total = 0;
                            foreach (var product in products)
                            {
                                CartProduct? cartProduct = await context.CartProducts
                                                .FindAsync(user.Id, product.Id);

                                Console.Write($"\n Name: {product.Name.ToUpper()},\n" +
                                              $"Price: {product.Price},\n" +
                                              $"Description: {product.Description}\n" +
                                              $"Count In cart:{cartProduct.ProductCountInCart}\n");

                                total += product.Price * cartProduct.ProductCountInCart;

                            }
                            Console.WriteLine($"\nTotal Price ${total}\n");

                            bool isContinueCart = true;
                            while (isContinueCart)
                            {
                                Console.WriteLine("1)Buy All Items in the cart");
                                Console.WriteLine("2)Select items to buy");
                                Console.WriteLine("0)Return to main page");
                                string? cartOption = Console.ReadLine();
                                int cartIntOption;
                                bool isIntHomePage = int.TryParse(cartOption, out cartIntOption);
                                if (isIntHomePage)
                                {
                                    if (cartIntOption >= 0 && cartIntOption <= 2)
                                    {
                                        switch (cartIntOption)
                                        {
                                            case (int)CartEnum.BuyAllProducts:
                                                try
                                                {
                                                    //var wallets = context.Wallets.Where(w => w.UserId == user.Id);
                                                    //if (wallets.Any())
                                                    //{
                                                    //    foreach (var wallet in context.Wallets.Where(w => w.User == user))
                                                    //    {
                                                    //        Console.WriteLine($"Id:{wallet.Id}/Balance:{wallet.Balance}");
                                                    //    }
                                                    //// walletService.WalletsOfUser(user);
                                                    //Console.WriteLine("\nChoose the card that you want to pay with:\n");
                                                    //int walletId = Convert.ToInt32(Console.ReadLine());
                                                    //List<CartProduct>? cartProducts =  context.CartProducts.Where(cp => cp.CartId == user.Id).Include(cp => cp.Product).ToList();
                                                    //foreach (var cartProduct in cartProducts)
                                                    //{
                                                    //    Product product = cartProduct.Product;
                                                    //    invoiceService.CreateInvoice(walletId, user, product, total);
                                                    //}
                                                    //}
                                                    //else throw new CannotBeFoundException("You do not have any saved cart");
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }
                                                break;
                                            case (int)CartEnum.SelectItemsToBuy:

                                                foreach (var product in products)
                                                {
                                                    CartProduct? cartProduct = await context.CartProducts
                                                                    .FindAsync(user.Id, product.Id);

                                                    Console.Write($"\nId:{product.Id}/ Name: {product.Name.ToUpper()},\n" +
                                                                  $"Price: {product.Price},\n" +
                                                                  $"Description: {product.Description}\n" +
                                                                  $"Count In cart:{cartProduct.ProductCountInCart}\n");
                                                }



                                                    //var wallets = context.Wallets.Where(w => w.UserId == user.Id);
                                                    //if (wallets.Any())
                                                    //{
                                                    //    foreach (var wallet in context.Wallets.Where(w => w.User == user))
                                                    //    {
                                                    //        Console.WriteLine($"Id:{wallet.Id}/Balance:{wallet.Balance}");
                                                    //    }
                                                    //    // walletService.WalletsOfUser(user);
                                                    //    Console.WriteLine("\nChoose the card that you want to pay with:\n");
                                                    //    int walletId = Convert.ToInt32(Console.ReadLine());
                                                    //    List<CartProduct>? cartProducts = context.CartProducts.Where(cp => cp.CartId == user.Id).Include(cp => cp.Product).ToList();
                                                    //    foreach (var cartProduct in cartProducts)
                                                    //    {
                                                    //        Product product = cartProduct.Product;
                                                    //        invoiceService.CreateInvoice(walletId, user, product, total);
                                                    //    }
                                                    //}
                                                    //else throw new CannotBeFoundException("You do not have any saved cart");
                                                    break;
                                            case (int)CartEnum.ReturnToMainPage:
                                                isContinueCart = false;
                                                break;

                                        }


                                    }
                                    else Console.WriteLine("You do not have any product in your cart");
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                    break;
                case (int)MainPage.SeeUserInfo:
                    Console.WriteLine($"\nName:{user.Name.ToUpper()}\n" +
                        $"Surname:{user.Surname.ToUpper()}\n" +
                        $"Email:{user.Email.ToLower()}\n" +
                        $"Username:{user.UserName}\n" +
                        $"Phone:{user.Phone}\n" +
                        $"Address:{user.Address}\n");
                    bool hasWallet = context.Wallets.Where(w => w.UserId == user.Id).Any();
                    if (hasWallet)
                    {
                        foreach (var wallet in context.Wallets.Where(w => w.UserId == user.Id))
                        {
                            Console.WriteLine($"Card:{wallet.Number}\n" +
                                              $"Balance:{wallet.Balance}");
                        }

                    }
                    else Console.WriteLine("You do not have any saved card");
                    bool isUserInfoContinue = true;
                    while (isUserInfoContinue)
                    {
                        Console.WriteLine("\n1)Update User Details");
                        Console.WriteLine("2)Add New Card");
                        Console.WriteLine("0)Return to main page\n");
                        string? homePageOption = Console.ReadLine();
                        int homePageIntOption;
                        bool isIntHomePage = int.TryParse(homePageOption, out homePageIntOption);
                        if (isIntHomePage)
                        {
                            if (homePageIntOption >= 0 && homePageIntOption <= 2)
                            {
                                switch (homePageIntOption)
                                {
                                    case (int)UserInfo.UpdateUserInfo:

                                        break;

                                    case (int)UserInfo.AddNewCard:
                                        try
                                        {
                                            Console.WriteLine("Enter card number");
                                            string cardNumber = Console.ReadLine();
                                            Console.WriteLine("Enter CVC");
                                            string cvc = Console.ReadLine();
                                            Console.WriteLine("Enter balance");
                                            decimal balance = Convert.ToDecimal(Console.ReadLine());
                                            walletService.CreateCard(user, cardNumber, cvc, balance);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;
                                    case (int)UserInfo.ReturnToHomePage:
                                        isUserInfoContinue = false;
                                        break;
                                }
                            }
                            else Console.WriteLine("Invalid option. Please select again.");
                        }
                        else Console.WriteLine("Please enter correct format");
                    }
                    break;
            }
        }
    }
}



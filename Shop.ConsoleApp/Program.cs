﻿using Shop.Business.Services;
using Shop.Business.Utilities.Helper;
using Shop.Core.Entities;
using Shop.DataAccess;

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
isContinue = true;
Console.WriteLine("\nWe wish you a pleasant experience :))\n");
while (isContinue)
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
                        Console.Write($"\n Name:{product.Name.ToUpper()},\n" +
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
                                            .Skip(5*n)
                                            .Take(5)
                                            .Where(p => p.IsDeactive == false))
                                        {
                                            Console.Write($"\n Name:{product.Name.ToUpper()},\n" +
                                                $"Price:{product.Price},\n" +
                                                $"Description:{product.Description},\n" +
                                                $"Available Count:{product.AvailableCount}\n\n");
                                            n++;
                                        }
                                        break;
                                    case (int)HomePage.AddProductToCart:
                                        try
                                        {
                                            Console.WriteLine("Please enter product name");
                                            string productName = Console.ReadLine();
                                            Console.WriteLine("And the count you want to add");
                                            int productCount = Convert.ToInt32(Console.ReadLine());
                                            productService.AddProductToCart(productName, user, productCount);
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
                        CartProduct? cartProduct = context.CartProducts.FirstOrDefault(cp=>cp.CartId==user.Id);
                        if (cartProduct is not null)
                        {
                            foreach (var product in context.Products.Where(p => p.CartProducts.Any(cp => cp.CartId == user.Id)))
                            {
                                Console.Write($"\n Name: {product.Name.ToUpper()},\n" +
                                              $"Price: {product.Price},\n" +
                                              $"Description: {product.Description},\n");
                            }

                        }
                        else Console.WriteLine("You do not have any product in your cart");
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



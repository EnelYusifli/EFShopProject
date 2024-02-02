// See https://aka.ms/new-console-template for more information
using Shop.Business.Services;
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
}

isContinue = true;
while (isContinue)
{
    Console.WriteLine("\n1)Add Product");
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
                case (int)AdminPanel.CreateProduct:
                    Console.WriteLine("name");
                    string name = Console.ReadLine();
                    Console.WriteLine("desc");
                    string description = Console.ReadLine();
                    Console.WriteLine("price");
                    decimal price = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("count");
                    int count = Convert.ToInt32(Console.ReadLine());
                    
                    productService.CreateProduct(name, description, price, count);
                    break;
            }
        }
    }
}



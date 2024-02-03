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
CategoryService categoryService = new CategoryService();
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
                        userService.LoginAdminWithEmail(email, password);
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
                        userService.LoginAdminWithUsername(username, password);
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
    Console.WriteLine("\n1)Create Product");
    Console.WriteLine("2)Create Category");
    Console.WriteLine("3)Deactivate User");
    Console.WriteLine("4)Activate User");
    Console.WriteLine("5)Deactivate Product");
    Console.WriteLine("6)Activate Product");
    Console.WriteLine("0)Exit\n");
    string? option = Console.ReadLine();
    int intOption;
    bool isInt = int.TryParse(option, out intOption);
    if (isInt)
    {
        if (intOption > 0 && intOption <= 6)
        {
            switch (intOption)
            {
                case (int)AdminPanel.CreateProduct:
                    try
                    {
                        Console.WriteLine("Enter name");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter description");
                        string description = Console.ReadLine();
                        Console.WriteLine("Enter price");
                        decimal price = Convert.ToDecimal(Console.ReadLine());
                        Console.WriteLine("Enter availability count");
                        int count = Convert.ToInt32(Console.ReadLine());
                        foreach (var category in context.Categories)
                        {
                            Console.WriteLine($"\nId:{category.Id}\n" +
                                $"Name:{category.Name.ToUpper()}\n");
                        }
                        Console.WriteLine("Enter Category Id");
                        int categoryId = Convert.ToInt32(Console.ReadLine());
                        productService.CreateProduct(name, description, price, count, categoryId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                 
                    break;
                case (int)AdminPanel.CreateCategory:
                    try
                    {
                        Console.WriteLine("Enter Name");
                        string? categoryName = Console.ReadLine();
                        Console.WriteLine("Enter description");
                        string? categoryDescription = Console.ReadLine();
                        categoryService.CreateCategory(categoryName, categoryDescription);
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case (int)AdminPanel.DeactivateUser:
                    try
                    {
                        foreach (var userForDeactivate in context.Users.Where(u => u.IsDeactive==false))
                        {
                            Console.WriteLine($"\nId:{userForDeactivate.Id}/Name:{userForDeactivate.Name.ToUpper()}\n");
                        }
                            Console.WriteLine("\nEnter user id");
                            int userId= Convert.ToInt32(Console.ReadLine());
                            userService.DeactivateUser(userId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case (int)AdminPanel.ActivateUser:
                    try
                    {
                        foreach (var userForActivate in context.Users.Where(u => u.IsDeactive == true))
                        {
                            Console.WriteLine($"\nId:{userForActivate.Id}/Name:{userForActivate.Name.ToUpper()}\n");
                        }
                        Console.WriteLine("\nEnter user id");
                        int userId = Convert.ToInt32(Console.ReadLine());
                        userService.ActivateUser(userId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case (int)AdminPanel.DeactivateProduct:
                    try
                    {
                        foreach (var productForDeactivate in context.Products.Where(p => p.IsDeactive == false))
                        {
                            Console.WriteLine($"\nId:{productForDeactivate.Id}/Name:{productForDeactivate.Name.ToUpper()}\n");
                        }
                        Console.WriteLine("\nEnter product id");
                        int productId = Convert.ToInt32(Console.ReadLine());
                        productService.DeactivateProduct(productId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case (int)AdminPanel.ActivateProduct:
                    try
                    {
                        foreach (var productForDeactivate in context.Products.Where(p => p.IsDeactive == true))
                        {
                            Console.WriteLine($"\nId:{productForDeactivate.Id}/Name:{productForDeactivate.Name.ToUpper()}\n");
                        }
                        Console.WriteLine("\nEnter product id");
                        int productId = Convert.ToInt32(Console.ReadLine());
                        productService.ActivateProduct(productId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                default:
                    isContinue= false;
                    break;
            }
        }
    }
}



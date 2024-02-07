using Microsoft.EntityFrameworkCore;
using Shop.Business.Services;
using Shop.Business.Utilities.Helper;
using Shop.Core.Entities;
using Shop.DataAccess;

string Welcome = "Welcome!";
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
CartService cartService = new CartService();
InvoiceService invoiceService = new InvoiceService();
while (isContinue)
{
    Console.WriteLine("\n1)Register  \t  2)Login\n");
    Console.WriteLine("Choose an option");
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
                        string password = userService.ReadPasswordFromConsole();
                        string hashedPassword = userService.HashPassword(password);
                        Console.WriteLine("Enter UserName For App");
                        string username = Console.ReadLine();
                        Console.WriteLine("Enter Phone");
                        string phone = Console.ReadLine();
                        Console.WriteLine("Enter Address");
                        string address = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        userService.CreateUser(name, surname, age, email, password, username, phone, address);
                        Console.ResetColor();
                    YesOrNo:
                        Console.WriteLine("\nDo you want to continue? (yes/no)\n");
                        string? continueOption = Console.ReadLine();
                        if (continueOption is null || continueOption.ToLower() != "yes" && continueOption.ToLower() != "no")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
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
                    Console.WriteLine("\n1)Login With Email \t 2)Login With Username\n");
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
                                        string password = userService.ReadPasswordFromConsole();
                                        string hashedPassword = userService.HashPassword(password);
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        userService.LoginUserWithEmail(email, password);
                                        Console.ResetColor();
                                    YesOrNo:
                                        Console.WriteLine("\nDo you want to continue? (yes/no)");
                                        string? continueOption = Console.ReadLine();
                                        if (continueOption is null || continueOption.ToLower() != "yes" && continueOption.ToLower() != "no")
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Please write yes/no");
                                            Console.ResetColor();
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
                                        string password = userService.ReadPasswordFromConsole();
                                        string hashedPassword = userService.HashPassword(password);
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        userService.LoginUserWithUsername(username, password);
                                        Console.ResetColor();
                                    YesOrNo:
                                        Console.WriteLine("\nDo you want to continue? (yes/no)");
                                        string? continueOption = Console.ReadLine();
                                        if (continueOption is null || continueOption.ToLower() != "yes" && continueOption.ToLower() != "no")
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Please write yes/no");
                                            Console.ResetColor();
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
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine(ex.Message);
                                        Console.ResetColor();
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
Console.ForegroundColor = ConsoleColor.DarkMagenta;
Console.WriteLine("\n                          We wish you a pleasant experience :))\n");
Console.ResetColor();
while (isMainPageContinue)
{
    Console.WriteLine("\n1)Go To HomePage");
    Console.WriteLine("2)Go To Cart");
    Console.WriteLine("3)See UserInfo");
    Console.WriteLine("0)Exit\n");
    Console.WriteLine("Choose an option");
    string? option = Console.ReadLine();
    int intOption;
    bool isInt = int.TryParse(option, out intOption);
    if (isInt)
    {
        if (intOption >= 0 && intOption <= 3)
        {
            switch (intOption)
            {
                case 0:
                    isMainPageContinue = false;
                    break;
                case (int)MainPage.GoToHomePage:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    foreach (var product in context.Products.OrderByDescending(p => p.CreatedDate).Take(5).Where(p => p.IsDeactive == false))
                    {
                        Console.Write($"\n Id:{product.Id}/Name:{product.Name.ToUpper()},\n" +
                            $"Price:{product.Price},\n" +
                            $"Description:{product.Description},\n" +
                            $"Available Count:{product.AvailableCount}\n\n");
                    }
                    Console.ResetColor();
                    bool isContinueHomePage = true;
                    while (isContinueHomePage)
                    {
                        Console.WriteLine("\n1)Continue Scrolling");
                        Console.WriteLine("2)Add Product To Cart");
                        Console.WriteLine("3)Search Product By Name");
                        Console.WriteLine("4)Search Product By Category");
                        Console.WriteLine("5)Search Product By Brand");
                        Console.WriteLine("0)Return to main page\n");
                        Console.WriteLine("Choose an option");
                        string? homePageOption = Console.ReadLine();
                        int homePageIntOption;
                        bool isIntHomePage = int.TryParse(homePageOption, out homePageIntOption);
                        if (isIntHomePage)
                        {
                            if (homePageIntOption >= 0 && homePageIntOption <= 5)
                            {
                                int n = 1;
                                switch (homePageIntOption)
                                {
                                    case (int)HomePage.ContinueScrolling:
                                        Console.ForegroundColor = ConsoleColor.Yellow;
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
                                        Console.ResetColor();
                                        break;
                                    case (int)HomePage.AddProductToCart:
                                        try
                                        {
                                            Console.WriteLine("Please enter product id");
                                            int productId = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("And the count you want to add");
                                            int productCount = Convert.ToInt32(Console.ReadLine());
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            cartService.AddProductToCart(productId, user, productCount);
                                            Console.ResetColor();
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine(ex.Message);
                                            Console.ResetColor();
                                        }
                                        break;
                                    case (int)HomePage.SearchProductViaName:
                                        Console.WriteLine("Enter product name");
                                        string name = Console.ReadLine();
                                        try
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            productService.SearchForProductViaName(name);
                                            Console.ResetColor();
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine(ex.Message);
                                            Console.ResetColor();
                                        }
                                        break;
                                    case (int)HomePage.SearchProductViaCategory:
                                        Console.WriteLine("Enter category name");
                                        string categoryName = Console.ReadLine();
                                        try
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            productService.SearchProductsViaCategory(categoryName);
                                            Console.ResetColor();
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine(ex.Message);
                                            Console.ResetColor();
                                        }
                                        break;
                                    case (int)HomePage.SearchProductViaBrand:
                                        Console.WriteLine("Enter brand name");
                                        string brandName = Console.ReadLine();
                                        try
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            productService.SearchProductsViaBrand(brandName);
                                            Console.ResetColor();
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine(ex.Message);
                                            Console.ResetColor();
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
                       
                        var cartProductsInCart = context.CartProducts
                                .Where(cp => cp.Cart.Id == user.Id && !cp.IsDeactive && cp.ProductCountInCart != 0)
                                .Select(cp => new
                                {
                                    ProductId = cp.Product.Id,
                                    ProductName = cp.Product.Name.ToUpper(),
                                    ProductPrice = cp.Product.Price,
                                    ProductDescription = cp.Product.Description,
                                    ProductCountInCart = cp.ProductCountInCart
                                })
                                .ToList();

                        decimal total = 0;
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        foreach (var cartProduct in cartProductsInCart)
                        {
                            Console.Write($"\nId:{cartProduct.ProductId} Name: {cartProduct.ProductName},\n" +
                                          $"Price: {cartProduct.ProductPrice},\n" +
                                          $"Description: {cartProduct.ProductDescription}\n" +
                                          $"Count In cart:{cartProduct.ProductCountInCart}\n");
                            total += cartProduct.ProductPrice * cartProduct.ProductCountInCart;
                        }
                        Console.ResetColor();
                        Console.WriteLine($"\nTotal Price ${total}\n");
                        bool isContinueCart = true;
                        while (isContinueCart)
                        {
                            Console.WriteLine("\n1)Buy All Items in the cart");
                            Console.WriteLine("2)Remove Product");
                            Console.WriteLine("0)Return to main page\n");
                            Console.WriteLine("Choose an option");
                            string? cartOption = Console.ReadLine();
                            int cartIntOption;
                            bool isIntHomePage = int.TryParse(cartOption, out cartIntOption);
                            if (isIntHomePage)
                            {
                                if (cartIntOption >= 0 && cartIntOption <= 3)
                                {
                                    switch (cartIntOption)
                                    {
                                        case (int)CartEnum.BuyAllProducts:
                                            HandleBuyAllProducts(user, total, context, invoiceService);
                                            isContinueCart = false;
                                            break;

                                        case (int)CartEnum.RemoveProduct:
                                            HandleRemoveProduct(user, context, cartService);
                                            isContinueCart = false;
                                            break;

                                        case (int)CartEnum.ReturnToMainPage:
                                            isContinueCart = false;
                                            break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid option. Please select again.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Please enter correct format");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                    void HandleBuyAllProducts(User user, decimal total, ShopDbContext context, InvoiceService invoiceService)
                    {
                        List<Product> productsInCart = context.Products.Where(p => p.CartProducts.Where(cp => !cp.IsDeactive).Any(cp => cp.CartId == user.Id)).ToList();
                        try
                        {
                            invoiceService.BuyProduct(user, productsInCart, total);
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                        }
                    }

                    void HandleRemoveProduct(User user, ShopDbContext context, CartService cartService)
                    {
                        try
                        {
                            Console.WriteLine("Enter Product Id");
                            int productId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter the count that you want to remove");
                            int count = Convert.ToInt32(Console.ReadLine());
                            Console.ForegroundColor = ConsoleColor.Green;
                            cartService.RemoveProductFromCart(productId, user, count);
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                        }
                    }
                case (int)MainPage.SeeUserInfo:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nName:{user.Name.ToUpper()}\n" +
                        $"Surname:{user.Surname.ToUpper()}\n" +
                        $"Email:{user.Email.ToLower()}\n" +
                        $"Username:{user.UserName}\n" +
                        $"Phone:{user.Phone}\n" +
                        $"Address:{user.Address}\n");
                    Console.ResetColor();
                    var userWallets = context.Wallets
                               .Where(w => w.UserId == user.Id && !w.IsDeactive)
                               .Select(cw => new
                               {
                                  UserId=user.Id,
                                  Number=cw.Number,
                                  CVC=cw.CVC,
                                  Balance=cw.Balance,
                                  Id = cw.Id,
                               })
                               .ToList();
                    bool hasWallet = context.Wallets.Where(w => w.UserId == user.Id).Any();
                    if (hasWallet)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        foreach (var wallet in userWallets)
                        {
                            Console.WriteLine($"Card:{wallet.Number}\n" +
                                              $"Balance:{wallet.Balance}\n");
                        }
                        Console.ResetColor();

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("You do not have any saved card");
                        Console.ResetColor();
                    }


                    bool isUserInfoContinue = true;
                    while (isUserInfoContinue)
                    {
                        Console.WriteLine("\n1)Update User Details");
                        Console.WriteLine("2)Add New Card");
                        Console.WriteLine("3)Remove Card");
                        Console.WriteLine("4)Add Removed Card");
                        Console.WriteLine("5)Increase Card balance");
                        Console.WriteLine("6)Transfer money");
                        Console.WriteLine("7)Show Bought products");
                        Console.WriteLine("0)Return to main page\n");
                        Console.WriteLine("Choose an option");
                        string? homePageOption = Console.ReadLine();
                        int homePageIntOption;
                        bool isIntHomePage = int.TryParse(homePageOption, out homePageIntOption);
                        if (isIntHomePage)
                        {
                            if (homePageIntOption >= 0 && homePageIntOption <= 7)
                            {
                                switch (homePageIntOption)
                                {
                                    case (int)UserInfo.UpdateUserInfo:
                                        bool isContinueUpdateUser = true;
                                        while (isContinueUpdateUser)
                                        {
                                            Console.WriteLine("1)Update Name");
                                            Console.WriteLine("2)Update Surname");
                                            Console.WriteLine("3)Update Email");
                                            Console.WriteLine("4)Update Username");
                                            Console.WriteLine("5)Update Password");
                                            Console.WriteLine("6)Update Phone");
                                            Console.WriteLine("7)Update Address");
                                            Console.WriteLine("0)Return to user details page");
                                            string? updateUserOption = Console.ReadLine();
                                            int updateUserIntOption;
                                            bool isIntUpdateUser = int.TryParse(updateUserOption, out updateUserIntOption);
                                            if (isIntUpdateUser)
                                            {
                                                if (updateUserIntOption >= 0 && updateUserIntOption <= 7)
                                                {
                                                    switch (updateUserIntOption)
                                                    {
                                                        case (int)UserUpdateEnum.UpdateName:
                                                        name:
                                                            Console.WriteLine("Enter new Name:");
                                                            string name = Console.ReadLine();
                                                            if (name is not null)
                                                            {
                                                                if (user.Name.ToLower() != name.ToLower())
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                                    userService.UpdateUser(user, name, user.Surname, user.Email, user.UserName, user.Password, user.Phone, user.Address);
                                                                    Console.ResetColor();
                                                                }
                                                                else
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                                    Console.WriteLine("Name cannot be the same");
                                                                    Console.ResetColor();
                                                                    goto name;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                                Console.WriteLine("Name cannot be null");
                                                                Console.ResetColor();
                                                                goto name;
                                                            }
                                                            break;

                                                        case (int)UserUpdateEnum.UpdateSurname:
                                                        surname:
                                                            Console.WriteLine("Enter new surname:");
                                                            string surname = Console.ReadLine();
                                                            if (user.Surname.ToLower() != surname.ToLower())
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Green;
                                                                userService.UpdateUser(user, user.Name, surname, user.Email, user.UserName, user.Password, user.Phone, user.Address);
                                                                Console.ResetColor();
                                                            }
                                                            else
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                                Console.WriteLine("Surname cannot be the same");
                                                                Console.ResetColor();
                                                                goto surname;
                                                            }
                                                            break;
                                                        case (int)UserUpdateEnum.UpdateEmail:
                                                            try
                                                            {
                                                            email:
                                                                Console.WriteLine("Enter new email:");
                                                                string email = Console.ReadLine();
                                                                if (email is not null && email.Length > 0)
                                                                {
                                                                    if (user.Email.ToLower() != email.ToLower())
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.Green;
                                                                        userService.UpdateUser(user, user.Name, user.Surname, email, user.UserName, user.Password, user.Phone, user.Address);
                                                                        Console.ResetColor();
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                                                        Console.WriteLine("Email cannot be the same");
                                                                        Console.ResetColor();
                                                                        goto email;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                                    Console.WriteLine("Email cannot be null");
                                                                    Console.ResetColor();
                                                                    goto email;
                                                                }
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                Console.WriteLine(ex.Message);
                                                                Console.ResetColor();
                                                            }

                                                            break;
                                                        case (int)UserUpdateEnum.UpdateUsername:
                                                            try
                                                            {
                                                            username:
                                                                Console.WriteLine("Enter new username:");
                                                                string username = Console.ReadLine();
                                                                if (username is not null && username.Length > 0)
                                                                {
                                                                    if (user.UserName.ToLower() != username.ToLower())
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.Green;
                                                                        userService.UpdateUser(user, user.Name, user.Surname, user.Email, username, user.Password, user.Phone, user.Address);
                                                                        Console.ResetColor();
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                                                        Console.WriteLine("Username cannot be the same");
                                                                        Console.ResetColor();
                                                                        goto username;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                                    Console.WriteLine("Username cannot be null");
                                                                    Console.ResetColor();
                                                                    goto username;
                                                                }
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                Console.WriteLine(ex.Message);
                                                                Console.ResetColor();
                                                            }
                                                            break;
                                                        case (int)UserUpdateEnum.UpdatePassword:
                                                        password:
                                                            Console.WriteLine("Enter old password:");
                                                            string oldPassword = userService.ReadPasswordFromConsole();
                                                            string hashedPassword = userService.HashPassword(oldPassword);
                                                            if (oldPassword == user.Password)
                                                            {
                                                                Console.WriteLine("Enter new password:");
                                                                string password = userService.ReadPasswordFromConsole();
                                                                string hashedPasswordNew = userService.HashPassword(password);
                                                                if (user.Password.ToLower() != password.ToLower())
                                                                    if (password.Length >= 8)
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.Green;
                                                                        userService.UpdateUser(user, user.Name, user.Surname, user.Email, user.UserName, password, user.Phone, user.Address);
                                                                        Console.ResetColor();

                                                                    }
                                                                    else
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                                                        Console.WriteLine("Password length must be at least 8");
                                                                        Console.ResetColor();
                                                                        goto password;
                                                                    }
                                                                else
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                                    Console.WriteLine("Password cannot be the same");
                                                                    Console.ResetColor();
                                                                    goto password;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                                Console.WriteLine("Password is uncorrect");
                                                                Console.ResetColor();
                                                                goto password;
                                                            }
                                                            break;
                                                        case (int)UserUpdateEnum.UpdatePhone:
                                                        phone:
                                                            Console.WriteLine("Enter new Phone:");
                                                            string phone = Console.ReadLine();
                                                            if (user.Phone.ToLower() != phone.ToLower())
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Green;
                                                                userService.UpdateUser(user, user.Name, user.Surname, user.Email, user.UserName, user.Password, phone, user.Address);
                                                                Console.ResetColor();

                                                            }
                                                            else
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                                Console.WriteLine("Phone cannot be the same");
                                                                Console.ResetColor();
                                                                goto phone;
                                                            }
                                                            break;
                                                        case (int)UserUpdateEnum.UpdateAddress:
                                                        address:
                                                            Console.WriteLine("Enter new address:");
                                                            string address = Console.ReadLine();
                                                            if (user.Address.ToLower() != address.ToLower())
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Green;
                                                                userService.UpdateUser(user, user.Name, user.Surname, user.Email, user.UserName, user.Password, user.Phone, address);
                                                                Console.ResetColor();
                                                            }
                                                            else
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                                Console.WriteLine("Address cannot be the same");
                                                                Console.ResetColor();
                                                                goto address;
                                                            }
                                                            break;
                                                        case (int)UserUpdateEnum.ReturnToHomePage:
                                                            isContinueUpdateUser = false;
                                                            break;
                                                    }
                                                }
                                                else Console.WriteLine("Invalid option. Please select again.");
                                            }
                                            else Console.WriteLine("Please enter correct format");
                                        }
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
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            walletService.CreateCard(user, cardNumber, cvc, balance);
                                            Console.ResetColor();
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine(ex.Message);
                                            Console.ResetColor();
                                        }
                                        break;
                                    case (int)UserInfo.RemoveCard:
                                        try
                                        {
                                            bool hasWalletForRemove = context.Wallets.Where(w => w.UserId == user.Id && w.IsDeactive == false).Any();
                                            if (hasWalletForRemove)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                foreach (var wallet in context.Wallets.Where(w => w.UserId == user.Id && w.IsDeactive == false))
                                                {
                                                    Console.WriteLine($"Id:{wallet.Id}/" +
                                                                      $"Card:{wallet.Number}\n" +
                                                                      $"Balance:{wallet.Balance}");
                                                }
                                                Console.ResetColor();
                                                Console.WriteLine("Enter Card Id");
                                                int walletIdForDeact = Convert.ToInt32(Console.ReadLine());
                                                try
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                    walletService.DeactivateWallet(walletIdForDeact, user);
                                                    Console.ResetColor();
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine(ex.Message);
                                                    Console.ResetColor();
                                                }
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                Console.WriteLine("You do not have any active card");
                                                Console.ResetColor();
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                       
                                        break;
                                    case (int)UserInfo.AddRemovedCard:
                                        try
                                        {
                                            bool hasWalletForAdd = context.Wallets.Where(w => w.UserId == user.Id && w.IsDeactive == true).Any();
                                            if (hasWalletForAdd)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                foreach (var wallet in context.Wallets.Where(w => w.UserId == user.Id && w.IsDeactive == true))
                                                {
                                                    Console.WriteLine($"Id:{wallet.Id}/" +
                                                                      $"Card:{wallet.Number}\n" +
                                                                      $"Balance:{wallet.Balance}\n");
                                                }
                                                Console.ResetColor();
                                                Console.WriteLine("Enter Card Id");
                                                int walletIdForAddCard = Convert.ToInt32(Console.ReadLine());
                                                try
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                    walletService.ActivateWallet(walletIdForAddCard, user);
                                                    Console.ResetColor();
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine(ex.Message);
                                                    Console.ResetColor();
                                                }
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                Console.WriteLine("You do not have any deleted card");
                                                Console.ResetColor();
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                       
                                        break;
                                    case (int)UserInfo.IncreaseCardBalance:
                                        try
                                        {
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            foreach (var wallet in context.Wallets.Where(w => w.UserId == user.Id && w.IsDeactive == false))
                                            {
                                                Console.WriteLine($"Id:{wallet.Id}/" +
                                                                  $"Card:{wallet.Number}\n" +
                                                                  $"Balance:{wallet.Balance}\n");
                                            }
                                            Console.ResetColor();
                                            Console.WriteLine("Enter Card Id");
                                            int walletIdForIncBalance = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("Enter the amount of money");
                                            decimal amount = Convert.ToDecimal(Console.ReadLine());
                                            try
                                            {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                walletService.IncreaseBalance(walletIdForIncBalance, user, amount);
                                                Console.ResetColor();
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine(ex.Message);
                                                Console.ResetColor();
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                       
                                        break;
                                    case (int)UserInfo.TransferMoney:
                                        try
                                        {
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            foreach (var wallet in context.Wallets.Where(w => w.UserId == user.Id && w.IsDeactive == false))
                                            {
                                                Console.WriteLine($"Id:{wallet.Id}/" +
                                                                  $"Card:{wallet.Number}\n" +
                                                                  $"Balance:{wallet.Balance}\n");
                                            }
                                            Console.ResetColor();
                                            Console.WriteLine("Enter Card Id To Get the Money");
                                            int walletIdForInc = Convert.ToInt32(Console.ReadLine());
                                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                                            foreach (var wallet in context.Wallets.Where(w => w.UserId == user.Id && w.IsDeactive == false))
                                            {
                                                Console.WriteLine($"Id:{wallet.Id}/" +
                                                                  $"Card:{wallet.Number}\n" +
                                                                  $"Balance:{wallet.Balance}\n");
                                            }
                                            Console.ResetColor();
                                            Console.WriteLine("Enter Card Id To Transfer the Money");
                                            int walletIdForDec = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("Enter Amount to Transfer");
                                            decimal transferAmount = Convert.ToDecimal(Console.ReadLine());
                                            try
                                            {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                walletService.TransferMoney(walletIdForInc, walletIdForDec, user, transferAmount);
                                                Console.ResetColor();
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine(ex.Message);
                                                Console.ResetColor();
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;
                                    case (int)UserInfo.ShowBoughtProducts:
                                        try
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                                            userService.GetBoughtProducts(user);
                                            Console.ResetColor();
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine(ex.Message);
                                            Console.ResetColor();
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
        else Console.WriteLine("Invalid option. Please select again.");
    }
    else Console.WriteLine("Please enter correct format");
}



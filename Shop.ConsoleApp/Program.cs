using Shop.Business.Services;
using Shop.Business.Utilities.Helper;



bool isContinue = true;
UserService userService = new UserService();
ProductService productService = new ProductService();
Console.WriteLine("welcome");
while (isContinue)
{
    Console.WriteLine("\n1)Register");
    Console.WriteLine("2)Login\n");
    string? option = Console.ReadLine();
    int intOption;
    bool isInt = int.TryParse(option, out intOption);
    if (isInt)
    {
        if (intOption > 0 && intOption <= 2)
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
                        userService.CreateUserAsync(name, surname, age, email, password, username, phone, address);
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
                    int loginOption = Convert.ToInt32(Console.ReadLine());
                    switch (loginOption)
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
                                    isContinue = false;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please select again.");
                            break;
                    }
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
    Console.WriteLine("3)See UserInfo\n");
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
                    break;
                case (int)MainPage.GoToCart:
                    break;
                case (int)MainPage.SeeUserInfo:
                    break;
            }
        }
    }
}


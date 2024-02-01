// See https://aka.ms/new-console-template for more information
using Shop.Business.Interfaces;
using Shop.Business.Services;
using Shop.Business.Utilities.Helper;
bool isContinue = true;
UserService userService = new UserService();
while (isContinue)
{
    Console.WriteLine("welcome");
    Console.WriteLine("1)Register");
    Console.WriteLine("2)Login");
    int option = Convert.ToInt32(Console.ReadLine());
    switch (option)
    {
        case (int)ConsoleAppEnum.Register:
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            break;
        case (int)ConsoleAppEnum.Login:
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
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
            }
          
            break;
    }
}


using Microsoft.EntityFrameworkCore;
using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess;
using System.Security.Cryptography;
using System.Text;

namespace Shop.Business.Services;

public class UserService : IUserService
{
    ShopDbContext context = new ShopDbContext();
    public void CreateUser(string name, string? surname, int? age, string email, string password, string username, string? phone, string? address)
    {
        if (name is not null && email is not null && password is not null && username is not null)
        {
            if (age is not null && age < 16) throw new LessThanMinimumException("Age cannot be less than 16");
            if (password.Length < 8) throw new LessThanMinimumException("Password length must be at least 8");
            bool isDublicate = context.Users.Where(u => u.Email.ToLower() == email.ToLower() || u.UserName.ToLower() == username.ToLower()).Any();
            if (!isDublicate)
            {
                User user = new User()
                {
                    Name = name,
                    Surname = surname,
                    Age = age,
                    Email = email,
                    Password = password,
                    UserName = username,
                    Phone = phone,
                    Address = address
                };
                context.Users.Add(user);
                user.Cart = new Cart()
                {
                    Id = user.Id
                };
                context.Carts.Add(user.Cart);
                context.SaveChanges();
                Console.Out.WriteLine("Registered Successfully");
            }
            else throw new ShouldBeUniqueException("Email or Username is taken");
        }
        else throw new CannotBeNullException("Value cannot be null");
    }

    public void LoginUserWithEmail(string email, string password)
    {
        if (email is null || password is null) throw new CannotBeNullException("Value cannot be null");
        User? user = context.Users.FirstOrDefault(u => u.Email == email);
        if (user is null || user.IsDeactive == true) throw new CannotBeFoundException("User email cannot be found");
        if (user.Password != password) throw new IsNotCorrectException("Password is not correct");
        Console.Out.WriteLine("Logged in Successfully");
    }

    public void LoginUserWithUsername(string username, string password)
    {
        if (username is null || password is null) throw new CannotBeNullException("Value cannot be null");
        User? user = context.Users.FirstOrDefault(u => u.UserName == username);
        if (user is null || user.IsDeactive == true) throw new CannotBeFoundException("Username cannot be found");
        if (user.Password != password) throw new IsNotCorrectException("Password is not correct");
        Console.Out.WriteLine("Logged in Successfully");
    }
    public User FindUserByEmail(string email)
    {
        if (email is not null)
        {
            User user = context.Users.Where(u => u.Email.ToLower() == email.ToLower()).FirstOrDefault();
            {
                if (user is not null && user.IsDeactive == false)
                    return user;
                else throw new CannotBeFoundException("User cannot be found");
            }
        }
        else throw new CannotBeNullException("Value cannot be null");
    }

    public User FindUserByUsername(string username)
    {
        if (username is not null)
        {
            User user = context.Users.Where(u => u.UserName.ToLower() == username.ToLower()).FirstOrDefault();
            {
                if (user is not null && user.IsDeactive == false)
                    return user;
                else throw new CannotBeFoundException("User cannot be found");
            }
        }
        else throw new CannotBeNullException("Value cannot be null");
    }

    public void LoginAdminWithEmail(string email, string password)
    {
        if (email is null || password is null) throw new CannotBeNullException("Value cannot be null");
        User? admin = context.Users.Find(1);
        if (admin is null) throw new CannotBeFoundException("User email cannot be found");
        if (admin.Email != email) throw new IsNotCorrectException("Email is not correct");
        if (admin.Password != password) throw new IsNotCorrectException("Password is not correct");
        Console.Out.WriteLine("Logged in Successfully");
    }
    public void LoginAdminWithUsername(string username, string password)
    {
        if (username is null || password is null) throw new CannotBeNullException("Value cannot be null");
        User? admin = context.Users.Find(1);
        if (admin is null) throw new CannotBeFoundException("Username cannot be found");
        if (admin.UserName != username) throw new IsNotCorrectException("Username is not correct");
        if (admin.Password != password) throw new IsNotCorrectException("Password is not correct");
        Console.Out.WriteLine("Logged in Successfully");
    }

    public void DeactivateUser(int userId)
    {
        User user = context.Users.Find(userId);
        if (user is not null && user.Id != 1)
        {
            if (user.IsDeactive == false)
            {
                user.IsDeactive = true;
                Cart cart = context.Carts.Find(userId);
                cart.IsDeactive = true;
                List<CartProduct> cartProducts = context.CartProducts.Where(cp => cp.CartId == userId && cp.ProductCountInCart != 0).ToList();
                List<Wallet> wallets = context.Wallets.Where(w => w.UserId == userId).ToList();
                foreach (var cartProduct in cartProducts)
                {
                    cartProduct.IsDeactive = true;
                    cartProduct.ModifiedTime = DateTime.Now;
                }
                foreach (var wallet in wallets)
                {
                    wallet.IsDeactive = true;
                    wallet.ModifiedTime = DateTime.Now;
                }
                cart.ModifiedTime = DateTime.Now;
                user.ModifiedTime = DateTime.Now;
                context.SaveChanges();
                Console.WriteLine("Successfully Deactivated");
            }
            else throw new AlreadyExistException("User is already deactive");
        }
        else throw new CannotBeFoundException("User cannot be found");
    }
    public void ActivateUser(int userId)
    {
        User user = context.Users.Find(userId);
        if (user is not null && user.Id != 1)
        {
            if (user.IsDeactive == true)
            {
                user.IsDeactive = false;
                Cart cart = context.Carts.Find(userId);
                cart.IsDeactive = false;
                List<CartProduct> cartProducts = context.CartProducts.Where(cp => cp.CartId == userId && cp.ProductCountInCart != 0).ToList();
                List<Wallet> wallets = context.Wallets.Where(w => w.UserId == userId).ToList();
                foreach (var cartProduct in cartProducts)
                {
                    cartProduct.IsDeactive = false;
                    cartProduct.ModifiedTime = DateTime.Now;
                }
                foreach (var wallet in wallets)
                {
                    wallet.IsDeactive = false;
                    wallet.ModifiedTime = DateTime.Now;
                }
                cart.ModifiedTime = DateTime.Now;
                user.ModifiedTime = DateTime.Now;
                context.SaveChanges();
                Console.WriteLine("Successfully Activated");
            }
            else throw new AlreadyExistException("User is already Active");
        }
        else throw new CannotBeFoundException("User cannot be found");
    }
    public void UpdateUser(User user, string name, string? surname, string email, string username, string password, string? phone, string? address)
    {
        if (user is not null)
        {
            user.Name = name;
            user.Surname = surname;
            if (context.Users.Where(u => u.Email.ToLower() == email.ToLower() && u.Email.ToLower() != user.Email.ToLower()).Any())
                throw new ShouldBeUniqueException("This Email is already taken");
            user.Email = email;
            if (context.Users.Where(u => u.UserName.ToLower() == username.ToLower() && u.UserName.ToLower() != user.UserName.ToLower()).Any())
                throw new ShouldBeUniqueException("This Username is already taken");
            user.UserName = username;
            user.Password = password;
            user.Phone = phone;
            user.Address = address;
            user.ModifiedTime = DateTime.Now;
            context.SaveChanges();
            Console.WriteLine("Successfully updated");
        }
        else throw new CannotBeFoundException("User cannot be found");
    }
    public string HashPassword(string input)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
    public string ReadPasswordFromConsole()
    {
        StringBuilder password = new StringBuilder();
        ConsoleKeyInfo keyInfo;

        do
        {
            keyInfo = Console.ReadKey(intercept: true);
            if (keyInfo.Key != ConsoleKey.Enter)
            {
                Console.Write("*");
                password.Append(keyInfo.KeyChar);
            }
        } while (keyInfo.Key != ConsoleKey.Enter);

        Console.WriteLine();
        return password.ToString();
    }

    //public void GetBoughtProducts(User user)
    //{
    //    if (user is not null)
    //    {
    //        List<ProductIncoive> productIncoives = context.ProductInvoices.Where(pi => !pi.IsDeactive).Include(pi => pi.Invoice.Where(i=>i.IsPaid)).ThenInclude(i => pi.Product).ToList();
    //        if(invoices is not null)
    //        {
    //            foreach (var invoice in invoices)
    //            {
    //                Console.WriteLine($"Bought date:{invoice.ModifiedTime}\n" +
    //                    $"Product Name:{}");
    //            }
    //        }
    //    }
    //    else throw new CannotBeFoundException("User Cannot be found");
    //}
}

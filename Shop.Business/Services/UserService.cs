using Microsoft.EntityFrameworkCore;
using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess;

namespace Shop.Business.Services;

public class UserService : IUserService
{
    ShopDbContext context = new ShopDbContext();
    public void CreateUser(string name, string? surname, int? age, string email, string password, string username, string? phone, string? address)
    {
        if (name is not null && email is not null && password is not null && username is not null)
        {
            if (age != null && age < 16) throw new LessThanMinimumException("Age cannot be less than 16");
            if (password.Length < 8) throw new LessThanMinimumException("Password length must be at least 8");
            bool isDublicate = context.Users.Where(u=> u.Email.ToLower() == email.ToLower() || u.UserName.ToLower() == username.ToLower()).Any();
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
        User? user= context.Users.FirstOrDefault(u=> u.Email == email);
        if (user is null) throw new CannotBeFoundException("User email cannot be found");
        if (user.Password != password) throw new IsNotCorrectException("Password is not correct");
        Console.Out.WriteLine("Logged in Successfully");
    }

    public void LoginUserWithUsername(string username, string password)
    {
        if (username is null || password is null) throw new CannotBeNullException("Value cannot be null");
        User? user = context.Users.FirstOrDefault(u => u.UserName == username);
        if (user is null) throw new CannotBeFoundException("User email cannot be found");
        if (user.Password != password) throw new IsNotCorrectException("Password is not correct");
        Console.Out.WriteLine("Logged in Successfully");
    }
    public User FindUserByEmail(string email)
    {
        if (email is not null)
        {
            User user = context.Users.Where(u => u.Email.ToLower() == email.ToLower()).FirstOrDefault();
            {
                if (user is not null)
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
                if (user is not null)
                    return user;
                else throw new CannotBeFoundException("User cannot be found");
            }
        }
        else throw new CannotBeNullException("Value cannot be null");
    }

    //public void UpdateUser

}

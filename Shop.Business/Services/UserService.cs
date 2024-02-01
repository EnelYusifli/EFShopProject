using Microsoft.EntityFrameworkCore;
using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess;

namespace Shop.Business.Services;

public class UserService : IUserService
{
    ShopDbContext context = new ShopDbContext();
    public async void CreateUserAsync(string name, string? surname, int? age, string email, string password, string username, string? phone, string? address)
    {
        if (name is not null && email is not null && password is not null && username is not null)
        {
            if (age != null && age < 16) throw new LessThanMinimumException("Age cannot be less than 16");
            if (password.Length < 8) throw new LessThanMinimumException("Password length must be at least 8");
            bool isDublicate = await context.Users.Where(u=> u.Email == email || u.UserName == username).AnyAsync();
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
                await context.Users.AddAsync(user);
                user.Cart = new Cart()
                {
                    Id = user.Id
                };
                await context.Carts.AddAsync(user.Cart);
                await context.SaveChangesAsync();
            }
            else throw new ShouldBeUniqueException("Email or Username is taken");
        }
        else throw new CannotBeNullException("Value cannot be null");
    }

    public async void LoginUserWithEmail(string email, string password)
    {
        if (email is null || password is null) throw new CannotBeNullException("Value cannot be null");
        User? user= await context.Users.FirstOrDefaultAsync(u=> u.Email == email);
        if (user is null) throw new CannotBeFoundException("User email cannot be found");
        if (user.Password != password) throw new IsNotCorrectException("Password is not correct");
        await Console.Out.WriteLineAsync("Logged in Successfully");
    }

    public async void LoginUserWithUsername(string username, string password)
    {
        if (username is null || password is null) throw new CannotBeNullException("Value cannot be null");
        User? user = await context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        if (user is null) throw new CannotBeFoundException("User email cannot be found");
        if (user.Password != password) throw new IsNotCorrectException("Password is not correct");
        await Console.Out.WriteLineAsync("Logged in Successfully");
    }
   
}

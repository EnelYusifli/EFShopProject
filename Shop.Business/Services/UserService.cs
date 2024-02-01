using Microsoft.EntityFrameworkCore;
using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess;
using System.ComponentModel.Design;

namespace Shop.Business.Services;

public class UserService : IUserService
{
    ShopDbContext context = new ShopDbContext();
    public async void CreateUserAsync(string name, string? surname, int? age, string email, string password, string username, string? phone, string? address)
    {
        if (name is not null && email is not null && password is not null && username is not null)
        {
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
                await context.SaveChangesAsync();
            }
            else throw new ShouldBeUniqueException("Email or Username is taken");
        }
        else throw new CannotBeNullException("Value cannot be null");
    }  
}

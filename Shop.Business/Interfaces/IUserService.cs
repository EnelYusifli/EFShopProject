using Shop.Core.Entities;

namespace Shop.Business.Interfaces;

public interface IUserService
{
    void CreateUser(string name, string? surname, int? age, string email, string password, string username, string? phone, string? address);
    void LoginUserWithEmail(string email,string password );
    void LoginUserWithUsername(string username,string password );
     User FindUserByEmail(string email);
     User FindUserByUsername(string username);
     void LoginAdminWithEmail(string email, string password);
     void LoginAdminWithUsername(string username, string password);
     void DeactivateUser(int userId);
     void ActivateUser(int userId);
     void UpdateUser(User user, string name, string? surname, string email, string username, string password, string? phone, string? address);
     string HashPassword(string input);
     string ReadPasswordFromConsole();
    void GetBoughtProducts(User user);
}

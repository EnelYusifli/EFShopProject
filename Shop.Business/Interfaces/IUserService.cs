namespace Shop.Business.Interfaces;

public interface IUserService
{
    void CreateUser(string name, string? surname, int? age, string email, string password, string username, string? phone, string? address);
    void LoginUserWithEmail(string email,string password );
    void LoginUserWithUsername(string username,string password );
}

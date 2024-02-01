namespace Shop.Business.Interfaces;

public interface IUserService
{
     void CreateUserAsync(string name, string? surname, int? age, string email, string password, string username, string? phone, string? address)
}

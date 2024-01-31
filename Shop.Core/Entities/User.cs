namespace Shop.Core.Entities;

public class User : BaseEntities
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Surname { get; set; }
    public int? Age { get; set; } = 18;
    public string Email { get; set; }=null!;
    public string Password { get; set; } = null!;
    public string UserName { get; set; }= null!;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public ICollection<Wallet>? Wallets { get; set; }
    public Cart Cart { get; set; } = null!;
}

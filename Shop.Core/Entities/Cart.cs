namespace Shop.Core.Entities;

public class Cart:BaseEntities
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public ICollection<CartProduct>? CartProducts { get; set; }
}

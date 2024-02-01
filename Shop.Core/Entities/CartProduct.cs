using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class CartProduct:BaseEntities
{
    public Cart Cart { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int ProductCountInCart { get; set; }
}

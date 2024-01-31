namespace Shop.Core.Entities;

public class CartProduct:BaseEntities
{
    public Cart Cart { get; set; }
    public Product Product { get; set; }
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int ProductCount { get; set; }
}

using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class ProductDiscount:BaseEntities
{
    public int ProductId { get; set; }
    public int DiscountId { get; set; }
    public bool IsActive { get; set; }
    public Product Product { get; set; } = null!;
    public Discount Discount { get; set; } = null!;
}

using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class Discount:BaseEntities
{
    public int Id { get; set; }
    public int Percent {  get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public ICollection<ProductDiscount>? ProductDiscounts { get; set; }
}

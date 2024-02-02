using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class Category:BaseEntities
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ICollection<Product>? Products { get; set; }
}

using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess;

namespace Shop.Business.Services;

public class ProductService : IProductService
{
    ShopDbContext context = new ShopDbContext();

    public async void CreateProduct(string productName, string description, decimal price, int availableCount)
    {
        if (productName is not null)
        {
            if (availableCount > 0)
            {
                if (price > 0)
                {
                    Product product = new Product()
                    {
                        Name = productName,
                        Price = price,
                        AvailableCount = availableCount,
                        Description = description
                    };
                    await context.Products.AddAsync(product);
                    await context.SaveChangesAsync();
                }
                else throw new LessThanMinimumException("Price should be more than 0");
            }
            else throw new LessThanMinimumException("Product cannot be created without availability");
        }
        else throw new CannotBeNullException("Name cannot be null");
    }
}

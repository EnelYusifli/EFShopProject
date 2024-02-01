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
                    bool isDublicate = context.Products.Where(p => p.Name.ToLower() == productName.ToLower()).Any();
                    if (!isDublicate)
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
                    else throw new ShouldBeUniqueException("Product Name must be unique");
                }
                else throw new LessThanMinimumException("Price should be more than 0");
            }
            else throw new LessThanMinimumException("Product cannot be created without availability");
        }
        else throw new CannotBeNullException("Name cannot be null");
    }
    public async void AddProductToCart(string productName)
    {

    }




}

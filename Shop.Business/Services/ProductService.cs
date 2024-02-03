using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess;

namespace Shop.Business.Services;

public class ProductService : IProductService
{
    ShopDbContext context = new ShopDbContext();

    public async void CreateProduct(string productName, string description, decimal price, int availableCount, int categoryId)
    {

        if (productName is not null)
        {
            if (availableCount > 0)
            {
                if (price > 0)
                {
                    if (categoryId > 0)
                    {
                        Category category = context.Categories.Find(categoryId);
                        if (category is not null)
                        {
                            bool isDublicate = context.Products.Where(p => p.Name.ToLower() == productName.ToLower()).Any();
                            if (!isDublicate)
                            {
                                Product product = new Product()
                                {
                                    Name = productName,
                                    Price = price,
                                    AvailableCount = availableCount,
                                    Description = description,
                                    CategoryId = categoryId,
                                    Category = category
                                };
                                await context.Products.AddAsync(product);
                                await context.SaveChangesAsync();
                            }
                            else throw new ShouldBeUniqueException("Product Name must be unique");
                        }
                        else throw new CannotBeFoundException("Category cannot be found");
                    }
                    else throw new LessThanMinimumException("Id should be more than 0");

                }
                else throw new LessThanMinimumException("Price should be more than 0");
            }
            else throw new LessThanMinimumException("Product cannot be created without availability");
        }
        else throw new CannotBeNullException("Name cannot be null");
    }
    public void AddProductToCart(int productId, User user, int count = 1)
    {
        Product? product = context.Products.Find(productId);
        if (product is not null && product.IsDeactive == false && user is not null)
        {
            if (count <= product.AvailableCount)
            {
                CartProduct? cartProduct = context.CartProducts.Find(user.Id, product.Id);
                if (cartProduct is not null)
                {
                    throw new AlreadyExistException("Product is already in your cart");
                }
                CartProduct newCartProduct = new CartProduct()
                {
                    ProductId = product.Id,
                    CartId = user.Id,
                    ProductCountInCart = count
                };
                context.CartProducts.Add(newCartProduct);
                context.SaveChanges();
                Console.Out.WriteLine("Added to Cart Successfully");

            }
            else throw new MoreThanMaximumException("Count is more than available");
        }
        else throw new CannotBeFoundException("Product cannot be found");
    }




}

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
        if (product is not null && product.IsDeactive == false && user is not null && user.IsDeactive==false)
        {
            if (count <= product.AvailableCount)
            {
                CartProduct? cartProduct = context.CartProducts.Find(user.Id, product.Id);
                if (cartProduct is not null) throw new AlreadyExistException("Product is already in your cart");
                
                CartProduct newCartProduct = new CartProduct()
                {
                    ProductId = product.Id,
                    CartId = user.Id,
                    ProductCountInCart = count,
                };
                context.CartProducts.Add(newCartProduct);
                context.SaveChanges();
                Console.Out.WriteLine("Added to Cart Successfully");

            }
            else throw new MoreThanMaximumException("Count is more than available");
        }
        else throw new CannotBeFoundException("Product or user cannot be found");
    }
    public void DeactivateCartProduct(int productId, User user)
    {
        Product? product = context.Products.Find(productId);
        if (product is not null && product.IsDeactive == false && user is not null)
        {
            CartProduct? cartProduct = context.CartProducts.Find(user.Id, product.Id);
            if (cartProduct is null) throw new AlreadyExistException("Product is not in your cart");
            cartProduct.IsDeactive = true;
            Console.Out.WriteLine("Deleted from Cart Successfully");
        }
        else throw new CannotBeFoundException("Product cannot be found on your cart");
    }
    public void ActivateProduct(int productId)
    {
        Product product = context.Products.Find(productId);
        if (product is not null)
        {
            if (product.IsDeactive == true)
            {
                product.IsDeactive = false;
                context.SaveChanges();
                Console.WriteLine("Successfully Deactivated");
            }
            else throw new AlreadyExistException("Product is already active");
        }
        else throw new CannotBeFoundException("Product cannot be found");
    }

    public void DeactivateProduct(int productId)
    {
        Product product = context.Products.Find(productId);
        if (product is not null)
        {
            if (product.IsDeactive == false)
            {
                product.IsDeactive = true;
                context.SaveChanges();
                Console.WriteLine("Successfully Deactivated");
            }
            else throw new AlreadyExistException("Product is already deactive");
        }
        else throw new CannotBeFoundException("Product cannot be found");
    }

    public void UpdateProduct(Product product, string name, string description,decimal price,int availableCount,int categoryId)
    {
        if (product is not null)
        {
            if (context.Products.Where(p => p.Name.ToLower() == name.ToLower() && p.Name.ToLower() != product.Name.ToLower()).Any())
                throw new ShouldBeUniqueException("This Name is already taken");
            product.Name = name;
            product.Description = description;
            product.Price = price;
            product.AvailableCount = availableCount;
            Category category=context.Categories.Find(categoryId);
            if (category is not null)
                product.CategoryId = categoryId;
            else throw new CannotBeFoundException("Category cannot be found");
            context.Products.Update(product);
            context.SaveChanges();
            Console.WriteLine("Updated Successfully");
        }
        else throw new CannotBeFoundException("Product cannot be found");
    }

}

using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess;
namespace Shop.Business.Services;

public class ProductService : IProductService
{
    ShopDbContext context = new ShopDbContext();

    public async void CreateProduct(string productName, string description, decimal price, int availableCount, int categoryId, int brandId)
    {

        if (productName is not null)
        {
            if (availableCount > 0)
            {
                if (price > 0)
                {
                    if (categoryId > 0 && brandId > 0)
                    {
                        Category category = context.Categories.Find(categoryId);
                        Brand brand = context.Brands.Find(brandId);
                        if (category is not null && brand is not null)
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
                                    Category = category,
                                    Brand = brand
                                };
                                await context.Products.AddAsync(product);
                                await context.SaveChangesAsync();
                            }
                            else throw new ShouldBeUniqueException("Product Name must be unique");
                        }
                        else throw new CannotBeFoundException("Category or brand cannot be found");
                    }
                    else throw new LessThanMinimumException("Id should be more than 0");
                }
                else throw new LessThanMinimumException("Price should be more than 0");
            }
            else throw new LessThanMinimumException("Product cannot be created without availability");
        }
        else throw new CannotBeNullException("Name cannot be null");
    }

    public void DeactivateCartProduct(int productId, User user)
    {
        Product? product = context.Products.Find(productId);
        if (product is not null && product.IsDeactive == false && user is not null)
        {
            CartProduct? cartProduct = context.CartProducts.Find(user.Id, product.Id);
            if (cartProduct is null) throw new AlreadyExistException("Product is not in your cart");
            cartProduct.IsDeactive = true;
            cartProduct.ModifiedTime = DateTime.Now;
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
                List<CartProduct> cartProducts = context.CartProducts.Where(cp => cp.ProductId == productId && cp.ProductCountInCart != 0).ToList();
                foreach (var cartProduct in cartProducts)
                {
                    cartProduct.IsDeactive = false;
                    cartProduct.ModifiedTime = DateTime.Now;
                }
                product.ModifiedTime = DateTime.Now;
                context.SaveChanges();
                Console.WriteLine("Successfully Activated");
            }
            else throw new AlreadyExistException("Product is already deactive");
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
                List<CartProduct> cartProducts = context.CartProducts.Where(cp => cp.ProductId == productId && cp.ProductCountInCart != 0).ToList();
                foreach (var cartProduct in cartProducts)
                {
                    cartProduct.IsDeactive = true;
                    cartProduct.ModifiedTime = DateTime.Now;
                }
                product.ModifiedTime = DateTime.Now;
                context.SaveChanges();
                Console.WriteLine("Successfully Deactivated");
            }
            else throw new AlreadyExistException("Product is already deactive");
        }
        else throw new CannotBeFoundException("Product cannot be found");
    }

    public void UpdateProduct(Product product, string name, string description, decimal price, int availableCount, int categoryId, int brandId)
    {
        if (product is not null)
        {
            if (context.Products.Where(p => p.Name.ToLower() == name.ToLower() && p.Name.ToLower() != product.Name.ToLower()).Any())
                throw new ShouldBeUniqueException("This Name is already taken");
            product.Name = name;
            product.Description = description;
            product.Price = price;
            product.AvailableCount = availableCount;
            Category category = context.Categories.Find(categoryId);
            Brand brand = context.Brands.Find(brandId);
            if (category is not null)
            {
                if (brand is not null)
                {
                    product.CategoryId = categoryId;
                    product.BrandId = brandId;
                    product.ModifiedTime = DateTime.Now;
                    context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                    Console.WriteLine("Updated Successfully");
                }
                else throw new CannotBeFoundException("Brand cannot be found");
            }
            else throw new CannotBeFoundException("Category cannot be found");
        }
        else throw new CannotBeFoundException("Product cannot be found");
    }
    //public void SearchForProductVia() 
    //{

    //}
}

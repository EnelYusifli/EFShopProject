using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess;

namespace Shop.Business.Services;

public class CategoryService : ICategoryService
{
    ShopDbContext context = new ShopDbContext();
    ProductService productService = new ProductService();
    public void CreateCategory(string name, string description)
    {
        if (name is not null)
        {
            bool isDublicate = context.Categories.Where(c => c.Name.ToLower() == name.ToLower()).Any();
            if (isDublicate) throw new AlreadyExistException("This category is already exist");
            Category category = new Category();
            {
                category.Name = name;
                category.Description = description;
            }
            context.Categories.Add(category);
            context.SaveChanges();
            Console.WriteLine("Added successfully");
        }
    }
    public void UpdateCategory(Category category, string name, string description)
    {
            if (category is not null)
            {
                if (context.Categories.Where(c => c.Name.ToLower() == name.ToLower() && c.Name.ToLower() != category.Name.ToLower()).Any())
                    throw new ShouldBeUniqueException("This Name is already taken");
                category.Name = name;
                category.Description = description;
                category.ModifiedTime= DateTime.Now;
                context.Categories.Update(category);
                context.SaveChanges();
                Console.WriteLine("Updated Successfully");
            }
            else throw new CannotBeFoundException("Category cannot be found");
    }
    public void DeactivateCategory(int categoryId)
    {
        if (categoryId <= 0) throw new LessThanMinimumException("Id cannot be negative or 0");
        Category category = context.Categories.Find(categoryId);
        if (category is not null)
        {
            if (category.IsDeactive == false)
            {
                category.IsDeactive = true;
                List<Product> products = context.Products.Where(p => p.CategoryId == categoryId).ToList();
                foreach (var product in products)
                {
                    productService.DeactivateProduct(product.Id);
                }
                category.ModifiedTime = DateTime.Now;
                context.SaveChanges();
                Console.WriteLine("Successfully Deactivated");
            }
            else throw new AlreadyExistException("Category is already deactive");
        }
        else throw new CannotBeFoundException("Category cannot be found");
    }
    public void ActivateCategory(int categoryId)
    {
        if (categoryId <= 0) throw new LessThanMinimumException("Id cannot be negative or 0");
        Category category = context.Categories.Find(categoryId);
        if (category is not null)
        {
            if (category.IsDeactive == true)
            {
                category.IsDeactive = false;
                List<Product> products = context.Products.Where(p => p.CategoryId == categoryId).ToList();
                foreach (var product in products)
                {
                    productService.ActivateProduct(product.Id);
                }
                category.ModifiedTime = DateTime.Now;
                context.SaveChanges();
                Console.WriteLine("Successfully Activated");
            }
            else throw new AlreadyExistException("Category is already active");
        }
        else throw new CannotBeFoundException("Category cannot be found");
    }
}

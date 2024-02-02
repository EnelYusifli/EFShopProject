using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess;

namespace Shop.Business.Services;

public class CategoryService: ICategoryService
{
    ShopDbContext context = new ShopDbContext();
    public void CreateCategory(string name,string description)
    {
        if(name is not null)
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
        }
    }
}

using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess;

namespace Shop.Business.Services;

public class BrandService:IBrandService
{
    ShopDbContext context = new ShopDbContext();
    ProductService productService = new ProductService();
    public void CreateBrand(string name)
    {
        if (name is not null)
        {
            bool isDublicate = context.Brands.Where(b => b.Name.ToLower() == name.ToLower()).Any();
            if (isDublicate) throw new AlreadyExistException("This brand is already exist");
            Brand brand = new Brand();
            {
                brand.Name = name;
            }
            context.Brands.Add(brand);
            context.SaveChanges();
            Console.WriteLine("Added successfully");
        }
    }
    public void UpdateBrand(Brand brand, string name)
    {
        if (brand is not null)
        {
            if (context.Brands.Where(b => b.Name.ToLower() == name.ToLower() && b.Name.ToLower() != brand.Name.ToLower()).Any())
                throw new ShouldBeUniqueException("This Name is already taken");
            brand.Name = name;
            brand.ModifiedTime = DateTime.Now;
            context.Brands.Update(brand);
            context.SaveChanges();
            Console.WriteLine("Updated Successfully");
        }
        else throw new CannotBeFoundException("Brand cannot be found");
    }
    public void DeactivateBrand(int brandId)
    {
        if (brandId <= 0) throw new LessThanMinimumException("Id cannot be negative or 0");
        Brand brand = context.Brands.Find(brandId);
        if (brand is not null)
        {
            if (brand.IsDeactive == false)
            {
                brand.IsDeactive = true;
                List<Product> products = context.Products.Where(p => p.BrandId == brandId).ToList();
                foreach (var product in products)
                {
                    productService.DeactivateProduct(product.Id);
                }
                brand.ModifiedTime = DateTime.Now;
                context.SaveChanges();
                Console.WriteLine("Successfully Deactivated");
            }
            else throw new AlreadyExistException("Brand is already deactive");
        }
        else throw new CannotBeFoundException("Brand cannot be found");
    }
    public void ActivateBrand(int brandId)
    {
        if (brandId <= 0) throw new LessThanMinimumException("Id cannot be negative or 0");
        Brand brand = context.Brands.Find(brandId);
        if (brand is not null)
        {
            if (brand.IsDeactive == true)
            {
                brand.IsDeactive = false;
                List<Product> products = context.Products.Where(p => p.BrandId == brandId).ToList();
                foreach (var product in products)
                {
                    productService.ActivateProduct(product.Id);
                }
                brand.ModifiedTime = DateTime.Now;
                context.SaveChanges();
                Console.WriteLine("Successfully Activated");
            }
            else throw new AlreadyExistException("Brand is already active");
        }
        else throw new CannotBeFoundException("Brand cannot be found");
    }
}

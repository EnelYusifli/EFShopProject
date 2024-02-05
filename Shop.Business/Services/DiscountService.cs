using Microsoft.EntityFrameworkCore;
using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess;

namespace Shop.Business.Services;

public class DiscountService: IDiscountService
{
    ShopDbContext context=new ShopDbContext();
    ProductService productService = new ProductService();
    public void CreateDiscount(int percent,DateTime startTime,DateTime endTime)
    {
        if (percent >= 0 && percent<=100)
        {
            if(startTime<endTime)
            {
                Discount discount = new Discount()
                {
                    Percent = percent,
                    StartTime = startTime,
                    EndTime = endTime
                };
                context.Discounts.Add(discount);
                context.SaveChanges();
                
            }throw new IsNotCorrectException("Start time cannot be after the end time");
        }throw new IsNotCorrectException("Percent must be between 0 and 100");
    }

    public void DeactivateDiscount(int discountId)
    {
        Discount discount = context.Discounts.Find(discountId);
        if (discount is not null)
        {
            if (discount.EndTime < DateTime.Now) throw new IsNotCorrectException("The Discount has been already ended");
            if (discount.IsDeactive == false)
            {
                discount.IsDeactive = true;
                var productDiscounts = context.ProductDiscounts.Where(cp => cp.DiscountId == discountId && !cp.IsDeactive);

                foreach (var productDiscount in productDiscounts)
                {
                    productDiscount.IsDeactive = true;
                    productDiscount.ModifiedTime= DateTime.Now;
                }
                discount.ModifiedTime = DateTime.Now;
                context.SaveChanges();
                Console.WriteLine("Successfully Deactivated");
            }
            else throw new AlreadyExistException("Discount is already deactive");
        }
        else throw new CannotBeFoundException("Discount cannot be found");
    }
    public void ActivateDiscount(int discountId)
    {
        Discount discount = context.Discounts.Find(discountId);
        if (discount is not null)
        {
            if (discount.EndTime < DateTime.Now) throw new IsNotCorrectException("The Discount has been already ended");
            if (discount.IsDeactive == true)
            {
                discount.IsDeactive = false;
                var productDiscounts = context.ProductDiscounts.Where(cp => cp.DiscountId == discountId && !cp.IsDeactive);

                foreach (var productDiscount in productDiscounts)
                {
                    productDiscount.IsDeactive = false;
                    productDiscount.ModifiedTime = DateTime.Now;
                }
                discount.ModifiedTime = DateTime.Now;
                context.SaveChanges();
                Console.WriteLine("Successfully Activated");
            }
            else throw new AlreadyExistException("Discount is already active");
        }
        else throw new CannotBeFoundException("Discount cannot be found");
    }
}

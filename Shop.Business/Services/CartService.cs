using Microsoft.EntityFrameworkCore;
using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess;

namespace Shop.Business.Services;

public class CartService : ICartService
{
    ShopDbContext context = new();
    public void AddProductToCart(int productId, User user, int count = 1)
    {
        if (count <= 0) throw new LessThanMinimumException("Value cannot be 0 or negative");
        Product? product = context.Products.Find(productId);
        if (product is not null && product.IsDeactive == false && user is not null && user.IsDeactive == false)
        {
            if (count <= product.AvailableCount)
            {
                CartProduct? cartProduct = context.CartProducts.Find(user.Id, product.Id);
                if (cartProduct is not null && cartProduct.IsDeactive == false)
                {
                    if ((cartProduct.ProductCountInCart + count) > product.AvailableCount)
                        throw new MoreThanMaximumException("Count is More than Available");
                    else
                    {
                        cartProduct.ProductCountInCart += count;
                        context.SaveChanges();
                        Console.Out.WriteLine("Added to Cart Successfully");
                        return;
                    }
                };
                if (cartProduct is not null && cartProduct.IsDeactive == true)
                {
                    cartProduct.IsDeactive = false;
                    cartProduct.ProductCountInCart = count;
                    cartProduct.ModifiedTime = DateTime.Now;
                    context.SaveChanges();
                    Console.Out.WriteLine("Added to Cart Successfully");
                    return;
                }
                else
                {
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

            }
            else throw new MoreThanMaximumException("Count is more than available");
        }
        else throw new CannotBeFoundException("Product or user cannot be found");
    }

    public void RemoveProductFromCart(int productId, User user, int count = 1)
    {
        Product? product = context.Products.Find(productId);
        if (product is not null && product.IsDeactive == false && user is not null && user.IsDeactive == false)
        {
            CartProduct? cartProduct = context.CartProducts.Find(user.Id, product.Id);
            if (cartProduct is null || (cartProduct is not null && cartProduct.IsDeactive == true))
                throw new CannotBeFoundException("Product does not exist in your cart");
            if (cartProduct is not null && cartProduct.IsDeactive == false)
            {
                if (cartProduct.ProductCountInCart < count)
                    throw new MoreThanMaximumException("You do not have that many product in your cart");
                cartProduct.ProductCountInCart -= count;
                if (cartProduct.ProductCountInCart == 0)
                {
                    cartProduct.IsDeactive = true;
                    cartProduct.ModifiedTime = DateTime.Now;
                }
                context.Entry(cartProduct).State = EntityState.Modified;
                context.SaveChanges();
                Console.Out.WriteLine("Removed from Cart Successfully");
                return;
            }
        }
        else throw new CannotBeFoundException("Product or user cannot be found");
    }
}

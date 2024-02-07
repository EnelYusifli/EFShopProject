using Shop.Core.Entities;

namespace Shop.Business.Interfaces;

public interface ICartService
{
    void AddProductToCart(int productId, User user, int count = 1);
    void RemoveProductFromCart(int productId, User user, int count = 1);
}

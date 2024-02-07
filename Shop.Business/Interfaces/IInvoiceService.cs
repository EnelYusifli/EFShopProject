using Shop.Core.Entities;

namespace Shop.Business.Interfaces;

public interface IInvoiceService
{
    void CreateInvoice(User user, List<Product> products, decimal total);
    void BuyProduct(User user, List<Product> products, decimal totalPrice);

}

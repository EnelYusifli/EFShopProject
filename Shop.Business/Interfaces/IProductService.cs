using Shop.Core.Entities;

namespace Shop.Business.Interfaces;

public interface IProductService
{
    void CreateProduct(string productName, string description, decimal price, int availableCount, int categoryId,int brandId);
    void DeactivateCartProduct(int productId, User user);
    void ActivateProduct(int productId);
    void DeactivateProduct(int productId);
    public void UpdateProduct(Product product, string name, string description, decimal price, int availableCount, int categoryId, int brandId);
    void SearchForProductViaName(string name);
    void SearchProductsViaBrand(string brandName);
    void SearchProductsViaCategory(string categoryName);
}

namespace Shop.Business.Interfaces;

public interface IProductService
{
    void CreateProduct(string productName, string description, decimal price, int availableCount, int categoryId,int brandId);
}

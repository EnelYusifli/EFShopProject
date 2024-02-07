using Shop.Core.Entities;

namespace Shop.Business.Interfaces;

public interface IBrandService
{
    void CreateBrand(string name);
    void UpdateBrand(Brand brand, string name);
    void DeactivateBrand(int brandId);
    void ActivateBrand(int brandId);
}

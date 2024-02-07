using Shop.Core.Entities;

namespace Shop.Business.Interfaces;

public interface ICategoryService
{
    void CreateCategory(string name, string description);
    void UpdateCategory(Category category, string name, string description);
    void DeactivateCategory(int categoryId);
    void ActivateCategory(int categoryId);
}

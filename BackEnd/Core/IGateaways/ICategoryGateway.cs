using Core.Models;
namespace Core.IGateways;
public interface ICategoryGateway
{
    int AddCategory(Category category);
    Category? GetCategory(int id);
    int? GetCategoryId(Category category);
    void DeleteCategory(int id);
    bool UpdateCategory(Category category);

}
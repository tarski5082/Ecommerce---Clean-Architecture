namespace Infrastructure.Repositories.Abstractions;

using Infrastructure.Models;

public interface ICategoryRepository
{
    int AddCategory(Category category);
    Category? GetCategory(int id);
    int? GetCategoryId(Category category);
    void DeleteCategory(int id);
    bool UpdateCategory(Category category);
}
namespace Infrastructure.Gateways;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Models;
using Core.IGateways;

public class CategoryGateway:ICategoryGateway
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryGateway(ICategoryRepository categoryRepository)
    {
        _categoryRepository=categoryRepository;
    }

    public int AddCategory(Core.Models.Category category)
    {
        var _category = new Category
        {
            Nom =category.Nom
        };
        return _categoryRepository.AddCategory(_category);
    }

    public Core.Models.Category GetCategory(int id){
        var category = _categoryRepository.GetCategory(id);
        return new Core.Models.Category
        {
            Id=category.Id,
            Nom=category.Nom
        };
    }

    public int? GetCategoryId(Core.Models.Category category)
    {
        return _categoryRepository.GetCategoryId(
            new Category
            {
                Id=category.Id,
                Nom=category.Nom
            }
        );
    }

    public void DeleteCategory(int id)
    {
        _categoryRepository.DeleteCategory(id);
    }

    public bool UpdateCategory(Core.Models.Category category)
    {
        return _categoryRepository.UpdateCategory(new Category
        {
            Id=category.Id,
            Nom=category.Nom
        });
    }
}
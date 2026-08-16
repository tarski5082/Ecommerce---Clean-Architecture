using Core.Models;
namespace Core.UseCases.Abstractions;
public interface IProductUseCases
{
    IEnumerable<Product>GetAllProducts();
    Product? GetProductById(int id);
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(int id);
}
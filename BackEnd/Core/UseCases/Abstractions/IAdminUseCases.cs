namespace Core.UseCases.Abstractions;
using Core.Models;
public interface IAdminUseCases
{
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(int id);
}
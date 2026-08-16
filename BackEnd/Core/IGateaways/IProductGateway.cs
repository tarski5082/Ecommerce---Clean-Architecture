using Core.Models;
namespace Core.IGateways;
public interface IProductGateway
{
    IEnumerable<Product>GetAllProducts();
    Product? GetProductById(int id);
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(int id);

}
using Core.IGateways;
using Core.UseCases.Abstractions;
using Core.Models;
namespace Core.UseCases;

public class ProductUseCases:IProductUseCases
{
    private readonly IProductGateway _productGateway;
    private readonly ICategoryGateway _categorieGateway;
    public ProductUseCases(IProductGateway productGateway,ICategoryGateway categorieGateway)
    {
        _productGateway=productGateway;
        _categorieGateway=categorieGateway;
    }


    public IEnumerable<Product> GetAllProducts()
    {
        return _productGateway.GetAllProducts();
    }
    public Product? GetProductById(int id){
        return _productGateway.GetProductById(id);
    }
    public void AddProduct(Product product)
    {
        _productGateway.AddProduct(product);
    }
    public void UpdateProduct(Product product)
    {
        _productGateway.UpdateProduct(product);
    }
    public void DeleteProduct(int id)
    {
        _productGateway.DeleteProduct(id);
    }

    public string getGategorie(int id)
    {
        var cat = _categorieGateway.GetCategory(id).Nom;
        if(cat==null || id==0) return "";
        return cat;
    }
}
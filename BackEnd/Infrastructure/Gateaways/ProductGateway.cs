using Infrastructure.Repositories.Abstractions;
using Infrastructure.Models;
using Core.IGateways;
namespace Infrastructure.Gateways;
public class ProductGateway:IProductGateway
{
    public IProductRepository _productRepository;

    public ProductGateway(IProductRepository productRepository)
    {
        _productRepository=productRepository;
    }


    public IEnumerable<Core.Models.Product> GetAllProducts()
    {
        var infraProducts = _productRepository.GetAllProducts();
        var coreProducts = new List<Core.Models.Product>();
        foreach(var p in infraProducts)
        {
            coreProducts.Add(new Core.Models.Product
            {
                Id = p.Id,
                Nom = p.Nom,
                Inventaire = p.Inventaire,
                PrixUnitaire = p.PrixUnitaire,
                ImageUrl = p.ImageUrl,
                IdCategorie = p.IdCategorie
            });
        }
        return coreProducts;
    }
    public Core.Models.Product? GetProductById(int id)
    {
        var p = _productRepository.GetProductById(id);
        return new Core.Models.Product
        {
            Id = p.Id,
            Nom = p.Nom,
            Inventaire = p.Inventaire,
            PrixUnitaire = p.PrixUnitaire,
            ImageUrl = p.ImageUrl,
            IdCategorie = p.IdCategorie
        };
    }
    public void AddProduct(Core.Models.Product product)
    {
        var _product = new Product
        {
            Id = product.Id,
            Nom = product.Nom,
            Inventaire = product.Inventaire,
            PrixUnitaire = product.PrixUnitaire,
            ImageUrl = product.ImageUrl,
            IdCategorie = product.IdCategorie
        };
        _productRepository.AddProduct(_product);
    }
    public void UpdateProduct(Core.Models.Product product)
    {
        var _product = new Product
        {
            Id = product.Id,
            Nom = product.Nom,
            Inventaire = product.Inventaire,
            PrixUnitaire = product.PrixUnitaire,
            ImageUrl = product.ImageUrl,
            IdCategorie = product.IdCategorie
        };
        _productRepository.UpdateProduct(_product);
    }
    public void DeleteProduct(int id)
    {
        _productRepository.DeleteProduct(id);
    }

}
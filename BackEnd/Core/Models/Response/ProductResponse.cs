namespace Core.Models.Response;
using Core.Models;
using Core.UseCases;
public class ProductResponse
{
    public int Id { get; set; }

    public string? Nom { get; set; }
    public int Inventaire { get; set; }
    public decimal PrixUnitaire { get; set; }

    public string? ImageUrl { get; set; }
    public string Categorie {get;set;}

    public ProductResponse(Product product,string _categorie)
    {
        Id=product.Id;
        Nom=product.Nom;
        Inventaire=product.Inventaire;
        PrixUnitaire=product.PrixUnitaire;
        ImageUrl=product.ImageUrl;
        Categorie = _categorie;
    }
    public ProductResponse(Product product)
    {
        Id=product.Id;
        Nom=product.Nom;
        Inventaire=product.Inventaire;
        PrixUnitaire=product.PrixUnitaire;
        ImageUrl=product.ImageUrl;
    }
}
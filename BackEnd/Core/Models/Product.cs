namespace Core.Models;

public class Product
{
   public int Id { get; set; }

    public string? Nom { get; set; }

    public int Inventaire { get; set; }

    public decimal PrixUnitaire { get; set; }

    public string? ImageUrl { get; set; }

    public int? IdCategorie { get; set; }
}
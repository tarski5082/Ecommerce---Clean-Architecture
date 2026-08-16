
namespace Core.Models;
public class CartItem
{
    public Guid Id{get;set;}
    public required int ProduitId{get;set;}
    public uint Quantite{get;set;}
    public Guid PanierId{get;set;}

}
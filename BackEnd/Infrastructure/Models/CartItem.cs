using System.Security.Cryptography;

namespace Infrastructure.Models;
public class CartItem
{
    public Guid Id{get;set;}
    public required int ProduitId{get;set;}
    public uint Quantite{get;set;}
   
    public Guid PanierId{get;set;}

    public Core.Models.CartItem ToCoreModel()
    {
        return new Core.Models.CartItem
        {
            Id=Id,
            ProduitId=ProduitId,
            Quantite=Quantite,
            PanierId=PanierId
        };
    }

    public static CartItem ToInfraModel(Core.Models.CartItem cartItem)
    {
        return new CartItem
        {
            Id=cartItem.Id,
            ProduitId=cartItem.ProduitId,
            Quantite=cartItem.Quantite,
            PanierId=cartItem.PanierId
        };
    }
}
namespace Infrastructure.Models;


public class Cart
{
    public Guid Id{get;set;}
    public required Guid UserId{get;set;}
    public string Etat { get; set; } = "en attente";
    public string Livraison {get;set;} = "en attente";

    
    public Core.Models.Cart ToCoreModel()
    {
        return new Core.Models.Cart
        {
            Id=Id,
            UserId=UserId,
            Etat=Etat,
            Livraison=Livraison
        };
    }

    public static Cart ToInfraModel(Core.Models.Cart cart)
    {
        return new Cart
        {
            Id=cart.Id,
            UserId=cart.UserId,
            Etat=cart.Etat,
            Livraison=cart.Livraison
        };
    }
}
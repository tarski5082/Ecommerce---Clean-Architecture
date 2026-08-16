namespace Core.Models;

public class Cart
{
    public Guid Id{get;set;}
    public required Guid UserId{get;set;}
    public string Etat { get; set; } = "en attente";
    public string Livraison {get;set;} = "en attente";


}
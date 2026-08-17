namespace Core.Models.Request;


public class ProfileRequest
{
    public required string Username{get;set;}
    public string Nom{get;set;}=string.Empty;
    public string Prenom{get;set;}=string.Empty;
    public AddressRequest? Facturation{get;set;}
    public AddressRequest? Livraison{get;set;}
    
}
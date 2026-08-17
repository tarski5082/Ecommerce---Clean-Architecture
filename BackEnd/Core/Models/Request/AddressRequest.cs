using Core.Models.Request;

namespace Core.Models.Request;

public class AddressRequest
{
    public required string Rue { get; set; }
    public required int Numero { get; set; }
    public string? Boite { get; set; }
    public required LocalityRequest localite{get;set;}
}
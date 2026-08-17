namespace Infrastructure.Models;
using Core.Models;

public class Address
{
    public int Id { get; set; }
    public string Rue { get; set; } = "";
    public int Numero { get; set; }
    public string? Boite { get; set; }
    public required int IdLocalite {get; set;}

    public static Address ToInfraModel(Core.Models.Address address)
    {
        return new Address
        {
            Rue=address.Rue,
            Numero=address.Numero,
            Boite=address.Boite,
            IdLocalite=address.IdLocalite
        };
    }
}


namespace Core.Models;
public class Address
{
    public int Id { get; set; }
    public string Rue { get; set; } = "";
    public int Numero { get; set; }
    public string? Boite { get; set; }
    public Locality Localite { get; set; }
}
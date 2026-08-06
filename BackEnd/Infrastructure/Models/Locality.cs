namespace Infrastructure.Models;

public class Locality
{
    public int Id { get; set; }
    public int CodePostal { get; set; }
    public string Ville { get; set; } = "";
    public string Province { get; set; } = "";
}
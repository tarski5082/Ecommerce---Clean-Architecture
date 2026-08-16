namespace Core.Models.Request;

public class LocalityRequest
{
    public required int CodePostal { get; set; }
    public required string Ville { get; set; } 
    public required string Province { get; set; } = "";
}
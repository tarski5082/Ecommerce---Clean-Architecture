namespace Infrastructure.Models;

public class User
{
    public Guid Id{get;set;}
    public required string Username{get;set;}
    public required string PasswordHash{get;set;}
    public bool IsAdmin{get;set;}
    public string Nom{get;set;}=string.Empty;
    public string Prenom{get;set;}=string.Empty;

    public int? IdFacturation{get;set;}
    public int? IdLivraison{get;set;}
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
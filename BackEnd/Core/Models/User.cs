using System;

namespace Core.Models;

public class User
{
    public Guid Id{get;set;}
    public required string Username{get;set;}
    public bool IsAdmin;
    public string Nom{get;set;}=string.Empty;
    public string Prenom{get;set;}=string.Empty;
    public int? IdFacturation{get;set;}
    public int? IdLivraison{get;set;}
}
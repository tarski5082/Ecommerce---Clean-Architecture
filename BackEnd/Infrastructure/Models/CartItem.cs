namespace Infrastructure.Models;
public class CartItem
{
    public Guid Id{get;set;}
    public required Product product{get;set;}
    public uint quantity{get;set;}
}
namespace Infrastructure.Models;

public class Cart
{
    public Guid Id{get;set;}
    public required User User{get;set;}
    public required List<CartItem> CartItems{get;set;}

}
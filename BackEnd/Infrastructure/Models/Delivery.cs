namespace Infrastructure.Models;

public class Order
{    
    public Guid Id{get;set;}
    public required Cart cart{get;set;}

}
namespace Core.Models.Response;
using Core.Models;

public class CartResponse
{
    public Guid Id{get;set;}
    public List<ProductResponse>CartItem{get;set;}

    public CartResponse(Guid cartId, List<ProductResponse> item)
    {
        Id=cartId;
        CartItem=item;
    }
}
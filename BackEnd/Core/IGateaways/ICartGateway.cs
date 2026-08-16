using Core.Models;

namespace Core.IGateways;

public interface ICartGateway
{
    IEnumerable<Cart> GetAllCarts(Guid UserId);
    Cart? GetCart(Guid CartId);
    void CreateCart(Guid UserId);
    void Delete(Guid CartId);
    void UpdateStatus(Cart cart);
    void AddCartItem(Guid cartId, IEnumerable<CartItem> items);
    void ClearCart(Guid cartId);
    void UpdateItemsInCart(Guid cartId, IEnumerable<CartItem> items);
    Guid GetId(Guid cartId);
}
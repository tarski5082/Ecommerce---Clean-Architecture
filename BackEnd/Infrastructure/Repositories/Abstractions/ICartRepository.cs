namespace Infrastructure.Repositories.Abstractions;

using Infrastructure.Models;

public interface ICartRepository
{
    IEnumerable<CartItem> getCartItems(Guid cartId);
    IEnumerable<Cart> GetAllCarts(Guid UserId);
    Cart? GetCart(Guid Id);
    void CreateCart(Guid Id);
    void Delete(Guid Id);
    void UpdateStatus(Cart cart);
    void AddCartItem(Guid cartId, IEnumerable<CartItem> items);
    void ClearCart(Guid cartId);
    void UpdateItemsInCart(Guid cartId, IEnumerable<CartItem> items);
    Guid GetId(Guid cartId);
}
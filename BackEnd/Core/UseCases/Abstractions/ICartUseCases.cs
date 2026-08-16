namespace Core.UseCases.Abstractions;

using Core.Models;
public interface ICartUseCases
{
    void CreateCart(Guid id);
    void PayCart(Guid id);
    IEnumerable<Cart> GetAll(Guid id);
    void AddCartItem(Guid UserId, IEnumerable<CartItemRequest> items);
    void UpdateItemsInCart(Guid UserId,IEnumerable<CartItemRequest>items);
    void Delete(Guid UserId);
}
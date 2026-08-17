namespace Infrastructure.Gateways;

using Core.IGateways;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

public class CartGateway:ICartGateway
{
    private readonly ICartRepository _cartRepository;

    public CartGateway(ICartRepository cartRepository)
    {
        _cartRepository=cartRepository;
    }


    public IEnumerable<Core.Models.Cart> GetAllCarts(Guid UserId)
    {
        var coreCart = new List<Core.Models.Cart>();
        var infraCart = _cartRepository.GetAllCarts(UserId);
        foreach(var cart in infraCart)
        {
            coreCart.Add(cart.ToCoreModel());
        }
        return coreCart;
    }
    public Core.Models.Cart GetCart(Guid cartId)
    {
        var cart = _cartRepository.GetCart(cartId);
        if(cart is null) throw new Exception("Le panier n'existe pas pour l'identifiant fournie");
        return cart.ToCoreModel();
    }
    public void CreateCart(Guid userId)
    {
        _cartRepository.CreateCart(userId);
    }
    public void Delete(Guid cartId)
    {
        _cartRepository.Delete(cartId);
    }
    public void UpdateStatus(Core.Models.Cart cart)
    {
        _cartRepository.UpdateStatus(new Cart
        {
            Id=cart.Id,
            UserId=cart.UserId,
            Etat=cart.Etat,
            Livraison=cart.Livraison
        });
    }
    public void AddCartItem(Guid cartId, IEnumerable<Core.Models.CartItem> items)
    {
        var infraCartItem = new List<CartItem>();
        foreach(var item in items)
        {
            infraCartItem.Add(CartItem.ToInfraModel(item));
        }
        _cartRepository.AddCartItem(cartId,infraCartItem);
    }
    public void ClearCart(Guid cartId)
    {
        _cartRepository.ClearCart(cartId);
    }
    public void UpdateItemsInCart(Guid cartId, IEnumerable<Core.Models.CartItem> items)
    {
        var infraCartItem = new List<CartItem>();
        foreach(var item in items)
        {
            infraCartItem.Add(CartItem.ToInfraModel(item));
        }
        _cartRepository.AddCartItem(cartId,infraCartItem);
    }

    public Guid GetId(Guid cartId)
    {
       return _cartRepository.GetId(cartId);
    }

    public IEnumerable<Core.Models.CartItem> GetCartItems(Guid cartId){
        var _cartItem = _cartRepository.getCartItems(cartId);
        var modelCartItem = new List<Core.Models.CartItem>();
        foreach(var item in _cartItem)
        {
            modelCartItem.Add(new Core.Models.CartItem
                {
                    Id=item.Id,
                    ProduitId=item.ProduitId,
                    Quantite=item.Quantite,
                    PanierId=item.PanierId
                });
                
            
        }
        return modelCartItem;
    }
}
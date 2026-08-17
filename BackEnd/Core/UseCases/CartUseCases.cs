namespace Core.UseCases;

using Core.IGateways;
using Core.Models;
using Core.Models.Response;
using Core.UseCases.Abstractions;

public class CartUseCases:ICartUseCases
{
    private readonly ICartGateway _cartGateway;
    private readonly IProductGateway _productgateway;

    public CartUseCases(ICartGateway cartGateway,IProductGateway productGateway)
    {
        _cartGateway=cartGateway;
        _productgateway=productGateway;
    }
    public void CreateCart(Guid UserId)
    {
        var carts = _cartGateway.GetAllCarts(UserId).Where(p=>p.Etat=="en attente");
        if (!carts.Any())
        {
            _cartGateway.CreateCart(UserId);
        }
        
    }

    public void PayCart(Guid UserId)
    {
        var cart = _cartGateway.GetAllCarts(UserId).FirstOrDefault(p => p.Etat == "en attente");

        if(cart is null)throw new Exception("Aucun panier en attente.");

        cart.Etat="effectue";

        _cartGateway.UpdateStatus(cart);

    }

    public IEnumerable<Cart> GetAll(Guid id){
        return _cartGateway.GetAllCarts(id);
    }

    public void AddCartItem(Guid UserId, IEnumerable<CartItemRequest> items)
    {
        CreateCart(UserId);
        var cart= _cartGateway.GetAllCarts(UserId).FirstOrDefault(p => p.Etat == "en attente");
        if(cart is null)throw new Exception("Aucun panier disponible pour ajouter un article");
        var cartId = cart.Id;
        var cartItems = new List<CartItem>();
        foreach(var item in items)
        {
            cartItems.Add(new CartItem
            {
                ProduitId=item.ProduitId,
                Quantite=item.Quantite,
                PanierId=cartId          
            });
        }


        _cartGateway.AddCartItem(cartId,cartItems);    
    }

    public void UpdateItemsInCart(Guid UserId,IEnumerable<CartItemRequest>items)
    {
        var cart= _cartGateway.GetAllCarts(UserId).FirstOrDefault(p => p.Etat == "en attente");

        if(cart is null) throw new Exception("Aucun panier disponible mettre a jour");

        var cartId = cart.Id;
        var cartItems = new List<CartItem>();
        foreach(var item in items)
        {
            cartItems.Add(new CartItem
            {
                ProduitId=item.ProduitId,
                Quantite=item.Quantite,
                PanierId=cartId          
            });
        }
        _cartGateway.UpdateItemsInCart(cartId,cartItems);
    }

    public void Delete(Guid UserId)
    {
        var cart= _cartGateway.GetAllCarts(UserId).FirstOrDefault(p => p.Etat == "en attente");

        if(cart is null) throw new Exception("Aucun panier disponible mettre a supprimer");

        _cartGateway.Delete(cart.Id);
    }

    public CartResponse GetAllCartItem(Guid cartId)
    {
        var items = _cartGateway.GetCartItems(cartId);
        var cartItems = new List<ProductResponse>();
        foreach(var item in items)
        {
            var product =_productgateway.GetProductById(item.ProduitId);
            cartItems.Add(new ProductResponse(product));
        }
        var cartResponse = new CartResponse(cartId,cartItems);

        return cartResponse;

    }

}
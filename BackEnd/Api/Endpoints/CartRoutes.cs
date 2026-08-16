using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Models;
using Core.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Core.UseCases.Abstractions;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection.Metadata.Ecma335;
using Org.BouncyCastle.Bcpg;
using MySqlX.XDevAPI.Common;

public static class CartRoutes
{

    public static Guid GetUserId(HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if(userIdClaim is null)
        {
             throw new UnauthorizedAccessException("User is not authenticated.");
        }
        if (!Guid.TryParse(userIdClaim.Value, out var userId))
        {
            throw new ArgumentException("Invalid user identifier format in token.");
        }

        return userId;

    }
    public static WebApplication AddCartRoutes(this WebApplication app)
    {
        var group = app.MapGroup("cart")
        .RequireAuthorization()
        .WithTags("Cart");


        group.MapPost("cart", ([FromBody]IEnumerable<CartItemRequest>items,ICartUseCases cartUseCase,HttpContext httpContext) =>
        {
            var userId = GetUserId(httpContext);
            cartUseCase.AddCartItem(userId,items.Select(i=>new CartItemRequest
            {
                ProduitId=i.ProduitId,
                Quantite=i.Quantite,

            }));
            return Results.Ok("Article ajoutee");
        }).Produces<object>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapDelete("cart",(ICartUseCases cartUseCase,HttpContext httpContext) =>
        {
            var userId =GetUserId(httpContext);
            cartUseCase.Delete(userId);
        }).Produces<object>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("carts",(ICartUseCases cartUseCase,HttpContext httpContext) =>
        {
            var userId = GetUserId(httpContext);
            cartUseCase.GetAll(userId);
        }).Produces<object>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status500InternalServerError);

        return app;
    }
}
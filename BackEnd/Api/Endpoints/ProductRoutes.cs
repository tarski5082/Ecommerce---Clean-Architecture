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
using Core.Models.Response;

namespace Api.EndPoints;

public static class ProductRoutes
{
    public static WebApplication AddProductRoutes(this WebApplication app)
    {
        var group = app.MapGroup("product")
        .RequireAuthorization()
        .WithTags("Product");

        group.MapGet("",(IProductUseCases productUseCase) =>
        {
            var products = productUseCase.GetAllProducts();
            return Results.Ok(products);
        }).AllowAnonymous()
        .WithName("Products")
        .Produces<object>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status500InternalServerError);;



        group.MapGet("/{id}",(int id,IProductUseCases productUseCase) =>
        {
            var products = productUseCase.GetProductById(id);
            var catId= products.IdCategorie;
            var categorie ="";
            if (catId != null)
            {
                categorie = productUseCase.getGategorie(catId.GetValueOrDefault());
            }
            return Results.Ok(new ProductResponse(products,categorie));
        }).AllowAnonymous()
        .WithName("Product")
        .Produces<object>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status500InternalServerError);;



        return app;
    }
}
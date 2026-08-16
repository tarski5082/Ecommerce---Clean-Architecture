using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Models;
using Core.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Core.UseCases.Abstractions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.EndPoints;

public static class ProfileRoutes
{
    public static WebApplication AddProfileRoutes(this WebApplication app)
    {
        var group = app.MapGroup("profil")
        .RequireAuthorization();


        group.MapPost("/update",([FromBody]ProfileRequest request,IProfilUseCases profilUseCases) =>
        {
            profilUseCases.UpdateProfile(request);
        }).AllowAnonymous();
        return app;

        
    }
}
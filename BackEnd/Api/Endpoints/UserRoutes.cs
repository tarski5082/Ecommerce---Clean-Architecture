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

public static class UserRoutes
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
    public static WebApplication AddUserRoutes(this WebApplication app)
    {
        var group = app.MapGroup("user")
        .RequireAuthorization()
        .WithTags("Users");

        group.MapPost("/auth",([FromBody] AuthenticationRequest request,IUserUseCases userUseCases,IConfiguration configuration) =>
        {
            var user = userUseCases.AuthenticateAndGetUser(request);
            if(user != null)
            {
                var issuer = configuration["Jwt:Issuer"];
                var audience = configuration["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]!);
                var expireTime = configuration["Jwt:ExpireTimeInMinutes"];
                var expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(expireTime ?? "30"));
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                };

                if (user.IsAdmin)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = expiration,
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);

                return Results.Ok(new { token = jwtToken });

            }
            else
            {
                return Results.Unauthorized();
            }
        })
        .AllowAnonymous()
        .WithName("Auth")
        .Produces<object>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status500InternalServerError);



        group.MapGet("/users",(IUserUseCases userUseCase) =>
        {
           var users = userUseCase.GetAllUsers(); 
           return Results.Ok(users);
        }).RequireAuthorization("AdminOnly")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status403Forbidden)
        .Produces(StatusCodes.Status500InternalServerError);


        group.MapPost("/register", ([FromBody] RegisterRequest request, IUserUseCases userUseCases) =>
        {
            userUseCases.Register(request);
            return Results.Ok(new { message = "User registered successfully" });
        }).AllowAnonymous()
        .WithName("Register")
        .Produces<object>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("",(IUserUseCases userUseCases,HttpContext httpContext) =>
        {
            var userId = GetUserId(httpContext);
            var profil = userUseCases.GetProfile(userId);
            return Results.Ok(profil);
        }).WithName("Profil")
        .Produces<object>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status500InternalServerError);
        group.MapPut("/livraison",([FromBody]AddressRequest request,IUserUseCases userUseCases,HttpContext httpContext) =>
        {
            var userId = GetUserId(httpContext);
            userUseCases.UpdateLivraison(request,userId);
            Results.Ok(new { message = "Delivery address updated successfully." });
        }).Produces<object>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapPut("/facturation",([FromBody]AddressRequest request,IUserUseCases userUseCases,HttpContext httpContext) =>
        {
            var userId = GetUserId(httpContext);
            userUseCases.UpdateFacturation(request,userId);
            Results.Ok(new { message = "Billing address updated successfully." });
        }).Produces<object>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status500InternalServerError);          
        return app;
    }
}
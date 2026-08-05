using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Core.UseCases.Abstractions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.EndPoints;

public static class UserRoutes
{
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
                var expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(expireTime ?? "5"));
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



        group.MapGet("",(IUserUseCases userUseCase) =>
        {
           var users = userUseCase.GetAllUsers(); 
           return Results.Ok(users);
        });


        group.MapPost("/register", ([FromBody] RegisterRequest request, IUserUseCases userUseCases) =>
        {
            userUseCases.Register(request);
            return Results.Ok(new { message = "User registered successfully" });
        })
        .WithName("Register")
        .Produces<object>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("/update",(HttpContext httpContext,[FromBody]Address address,IUserUseCases userUseCases)=>
        {
            var username = httpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(username))
            {
                return Results.Unauthorized();
            }
            userUseCases.addBillingAdress(username,address);
            return Results.Ok("Addresse ajoutee");
        });
        
       
        return app;
    }
}
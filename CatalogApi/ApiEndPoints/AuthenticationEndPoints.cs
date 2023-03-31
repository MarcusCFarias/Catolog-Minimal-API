using CatalogApi.Models;
using CatalogApi.Services;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.CompilerServices;

namespace CatalogApi.ApiEndPoints
{
    public static class AuthenticationEndPoints
    {
        public static void MapAuthenticationEndPoints(this WebApplication app)
        {
            app.MapPost("/login", [AllowAnonymous] (UserModel userModel, ITokenService tokenService) =>
            {
                if (userModel == null)
                    return Results.BadRequest();

                if (userModel.UserName == "user" && userModel.Password == "password123")
                {
                    var tokenString = tokenService.CreateToken(app.Configuration["Jwt:Key"],
                                                               app.Configuration["Jwt:Issuer"],
                                                               app.Configuration["Jwt:Audience"],
                                                               userModel);

                    return Results.Ok(new { token = tokenString });
                }
                else
                    return Results.BadRequest("Login Inválido");
            }).Produces(StatusCodes.Status400BadRequest)
              .Produces(StatusCodes.Status200OK)
              .WithName("Login")
              .WithTags("Authetication");
        }
    }
}

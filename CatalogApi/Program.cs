using CatalogApi.ApiEndPoints;
using CatalogApi.AppServicesExtensions;
using CatalogApi.Models;
using CatalogApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;
using System.Text;
using WebAPI.Context;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);
builder.AddApiSwager();
builder.AddPersistance();
builder.Services.AddCors();
builder.AddAuthenticationJwt();

var app = builder.Build();
app.MapAuthenticationEndPoints();
app.MapCategoriesEndPoints();
app.MapProductsEndPoints();

var enviroment = app.Environment;
app.UseExceptionHandling(enviroment)
    .UseSwaggerMiddleware()
    .UseAppCors(); 

app.UseAuthentication();
app.UseAuthorization();

app.Run();